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
            _InstallerThread = new Thread(() =>
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
            });
            _InstallerThread.Name = "NodeJS and Appium Installation";
            _InstallerThread.Priority = ThreadPriority.AboveNormal;
            _InstallerThread.Start();
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

            if (null != _InstallerThread && _InstallerThread.IsAlive)
            {
                _InstallerThread.Abort();
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

            // setup basic process info
            var appiumServerProcessStartInfo = new ProcessStartInfo();
            appiumServerProcessStartInfo.WorkingDirectory = AppiumPackageFolder;
            appiumServerProcessStartInfo.FileName = NodePath;
            appiumServerProcessStartInfo.Arguments = "server.js";
            appiumServerProcessStartInfo.UseShellExecute = true;

            // add more arguments
            appiumServerProcessStartInfo.Arguments += " -a " + IPAddress;
            appiumServerProcessStartInfo.Arguments += " -p " + Port.ToString();

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
        #endregion
    }
}
