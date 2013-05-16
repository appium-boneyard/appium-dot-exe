using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Appium
{
    public partial class MainForm : Form
    {
        /// <summary>downloads and installs nodejs</summary>
        private void _DownloadAndInstallNodeJS()
        {
            // determine the paths
            string npmZipPath = Path.Combine(AppiumRootFolder, "npm.zip");

            // download files from node and npm
            var webClient = new WebClient();
            this.Invoke(new Action(() => StatusBarText.Text = "Downloading NodeJS..."));
            webClient.DownloadFile(Constants.NodeJSWindowsBinaryUrl, NodePath);
            webClient.DownloadFile(Constants.NPMWindowsZipUrl, npmZipPath);

            // unzip npm
            this.Invoke(new Action(() => StatusBarText.Text = "Installing NPM..."));
            FastZip zip = new FastZip();
            zip.ExtractZip(npmZipPath, AppiumRootFolder, null);
            this.Invoke(new Action(() => StatusBarText.Text = ""));
            File.Delete(npmZipPath);
        }

        /// <summary>installs appium using npm</summary>
        private void _NPMInstallAppium()
        {
            // npm install appium
            ProcessStartInfo npmInstallProcessStartInfo = new ProcessStartInfo();
            npmInstallProcessStartInfo.WorkingDirectory = AppiumRootFolder;
            npmInstallProcessStartInfo.FileName = NPMPath;
            npmInstallProcessStartInfo.Arguments = "install appium";
            npmInstallProcessStartInfo.UseShellExecute = true;
            var npmInstallProcess = Process.Start(npmInstallProcessStartInfo);
            this.Invoke(new Action(() => StatusBarText.Text = "Installing Appium..."));
            npmInstallProcess.WaitForExit();
            this.Invoke(new Action(() => StatusBarText.Text = ""));
        }

        /// <summary>resets appium</summary>
        private void _ResetAppium()
        {
            // run reset
            ProcessStartInfo resetProcessInfo = new ProcessStartInfo();
            resetProcessInfo.WorkingDirectory = AppiumRootFolder;
            resetProcessInfo.FileName = Path.Combine(AppiumPackageFolder, "reset.bat");
            if (!File.Exists(resetProcessInfo.FileName))
                return;
            resetProcessInfo.Arguments = "";
            resetProcessInfo.UseShellExecute = true;
            var resetProcess = Process.Start(resetProcessInfo);
            this.Invoke(new Action(() => StatusBarText.Text = "Resetting Appium..."));
            resetProcess.WaitForExit();
            this.Invoke(new Action(() => StatusBarText.Text = ""));
        }

        /// <summary>detects available avds</summary>
        private void _DetectAVDs()
        {
            // use the android command to list the avds
            ProcessStartInfo avdDetectionProcessInfo = new ProcessStartInfo();
            avdDetectionProcessInfo.FileName = Path.Combine(AndroidSDKPath, "tools", "android.bat");
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
            foreach (var line in output.Split(new char [] {'\r', '\n'}))
            {
                if (line.Length > 0)
                {
                    avds.Add(line);
                }
            }
            this.Invoke(new Action(() => this.AVDs = avds.ToArray()));
        }
    }
}
