using Appium.Models;
using Appium.Models.Server;
using Appium.Utility;
using Ionic.Zip;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows;
using Microsoft.Win32;

namespace Appium.Engine
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
        /// Current Android SDK Path
        /// </summary>
        public string AndroidSDKPath
        {
            get { return null != _Settings && _Settings.UseSDKPath && !string.IsNullOrWhiteSpace(_Settings.SDKPath) ? _Settings.SDKPath : _AndroidSDKPath; } 
        }

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
                    _AVDs = AndroidSDKCommands.GetAvdList();
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
            appiumServerProcessStartInfo.Arguments = setup.GetArgumentsCmdLine() + " --log-no-color";
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

        /// <summary>
        /// On a new thread, checks for an update and updates accordingly 
        /// </summary>
        public void CheckForUpdate()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(_CheckForUpdate));
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
                ErrorDataReceived(string.Format("{0}", data));
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
            try
            {
                // use 3rd party zip software because System.IO.Compression will throw if the files already exists
                using (ZipFile zip = ZipFile.Read(npmZipPath))
                {
                    foreach (ZipEntry e in zip)
                    {
                        e.Extract(_AppiumRootFolder, ExtractExistingFileAction.OverwriteSilently);
                    }
                }
            }
            catch (Exception e)
            {
                _FireErrorData(e.Message);
            }

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


        /// <summary>
        /// checks for an update and updates accordingly
        /// 1) Gets the zipFile location
        /// 2) Gets Destination Location
        /// 3) Unzips the file into the temporary location
        /// 4) Run the update.bat file which does...
        /// 4.1) Copies all the files from temp folder to running directory (using xcopy) 
        /// 4.2) Deletes the temp folder
        /// 4.3) Restarts the Appium.exe again
        /// </summary>
        private void _CheckForUpdate(object o)
        {
            string version = null;
            string url = null;
            try
            {
                _FireOutputData("Checking if an update is available");
                // check to see if we have internet connection and what the latest update is
                var jsonString = new WebClient().DownloadString(@"https://raw.github.com/appium/appium.io/master/autoupdate/update-win.json");
                dynamic json = JsonConvert.DeserializeObject(jsonString);
                version = json.latest;
                url = json.url;
            }
            catch { return; }

            var currentVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();

            if (_IsServerVersionNewer(currentVersion, version))
            {
                _FireOutputData(string.Format("Update available to new version {0}", version));
                var upgradeMessage = string.Format("A new version of Appium is available. Would you like to download it?\n\n" +
                    "This Version:    {0}\nCurrent Version: {1}\nNOTE: This may take a few seconds depending on your connection", currentVersion, version);
                var downloadNew = MessageBox.Show(upgradeMessage, "Appium Upgrade Available", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;

                if (downloadNew)
                {
                    // download the latest code
                    var zipFileName = Path.GetFileName(url) ?? "AppiumForWindows.zip";
                    var zipFile = Path.Combine(Path.GetTempPath(), zipFileName);
                    var curFolder = Environment.CurrentDirectory;

                    // download the zip file
                    if (!File.Exists(zipFile))
                    {
                        try
                        {
                            _FireOutputData(string.Format("Downloading File from {0}", url));
                            new WebClient().DownloadFile(url, zipFile);
                        }
                        catch
                        {
                            _FireErrorData("Unable to download file");
                            MessageBox.Show("Unable to download file.\nPlease restart Appium", "Error",
                                MessageBoxButton.OK);
                            return;
                        }
                    }

                    _FireOutputData("Extracting zip file into temporary location");
                    try
                    {
                        // unzip the file into the temp location
                        // use 3rd party zip software because System.IO.Compression will throw if the files already exists
                        using (ZipFile zip = ZipFile.Read(zipFile))
                        {
                            foreach (ZipEntry e in zip)
                            {
                                e.Extract(curFolder, ExtractExistingFileAction.OverwriteSilently);
                            }
                        }
                    }
                    catch (PathTooLongException ptle)
                    {
                        _FireErrorData("Unable to update.");
                        _FireErrorData(ptle.Message);
                        _FireErrorData("Possible Fix would be to move main appium folder to a c:\\");
                        return;
                    }
                    catch (Exception e)
                    {
                        _FireErrorData("Unable to update.");
                        _FireErrorData(e.Message);
                        return;
                    }
                    finally
                    {
                        // remove the zip file
                        File.Delete(zipFile);
                    }


                    // install and restart the app
                    var restart = MessageBox.Show("Download is complete, would you like to install and restart?\nNOTE: This may take a few seconds", "Update and Restart", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
                    if (restart)
                    {
                        _FireOutputData("Restarting and updating to new version of Appium");
                        var info = new ProcessStartInfo();
                        info.Arguments = curFolder;
                        info.WindowStyle = ProcessWindowStyle.Hidden;
                        info.CreateNoWindow = true;
                        info.FileName = "update.bat";
                        Process.Start(info);

                        Application.Current.Dispatcher.Invoke(() => Application.Current.Shutdown());
                    }
                }
            }
            else
            {
                _FireOutputData("Update not available");
            }
        }


        /// <summary>
        /// Is the server version newer than the current application version
        /// </summary>
        /// <remarks>version number follows [major].[minor].[build].[revision]</remarks>
        /// <param name="currentVersion">current version of the application</param>
        /// <param name="serverVersion">current version on the server</param>
        /// <returns>true if current version is older than the server version, false otherwise; also false if unable to parse the versions</returns>
        private static bool _IsServerVersionNewer(string currentVersion, string serverVersion)
        {
            // if the same, then don't do anything
            if (currentVersion == serverVersion)
            {
                return false;
            }

            var retVal = false;
            // major, minor, build, revision  
            var tmp = currentVersion.Split('.');
            int curMajor = -1, curMinor = -1, curBuild = -1, curRev = -1;
            try
            {
                curMajor = int.Parse(tmp[0]);
                curMinor = int.Parse(tmp[1]);
                curBuild = int.Parse(tmp[2]);
                curRev = int.Parse(tmp[3]);
            }
            catch
            {
                return false;
            }

            tmp = serverVersion.Split('.');
            int serverMajor = -1, serverMinor = -1, serverBuild = -1, serverRev = -1;
            try
            {
                serverMajor = int.Parse(tmp[0]);
                serverMinor = int.Parse(tmp[1]);
                serverBuild = int.Parse(tmp[2]);
                serverRev = int.Parse(tmp[3]);
            }
            catch
            {
                return false;
            }

            if (curMajor < serverMajor)
            {
                retVal = true;
            }
            else if (curMajor == serverMajor)
            {
                if (curMinor < serverMinor)
                {
                    retVal = true;
                }
                else if (curMinor == serverMinor)
                {
                    if (curBuild < serverBuild)
                    {
                        retVal = true;
                    }
                    else if (curBuild == serverBuild)
                    {
                        if (curRev < serverRev)
                        {
                            retVal = true;
                        }
                    }
                }
            }

            return retVal;
        }

        #endregion Private Methods

    }

}

