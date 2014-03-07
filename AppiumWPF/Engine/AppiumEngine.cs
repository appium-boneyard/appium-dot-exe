using AppiumWPF.Models;
using AppiumWPF.Models.Server;
using AppiumWPF.Utility;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;

namespace AppiumWPF.Engine
{
    /// <summary>
    /// Delegate to inform listeners that data has been received
    /// </summary>
    /// <param name="data">string of</param>
    public delegate void DataReceivedDelegate(string data);

    /// <summary>
    /// Delegate to inform listeners that the running status of the node server has changed
    /// </summary>
    public delegate void RunningStatusChangedDelegate();

    sealed class AppiumEngine
    {
        private static readonly Lazy<AppiumEngine> _Instance = new Lazy<AppiumEngine>(() => new AppiumEngine());

        private List<string> _AVDs;

        private IAppiumAppSettings _Settings;

        #region Windows, Threads, and Processes
        /// <summary>process for the appium server</summary>
        private Process _AppiumServerProcess;

        /// <summary>the inspector window</summary>
        // private InspectorWindow.InpsectorForm _InspectorWindow;

        /// <summary>thread that runs setup actions after the form loads</summary>
        private Thread _LoadActionsThread;

        /// <summary>thread that monitors if the server is still running</summary>
        private Thread _ServerExitMonitorThread;
        #endregion Windows, Threads, and Processes

        #region Paths
        /// <summary>path to android sdk</summary>
        private string _AndroidSDKPath = Environment.GetEnvironmentVariable("ANDROID_HOME");

        /// <summary>lazy appium folder path</summary>
        private Lazy<string> __AppiumRootFolder = new Lazy<string>(() => Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof(MainWindow)).Location));

        /// <summary>appium folder path</summary>
        private string _AppiumRootFolder { get { return __AppiumRootFolder.Value; } }

        /// <summary>path to the appium package folder</summary>
        private string _AppiumPackageFolder { get { return Path.Combine(_NodeModulesFolder, "appium"); } }

        /// <summary>path to node.exe</summary>
        private string _NodePath { get { return Path.Combine(_AppiumRootFolder, "node.exe"); } }

        /// <summary>path to node package manager</summary>
        private string _NPMPath { get { return Path.Combine(_AppiumRootFolder, "npm.cmd"); } }

        /// <summary>path to the node modules folder</summary>
        private string _NodeModulesFolder { get { return Path.Combine(_AppiumRootFolder, "node_modules"); } }
        #endregion Paths

        #region Events
        /// <summary>Error Data Received Event</summary>
        public event DataReceivedDelegate ErrorDataReceived;

        /// <summary>Output Data Received Event</summary>
        public event DataReceivedDelegate OutputDataReceived;

        /// <summaryAppium Engines Run Status Changed (</summary>
        public event RunningStatusChangedDelegate RunningStatusChanged;
        #endregion Events

        #region Constructor
        /// <summary>Constructor</summary>
        private AppiumEngine() { }
        #endregion Constructor

        #region Public Properties
        /// <summary>
        /// Instance object for the Singleton class
        /// </summary>
        public static AppiumEngine Instance { get { return _Instance.Value; } }

        /// <summary>
        /// Is the Appium Engine Initialized
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// Is the appium server running
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Detects available AVDs
        /// </summary>
        public List<string> AVDs
        {
            get
            {
                // lazy load of AVD List
                if (null == _AVDs || 0 == _AVDs.Count)
                {
                    _AVDs = new List<string>();
                    try
                    {
                        // use the android command to list the avds
                        ProcessStartInfo avdDetectionProcessInfo = new ProcessStartInfo();
                        avdDetectionProcessInfo.FileName = Path.Combine(this._AndroidSDKPath, "tools", "android.bat");
                        if (File.Exists(avdDetectionProcessInfo.FileName))
                        {
                            avdDetectionProcessInfo.Arguments = "list avd -c";
                            avdDetectionProcessInfo.UseShellExecute = false;
                            avdDetectionProcessInfo.CreateNoWindow = true;
                            avdDetectionProcessInfo.RedirectStandardOutput = true;
                            var avdDetectionProcess = Process.Start(avdDetectionProcessInfo);
                            avdDetectionProcess.WaitForExit();

                            // read the output
                            string output = "";
                            using (System.IO.StreamReader myOutput = avdDetectionProcess.StandardOutput)
                            {
                                output = myOutput.ReadToEnd();
                            }
                            foreach (var line in output.Split(new char[] { '\r', '\n' }))
                            {
                                if (line.Length > 0)
                                {
                                    _AVDs.Add(line);
                                }
                            }
                        }
                    }
                    catch { }
                }
                return _AVDs;
            }
        }
        #endregion Public Properties

        #region Public Methods
        /// <summary>
        /// Initialize the Appium Engine with given settings
        /// Will download the node js and npm, appium package and reset appium config
        /// </summary>
        /// <param name="settings">Settings file to load</param>
        public void Init(IAppiumAppSettings settings)
        {
            _Settings = settings;
            _Settings.Load();

            this._LoadActionsThread = new Thread(() =>
            {
                if (!File.Exists(this._NodePath) || !File.Exists(this._NPMPath))
                {
                    _DownloadAndInstallNodeJS();
                }
                if (!Directory.Exists(this._AppiumPackageFolder))
                {
                    if (!Directory.Exists(this._NodeModulesFolder))
                    {
                        Directory.CreateDirectory(this._NodeModulesFolder);
                    }
                    _NPMInstallAppium();
                }
                if (!File.Exists(Path.Combine(this._AppiumPackageFolder, ".appiumconfig")))
                {
                    _ResetAppium();
                }

                IsInitialized = true;
            });

            this._LoadActionsThread.Name = "Load Actions";
            this._LoadActionsThread.Priority = ThreadPriority.AboveNormal;
            this._LoadActionsThread.Start();
        }

        /// <summary>
        /// Stop the Appium Node Server 
        /// </summary>
        public void Stop()
        {
            if (IsRunning && null != _ServerExitMonitorThread && _ServerExitMonitorThread.IsAlive && null != _AppiumServerProcess)
            {
                _AppiumServerProcess.OutputDataReceived -= _Process_OutputDataReceived;
                _AppiumServerProcess.ErrorDataReceived -= _Process_ErrorDataReceived;
                _AppiumServerProcess.Kill();
                _AppiumServerProcess.Dispose();
                _AppiumServerProcess = null;
                _FireOutputData("Killed Node Server.");
                _OnRunningChanged(false);
            }
        }

        /// <summary>
        /// Start the Appium Node Server
        /// </summary>
        public void Start()
        {
            //setup runner
            AppiumServerRunner setup = new AppiumServerRunner(this._NodePath, this._AppiumPackageFolder, _Settings);

            // setup basic process info
            var appiumServerProcessStartInfo = new ProcessStartInfo();
            appiumServerProcessStartInfo.WorkingDirectory = setup.WorkingDirectory;
            appiumServerProcessStartInfo.FileName = setup.Filename;
            appiumServerProcessStartInfo.Arguments = setup.GetArgumentsCmdLine() +" --log-no-color";
            appiumServerProcessStartInfo.RedirectStandardOutput = true;
            appiumServerProcessStartInfo.RedirectStandardError = true;
            appiumServerProcessStartInfo.CreateNoWindow = true;
            appiumServerProcessStartInfo.UseShellExecute = false;

            //set up the process and allow the thread to start it
            _AppiumServerProcess = new Process();
            _AppiumServerProcess.StartInfo = appiumServerProcessStartInfo;
            _AppiumServerProcess.OutputDataReceived += _Process_OutputDataReceived;
            _AppiumServerProcess.ErrorDataReceived += _Process_ErrorDataReceived;

            this._ServerExitMonitorThread = new Thread(() =>
            {
                _FireOutputData("Starting Node Server");
                _OnRunningChanged(true);
                this._AppiumServerProcess.Start();
                this._AppiumServerProcess.BeginOutputReadLine();
                this._AppiumServerProcess.BeginErrorReadLine();
                this._AppiumServerProcess.WaitForExit();
                _OnRunningChanged(false);
                _FireOutputData("Node Server Process Ended");
            });
            this._ServerExitMonitorThread.Name = "Server Exit Monitor";
            this._ServerExitMonitorThread.Priority = ThreadPriority.BelowNormal;
            this._ServerExitMonitorThread.Start();
        }

        #endregion Public Methods

        #region Private Methods

        #region Call Back Methods
        /// <summary>
        /// Callback method when the AppiumServerProcess fires off an ErrorDataReceived event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (null != e)
            {
                _FireErrorData(e.Data);
            }
        }

        /// <summary>
        /// Callback method when the AppiumServerProcess fires off an OutputDataReceived event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (null != e)
            {
                _FireOutputData(e.Data);
            }
        }

        /// <summary>
        /// Fires off Error Data Received event
        /// </summary>
        /// <param name="data">data for the listeners</param>
        private void _FireErrorData(string data)
        {
            if (null != ErrorDataReceived)
            {
                
                ErrorDataReceived(data);
            }
        }

        /// <summary>
        /// Fires off Output Data Received event
        /// </summary>
        /// <param name="data">data for the listeners</param>
        private void _FireOutputData(string data)
        {
            if (null != OutputDataReceived)
            {
                OutputDataReceived(data);
            }
        }
        
        /// <summary>
        /// Fire Event Handler to inform listeners of a status changed
        /// </summary>
        /// <param name="status">running status to change</param>
        private void _OnRunningChanged(bool status)
        {
            if (null != RunningStatusChanged && IsRunning != status)
            {
                IsRunning = status;
                RunningStatusChanged();
            }
        }
        #endregion Call Back Methods

        /// <summary>downloads and installs nodejs</summary>
        private void _DownloadAndInstallNodeJS()
        {
            // determine the paths
            string npmZipPath = Path.Combine(this._AppiumRootFolder, "npm.zip");

            // download files from node and npm
            var webClient = new WebClient();
            _FireOutputData("Downloading NodeJS...");
            webClient.DownloadFile(Constants.NodeJSWindowsBinaryUrl, this._NodePath);
            webClient.DownloadFile(Constants.NPMWindowsZipUrl, npmZipPath);
            _FireOutputData("Download NodeJS Complete...");

            // unzip npm
            _FireOutputData("Installing NPM...");
            FastZip zip = new FastZip();
            zip.ExtractZip(npmZipPath, this._AppiumRootFolder, null);
            _FireOutputData("Done Extracting NPM...");
            File.Delete(npmZipPath);
        }

        /// <summary>installs appium using npm</summary>
        private void _NPMInstallAppium()
        {
            // npm install appium
            ProcessStartInfo npmInstallProcessStartInfo = new ProcessStartInfo();
            npmInstallProcessStartInfo.WorkingDirectory = this._AppiumRootFolder;
            npmInstallProcessStartInfo.FileName = this._NPMPath;
            npmInstallProcessStartInfo.Arguments = "install appium";
            npmInstallProcessStartInfo.UseShellExecute = false;
            npmInstallProcessStartInfo.CreateNoWindow = true;
            npmInstallProcessStartInfo.RedirectStandardError = true;
            npmInstallProcessStartInfo.RedirectStandardOutput = true;

            var npmInstallProcess = new Process();
            npmInstallProcess.StartInfo = npmInstallProcessStartInfo;
            npmInstallProcess.OutputDataReceived += _Process_OutputDataReceived;
            npmInstallProcess.ErrorDataReceived += _Process_ErrorDataReceived;

            _FireOutputData("Installing Appium...");
            npmInstallProcess.Start();
            npmInstallProcess.BeginErrorReadLine();
            npmInstallProcess.BeginOutputReadLine();
            npmInstallProcess.WaitForExit();
            _FireOutputData("Done installing Appium...");
        }

        /// <summary>resets appium</summary>
        private void _ResetAppium()
        {
            // run reset
            ProcessStartInfo resetProcessInfo = new ProcessStartInfo();
            resetProcessInfo.WorkingDirectory = this._AppiumRootFolder;
            resetProcessInfo.FileName = Path.Combine(this._AppiumPackageFolder, "reset.bat");
            if (!File.Exists(resetProcessInfo.FileName))
                return;
            resetProcessInfo.Arguments = "";
            resetProcessInfo.UseShellExecute = false;
            resetProcessInfo.CreateNoWindow = true;
            resetProcessInfo.RedirectStandardError = true;
            resetProcessInfo.RedirectStandardOutput = true;

            var resetProcess = new Process();
            resetProcess.StartInfo = resetProcessInfo;
            resetProcess.OutputDataReceived += _Process_OutputDataReceived;
            resetProcess.ErrorDataReceived += _Process_ErrorDataReceived;

            _FireOutputData("Resetting Appium...");
            resetProcess.Start();
            resetProcess.BeginErrorReadLine();
            resetProcess.BeginOutputReadLine();
            resetProcess.WaitForExit();
            _FireOutputData("Done Resetting Appium...");
        }

        #endregion Private Methods


    }
}
