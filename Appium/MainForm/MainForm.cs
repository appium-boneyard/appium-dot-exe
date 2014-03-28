using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Appium.Models.Server;
using System.Text;

namespace Appium.MainWindow
{
    /// <summary>the main form</summary>
    public partial class MainForm : Form
    {
        private Model _Model;

        /// <summary>constructor</summary>
        public MainForm()
        {
            // initialize
            _Model = new Model(this, new Models.DefaultAppiumAppSettings());
            //_Model = new Model(this);
            InitializeComponent();
            this.modelBindingSource.DataSource = _Model;
            this.FormClosing += new FormClosingEventHandler(this.FileMenuExitItem_Click);
            richTextBox1.BackColor = System.Drawing.Color.Black;
            richTextBox1.ForeColor = System.Drawing.Color.White;
        }

        #region Windows, Threads, and Processes
        /// <summary>process for the appium server</summary>
        private Process _AppiumServerProcess;

        /// <summary>the inspector window</summary>
        private InspectorWindow.InpsectorForm _InspectorWindow;

        /// <summary>thread that runs setup actions after the form loads</summary>
        private Thread _LoadActionsThread;

        /// <summary>thread that monitors if the server is still running</summary>
        private Thread _ServerExitMonitorThread;
        #endregion

        #region Paths
        /// <summary>path to android sdk</summary>
        private string _AndroidSDKPath = Environment.GetEnvironmentVariable("ANDROID_HOME");

        /// <summary>lazy appium folder path</summary>
        private Lazy<string> __AppiumRootFolder = new Lazy<string>(() => Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof(MainForm)).Location));

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
        #endregion

        #region Form Handlers
        /// <summary>called whenever the form loads</summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        public void MainForm_Load(object sender, System.EventArgs e)
        {
            this._LoadActionsThread = new Thread(() =>
            {
                this._Model.LaunchButtonEnabled = false;
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
                this._Model.LaunchButtonEnabled = true;
                _DetectAVDs();
            });
            this._LoadActionsThread.Name = "Load Actions";
            this._LoadActionsThread.Priority = ThreadPriority.AboveNormal;
            this._LoadActionsThread.Start();
        }
        #endregion

        #region Menu Handlers
        /// <summary>called when the inspector menu item on the file menu is clicked</summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void FileMenuInspectorItem_Click(object sender, EventArgs e)
        {
            if (null == this._InspectorWindow || this._InspectorWindow.IsDisposed)
            {
                this._InspectorWindow = new InspectorWindow.InpsectorForm(this._Model);
            }
            if (!this._InspectorWindow.Visible)
            {
                if (this._InspectorWindow.Connect())
                {
                    this._InspectorWindow.Show();
                }
                else
                {
                    MessageBox.Show(String.Format("Failed to connect to appium: {0}", this._InspectorWindow.LastMessage));
                }
            }
        }

        /// <summary>called when the preferences menu item on the file menu is clicked</summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        public void FileMenuPreferencesItem_Click(object sender, EventArgs e)
        {
            this._Model.OpenPreferences();
        }

        /// <summary>called when the exit menu item on the file menu is clicked</summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        public void FileMenuExitItem_Click(object sender, EventArgs e)
        {
            // terminate appium server if its running
            if (null != this._AppiumServerProcess && !this._AppiumServerProcess.HasExited)
            {
                this._AppiumServerProcess.Kill();
            }

            if (null != this._LoadActionsThread && this._LoadActionsThread.IsAlive)
            {
                this._LoadActionsThread.Abort();
            }

            if (null != this._ServerExitMonitorThread && this._ServerExitMonitorThread.IsAlive)
            {
                this._ServerExitMonitorThread.Abort();
            }

            Application.Exit();
        }
        #endregion

        #region Button Handlers
        /// <summary>called when the launch button is clicked</summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        public void LaunchButton_Click(object sender, EventArgs e)
        {
            // kill the process if it's already running
            if (null != this._AppiumServerProcess && !this._AppiumServerProcess.HasExited)
            {
                this._AppiumServerProcess.Kill();
                return;
            }

            // don't launch if a remote server is being used
            if (this._Model.UseRemoteServer)
            {
                return;
            }

            //setup runner
            AppiumServerRunner setup = new AppiumServerRunner(this._NodePath, this._AppiumPackageFolder, _Model.Settings);

            // setup basic process info
            var appiumServerProcessStartInfo = new ProcessStartInfo();
            appiumServerProcessStartInfo.WorkingDirectory = setup.WorkingDirectory;
            appiumServerProcessStartInfo.FileName = setup.Filename;
            appiumServerProcessStartInfo.Arguments = setup.GetArgumentsCmdLine() + " --log-no-color";
            appiumServerProcessStartInfo.RedirectStandardOutput = true;
            appiumServerProcessStartInfo.RedirectStandardError = true;
            appiumServerProcessStartInfo.CreateNoWindow = true;
            appiumServerProcessStartInfo.UseShellExecute = false;


            // set up the process and allow the thread to start it
            _AppiumServerProcess = new Process();
            _AppiumServerProcess.StartInfo = appiumServerProcessStartInfo;
            _AppiumServerProcess.OutputDataReceived += _AppiumServerProcess_OutputDataReceived;
            _AppiumServerProcess.ErrorDataReceived += _AppiumServerProcess_ErrorDataReceived;

            this._ServerExitMonitorThread = new Thread(() =>
            {
                this._Model.LaunchButtonText = "Stop";
                this._AppiumServerProcess.Start();
                this._AppiumServerProcess.BeginOutputReadLine();
                this._AppiumServerProcess.BeginErrorReadLine();
                this._AppiumServerProcess.WaitForExit();
                this._Model.LaunchButtonText = "Launch";
            });
            this._ServerExitMonitorThread.Name = "Server Exit Monitor";
            this._ServerExitMonitorThread.Priority = ThreadPriority.BelowNormal;
            this._ServerExitMonitorThread.Start();
        }

        void _AppiumServerProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        void _AppiumServerProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    _AppiumServerProcess_OutputDataReceived(sender, e);
                };
                Invoke(del);
            }
            else
            {
                richTextBox1.Text += e.Data + "\n";
            }
        }

        public void AppPathBrowseButton_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog appPathDialog = new OpenFileDialog();
            if (File.Exists(this._Model.ApplicationPath))
            {
                appPathDialog.InitialDirectory = Path.GetDirectoryName(this._Model.ApplicationPath);
            }
            appPathDialog.CheckFileExists = true;
            appPathDialog.Multiselect = false;
            appPathDialog.Filter = "Android apps (*.apk)|*.apk";
            appPathDialog.Title = "Select Your Android App";
            var result = appPathDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _Model.SetupNewApplicationPath(appPathDialog.FileName);
            }
        }
        #endregion

        #region Utility
        /// <summary>downloads and installs nodejs</summary>
        private void _DownloadAndInstallNodeJS()
        {
            // determine the paths
            string npmZipPath = Path.Combine(this._AppiumRootFolder, "npm.zip");

            // download files from node and npm
            var webClient = new WebClient();
            this._Model.StatusBarText = "Downloading NodeJS...";
            webClient.DownloadFile(Constants.NodeJSWindowsBinaryUrl, this._NodePath);
            webClient.DownloadFile(Constants.NPMWindowsZipUrl, npmZipPath);

            // unzip npm
            this._Model.StatusBarText = "Installing NPM...";
            FastZip zip = new FastZip();
            zip.ExtractZip(npmZipPath, this._AppiumRootFolder, null);
            this._Model.StatusBarText = "";
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
            var npmInstallProcess = Process.Start(npmInstallProcessStartInfo);
            this._Model.StatusBarText = "Installing Appium...";
            npmInstallProcess.WaitForExit();
            this._Model.StatusBarText = "";
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
            resetProcessInfo.UseShellExecute = true;
            var resetProcess = Process.Start(resetProcessInfo);
            this._Model.StatusBarText = "Resetting Appium...";
            resetProcess.WaitForExit();
            this._Model.StatusBarText = "";
        }

        /// <summary>detects available avds</summary>
        private void _DetectAVDs()
        {
            List<string> avds = new List<string>();
            try
            {
                // use the android command to list the avds
                ProcessStartInfo avdDetectionProcessInfo = new ProcessStartInfo();
                avdDetectionProcessInfo.FileName = Path.Combine(this._AndroidSDKPath, "tools", "android.bat");
                if (!File.Exists(avdDetectionProcessInfo.FileName))
                    return;
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
                        avds.Add(line);
                    }
                }

            }
            catch { }
            this._Model.AVDs = avds.ToArray();
        }
        #endregion
    }
}
