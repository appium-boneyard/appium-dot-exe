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
            new Thread(() =>
            {
                if (!File.Exists(NodePath) || !File.Exists(NPMPath))
                {
                    _DownloadAndInstallNodeJS();
                }

                if (!Directory.Exists(AppiumPackageFolder))
                {
                    _NPMInstallAppium();
                }
            }).Start();
        }

        #region Menu Handlers
        /// <summary>called when the exit menu item is clicked on the file menu</summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void FileMenuExitItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Button Handlers
        /// <summary>called when the launch button is clicked</summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void LaunchButton_Click(object sender, EventArgs e)
        {
            // setup basic process info
            var appiumServerProcessStartInfo = new ProcessStartInfo();
            appiumServerProcessStartInfo.WorkingDirectory = AppiumRootFolder;
            appiumServerProcessStartInfo.FileName = NodePath;
            appiumServerProcessStartInfo.Arguments = Path.Combine(AppiumPackageFolder, "server.js");
            appiumServerProcessStartInfo.CreateNoWindow = true;
            appiumServerProcessStartInfo.UseShellExecute = false;
            appiumServerProcessStartInfo.RedirectStandardOutput = true;
            appiumServerProcessStartInfo.RedirectStandardInput = true;
            appiumServerProcessStartInfo.RedirectStandardError = true;

            // add more arguments
            appiumServerProcessStartInfo.Arguments += " -a " + IPAddress;
            appiumServerProcessStartInfo.Arguments += " -p " + Port.ToString();

            // start the process
            var appiumServerProcess = Process.Start(appiumServerProcessStartInfo);
        }
        #endregion
    }
}
