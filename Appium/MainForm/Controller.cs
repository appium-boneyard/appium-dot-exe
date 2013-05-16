﻿using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Appium.MainWindow
{
    public class Controller
    {
        public Controller(Model model)
        {
            this._Model = model;
        }

        private Model _Model;

        /// <summary>called whenever the form loads</summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        public void MainForm_Load(object sender, System.EventArgs e)
        {
            this._Model.LoadActionsThread = new Thread(() =>
            {
                this._Model.LaunchButtonEnabled = false;
                if (!File.Exists(this._Model.NodePath) || !File.Exists(this._Model.NPMPath))
                {
                    _DownloadAndInstallNodeJS();
                }
                if (!Directory.Exists(this._Model.AppiumPackageFolder))
                {
                    _NPMInstallAppium();
                }
                if (!File.Exists(Path.Combine(this._Model.AppiumPackageFolder, ".appiumconfig")))
                {
                    _ResetAppium();
                }
                this._Model.LaunchButtonEnabled = true;
                _DetectAVDs();
            });
            this._Model.LoadActionsThread.Name = "Load Actions";
            this._Model.LoadActionsThread.Priority = ThreadPriority.AboveNormal;
            this._Model.LoadActionsThread.Start();
        }

        #region Menu Handlers
        /// <summary>called when the exit menu item is clicked on the file menu</summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        public void FileMenuExitItem_Click(object sender, EventArgs e)
        {
            // terminate appium server if its running
            if (null != this._Model.AppiumServerProcess && !this._Model.AppiumServerProcess.HasExited)
            {
                this._Model.AppiumServerProcess.Kill();
            }

            if (null != this._Model.LoadActionsThread && this._Model.LoadActionsThread.IsAlive)
            {
                this._Model.LoadActionsThread.Abort();
            }

            if (null != this._Model.ServerExitMonitorThread && this._Model.ServerExitMonitorThread.IsAlive)
            {
                this._Model.ServerExitMonitorThread.Abort();
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
            if (null != this._Model.AppiumServerProcess && !this._Model.AppiumServerProcess.HasExited)
            {
                this._Model.AppiumServerProcess.Kill();
                return;
            }

            // don't launch if a remote server is being used
            if (this._Model.UseRemoteServer)
            {
                return;
            }

            // setup basic process info
            var appiumServerProcessStartInfo = new ProcessStartInfo();
            appiumServerProcessStartInfo.WorkingDirectory = this._Model.AppiumPackageFolder;
            appiumServerProcessStartInfo.FileName = this._Model.NodePath;
            appiumServerProcessStartInfo.Arguments = "server.js";
            appiumServerProcessStartInfo.UseShellExecute = true;

            // add more arguments
            appiumServerProcessStartInfo.Arguments += " --address " + this._Model.IPAddress;
            appiumServerProcessStartInfo.Arguments += " --port " + this._Model.Port.ToString();
            if (this._Model.UseApplicationPath)
            {
                appiumServerProcessStartInfo.Arguments += " --app " + this._Model.ApplicationPath;
            }

            // add android-specific arguments
            if (this._Model.UseAndroidActivity)
            {
                appiumServerProcessStartInfo.Arguments += " --app-activity " + this._Model.AndroidActivity;
            }
            if (this._Model.UseAndroidPackage)
            {
                appiumServerProcessStartInfo.Arguments += " --app-pkg " + this._Model.AndroidPackage;
            }
            if (this._Model.LaunchAVD)
            {
                appiumServerProcessStartInfo.Arguments += " --avd " + this._Model.AVDToLaunch;
            }
            if (this._Model.UseAndroidWaitActivity)
            {
                appiumServerProcessStartInfo.Arguments += " --app-wait-activity " + this._Model.AndroidWaitActivity;
            }

            // start the process
            this._Model.AppiumServerProcess = Process.Start(appiumServerProcessStartInfo);
            this._Model.ServerExitMonitorThread = new Thread(() =>
            {
                this._Model.LaunchButtonText = "Stop";
                this._Model.AppiumServerProcess.WaitForExit();
                this._Model.LaunchButtonText = "Launch";
            });
            this._Model.ServerExitMonitorThread.Name = "Server Exit Monitor";
            this._Model.ServerExitMonitorThread.Priority = ThreadPriority.BelowNormal;
            this._Model.ServerExitMonitorThread.Start();
        }

        public void AppPathBrowseButton_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog appPathDialog = new OpenFileDialog();
            appPathDialog.CheckFileExists = true;
            appPathDialog.Multiselect = false;
            appPathDialog.Filter = "Anrdoid apps (*.apk)|*.apk";
            appPathDialog.Title = "Select Your Android App";
            var result = appPathDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this._Model.ApplicationPath = appPathDialog.FileName;
            }
        }
        #endregion

        /// <summary>downloads and installs nodejs</summary>
        private void _DownloadAndInstallNodeJS()
        {
            // determine the paths
            string npmZipPath = Path.Combine(this._Model.AppiumRootFolder, "npm.zip");

            // download files from node and npm
            var webClient = new WebClient();
            this._Model.StatusBarText = "Downloading NodeJS...";
            webClient.DownloadFile(Constants.NodeJSWindowsBinaryUrl, this._Model.NodePath);
            webClient.DownloadFile(Constants.NPMWindowsZipUrl, npmZipPath);

            // unzip npm
            this._Model.StatusBarText = "Installing NPM...";
            FastZip zip = new FastZip();
            zip.ExtractZip(npmZipPath, this._Model.AppiumRootFolder, null);
            this._Model.StatusBarText = "";
            File.Delete(npmZipPath);
        }

        /// <summary>installs appium using npm</summary>
        private void _NPMInstallAppium()
        {
            // npm install appium
            ProcessStartInfo npmInstallProcessStartInfo = new ProcessStartInfo();
            npmInstallProcessStartInfo.WorkingDirectory = this._Model.AppiumRootFolder;
            npmInstallProcessStartInfo.FileName = this._Model.NPMPath;
            npmInstallProcessStartInfo.Arguments = "install appium";
            npmInstallProcessStartInfo.UseShellExecute = true;
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
            resetProcessInfo.WorkingDirectory = this._Model.AppiumRootFolder;
            resetProcessInfo.FileName = Path.Combine(this._Model.AppiumPackageFolder, "reset.bat");
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
            // use the android command to list the avds
            ProcessStartInfo avdDetectionProcessInfo = new ProcessStartInfo();
            avdDetectionProcessInfo.FileName = Path.Combine(this._Model.AndroidSDKPath, "tools", "android.bat");
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
            List<string> avds = new List<string>();
            foreach (var line in output.Split(new char[] { '\r', '\n' }))
            {
                if (line.Length > 0)
                {
                    avds.Add(line);
                }
            }
            this._Model.AVDs = avds.ToArray();
        }
    }
}
