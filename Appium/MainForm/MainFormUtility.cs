﻿using ICSharpCode.SharpZipLib.Zip;
using System;
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
            this.Invoke(new Action(() => LogTextBox.Text = "Downloading NodeJS..."));
            webClient.DownloadFile(Constants.NodeJSWindowsBinaryUrl, NodePath);
            webClient.DownloadFile(Constants.NPMWindowsZipUrl, npmZipPath);

            // unzip npm
            this.Invoke(new Action(() => LogTextBox.Text = "Installing NPM..."));
            FastZip zip = new FastZip();
            zip.ExtractZip(npmZipPath, AppiumRootFolder, null);
            this.Invoke(new Action(() => LogTextBox.Text = ""));
            File.Delete(npmZipPath);
        }

        /// <summary>installs appium using npm</summary>
        private void _NPMInstallAppium()
        {
            ProcessStartInfo npmInstallProcessStartInfo = new ProcessStartInfo();
            npmInstallProcessStartInfo.WorkingDirectory = AppiumRootFolder;
            npmInstallProcessStartInfo.FileName = NPMPath;
            npmInstallProcessStartInfo.Arguments = "install appium";
            npmInstallProcessStartInfo.CreateNoWindow = true;
            npmInstallProcessStartInfo.UseShellExecute = false;
            npmInstallProcessStartInfo.RedirectStandardOutput = true;
            npmInstallProcessStartInfo.RedirectStandardInput = true;
            npmInstallProcessStartInfo.RedirectStandardError = true;
            var npmInstallProcess = Process.Start(npmInstallProcessStartInfo);
            this.Invoke(new Action(() => LogTextBox.Text = "Installing Appium..."));
            npmInstallProcess.WaitForExit();
            this.Invoke(new Action(() => LogTextBox.Text = ""));
        }
    }
}
