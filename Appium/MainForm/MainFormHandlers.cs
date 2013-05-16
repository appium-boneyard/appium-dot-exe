using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Appium
{
    public partial class MainForm : Form
    {
        /// <summary>called whenever the form loads</summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void MainForm_Load(object sender, System.EventArgs e)
        {
            _LoadActionsThread = new Thread(() =>
            {
                this.Invoke(new Action(() => LaunchButton.Enabled = false));
                if (!File.Exists(NodePath) || !File.Exists(NPMPath))
                {
                    _DownloadAndInstallNodeJS();
                }
                if (!Directory.Exists(AppiumPackageFolder))
                {
                    _NPMInstallAppium();
                }
                if (!File.Exists(Path.Combine(AppiumPackageFolder, ".appiumconfig")))
                {
                    _ResetAppium();
                }
                this.Invoke(new Action(() => LaunchButton.Enabled = true));
                _DetectAVDs();
            });
            _LoadActionsThread.Name = "Load Actions";
            _LoadActionsThread.Priority = ThreadPriority.AboveNormal;
            _LoadActionsThread.Start();
        }

        #region Menu Handlers
        /// <summary>called when the exit menu item is clicked on the file menu</summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void FileMenuExitItem_Click(object sender, EventArgs e)
        {
            // terminate appium server if its running
            if (null != AppiumServerProcess && !AppiumServerProcess.HasExited)
            {
                AppiumServerProcess.Kill();
            }

            if (null != _LoadActionsThread && _LoadActionsThread.IsAlive)
            {
                _LoadActionsThread.Abort();
            }

            if (null !=_ServerExitMonitorThread && _ServerExitMonitorThread.IsAlive)
            {
                _ServerExitMonitorThread.Abort();
            }

            Application.Exit();
        }
        #endregion

        #region Button Handlers
        /// <summary>called when the launch button is clicked</summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void LaunchButton_Click(object sender, EventArgs e)
        {
            // kill the process if it's already running
            if (null != AppiumServerProcess && !AppiumServerProcess.HasExited)
            {
                AppiumServerProcess.Kill();
                return;
            }

            // don't launch if a remote server is being used
            if (this.UseRemoteServer)
            {
                return;
            }

            // setup basic process info
            var appiumServerProcessStartInfo = new ProcessStartInfo();
            appiumServerProcessStartInfo.WorkingDirectory = AppiumPackageFolder;
            appiumServerProcessStartInfo.FileName = NodePath;
            appiumServerProcessStartInfo.Arguments = "server.js";
            appiumServerProcessStartInfo.UseShellExecute = true;

            // add more arguments
            appiumServerProcessStartInfo.Arguments += " --address " + IPAddress;
            appiumServerProcessStartInfo.Arguments += " --port " + Port.ToString();
            if (this.UseApplicationPath)
            {
                appiumServerProcessStartInfo.Arguments += " --app " + this.ApplicationPath;
            }

            // add android-specific arguments
            if (this.UseAndroidActivity)
            {
                appiumServerProcessStartInfo.Arguments += " --app-activity " + this.AndroidActivity;
            }
            if (this.UseAndroidPackage)
            {
                appiumServerProcessStartInfo.Arguments += " --app-pkg " + this.AndroidPackage;
            }
            if (this.LaunchAVD)
            {
                appiumServerProcessStartInfo.Arguments += " --avd " + this.AVDToLaunch;
            }
            if (this.UseAndroidWaitActivity)
            {
                appiumServerProcessStartInfo.Arguments += " --app-wait-activity " + this.AndroidWaitActivity;
            }

            // start the process
            AppiumServerProcess = Process.Start(appiumServerProcessStartInfo);
            _ServerExitMonitorThread = new Thread(() =>
            {
                this.Invoke(new Action(() => LaunchButtonText = "Stop"));
                AppiumServerProcess.WaitForExit();
                this.Invoke(new Action(() => LaunchButtonText = "Launch"));
            });
            _ServerExitMonitorThread.Name = "Server Exit Monitor";
            _ServerExitMonitorThread.Priority = ThreadPriority.BelowNormal;
            _ServerExitMonitorThread.Start();
        }

        private void AppPathBrowseButton_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog appPathDialog = new OpenFileDialog();
            appPathDialog.CheckFileExists = true;
            appPathDialog.Multiselect = false;
            appPathDialog.Filter = "Anrdoid apps (*.apk)|*.apk";
            appPathDialog.Title = "Select Your Android App";
            var result = appPathDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.Invoke(new Action(() => this.ApplicationPath = appPathDialog.FileName ));
            }
        }
        #endregion
    }
}
