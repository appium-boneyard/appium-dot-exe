using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Appium.MainWindow
{
    public class Model
    {
        /// <summary>constructor</summary>
        /// <param name="form">main form</param>
        public Model(MainForm form)
        {
            this._View = form;
        }

        /// <summary>main form</summary>
        public MainForm _View;

        /// <summary>process for the appium server</summary>
        public Process AppiumServerProcess;

        /// <summary>thread that runs setup actions after the form loads</summary>
        public Thread LoadActionsThread;

        /// <summary>thread that monitors if the server is still running</summary>
        public Thread ServerExitMonitorThread;

        #region Paths
        /// <summary>path to android sdk</summary>
        public string AndroidSDKPath = Environment.GetEnvironmentVariable("ANDROID_HOME");

        /// <summary>lazy appium folder path</summary>
        private Lazy<string> _AppiumRootFolder = new Lazy<string>(() => Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof(MainForm)).Location));

        /// <summary>appium folder path</summary>
        public string AppiumRootFolder { get { return _AppiumRootFolder.Value; } }

        /// <summary>path to the appium package folder</summary>
        public string AppiumPackageFolder { get { return Path.Combine(NodeModulesFolder, "appium"); } }

        /// <summary>path to node.exe</summary>
        public string NodePath { get { return Path.Combine(AppiumRootFolder, "node.exe"); } }

        /// <summary>path to node package manager</summary>
        public string NPMPath { get { return Path.Combine(AppiumRootFolder, "npm.cmd"); } }

        /// <summary>path to the node modules folder</summary>
        public string NodeModulesFolder { get { return Path.Combine(AppiumRootFolder, "node_modules"); } }
        #endregion

        #region Server Settings
        /// <summary>android activity</summary>
        public string AndroidActivity { get { return this._View.AndroidActivityCheckbox.Text; } set { this._View.Invoke(new Action(() => this._View.AndroidActivityCheckbox.Text = value)); } }

        /// <summary>android package</summary>
        public string AndroidPackage { get { return this._View.AndroidPackageTextBox.Text; } set { this._View.Invoke(new Action(() => this._View.AndroidPackageTextBox.Text = value)); } }

        /// <summary>android activity to wait for</summary>
        public string AndroidWaitActivity { get { return this._View.WaitForAndroidActivityTextBox.Text; } set { this._View.Invoke(new Action(() => this._View.WaitForAndroidActivityTextBox.Text = value)); } }

        /// <summary>avd to launch</summary>
        public string AVDToLaunch
        {
            get { return this._View.LaunchAVDComboBox.SelectedItem.ToString(); }
            set
            {
                this._View.LaunchAVDComboBox.Text = value;
                try
                {
                    foreach (var item in this._View.LaunchAVDComboBox.Items)
                    {
                        if (item.ToString() == value)
                        {
                            this._View.Invoke(new Action(() => this._View.LaunchAVDComboBox.SelectedItem = item));
                            break;
                        }
                    }
                }
                catch { }
            }
        }

        /// <summary>list of available avds</summary>
        public string[] AVDs
        {
            get
            {
                List<string> avds = new List<string>();
                foreach (var item in this._View.LaunchAVDComboBox.Items)
                    avds.Add(item.ToString());
                return avds.ToArray();
            }
            set
            {
                this._View.Invoke(new Action(() =>
                {
                    this._View.LaunchAVDComboBox.Items.Clear();
                    this._View.LaunchAVDComboBox.Items.AddRange(value);
                }));
            }
        }

        /// <summary>path to the application</summary>
        public string ApplicationPath { get { return this._View.ApplicationPathTextBox.Text; } set { this._View.Invoke(new Action(() => this._View.ApplicationPathTextBox.Text = value)); } }

        /// <summary>ip address to listen on</summary>
        public string IPAddress { get { return this._View.IPAddressTextBox.Text; } set { this._View.Invoke(new Action(() => this._View.IPAddressTextBox.Text = value)); } }

        /// <summary>true if an AVD will be launched</summary>
        public bool LaunchAVD { get { return this._View.LaunchAVDCheckbox.Checked; } set { this._View.Invoke(new Action(() => this._View.LaunchAVDCheckbox.Checked = value)); } }

        /// <summary>sets whether the launch button is enabled</summary>
        public bool LaunchButtonEnabled { get { return this._View.LaunchButton.Enabled; } set { this._View.Invoke(new Action(() => this._View.LaunchButton.Enabled = value)); } }

        /// <summary>text displayed on the launch button</summary>
        public string LaunchButtonText { get { return this._View.LaunchButton.Text; } set { this._View.Invoke(new Action(() => this._View.LaunchButton.Text = value)); } }

        /// <summary>port to listen on</summary>
        public uint Port { get { return Convert.ToUInt32(this._View.PortTextBox.Value); } set { this._View.Invoke(new Action(() => this._View.PortTextBox.Value = value)); } }

        /// <summary>true if an android activity is supplied</summary>
        public bool UseAndroidActivity { get { return this._View.AndroidActivityCheckbox.Checked; } set { this._View.Invoke(new Action(() => this._View.AndroidActivityCheckbox.Checked = value)); } }

        /// <summary>true if an android package is supplied</summary>
        public bool UseAndroidPackage { get { return this._View.AndroidPackageCheckbox.Checked; } set { this._View.Invoke(new Action(() => this._View.AndroidPackageCheckbox.Checked = value)); } }

        /// <summary>true if an android wait activity is supplied</summary>
        public bool UseAndroidWaitActivity { get { return this._View.WaitForAndroidActivityCheckbox.Checked; } set { this._View.Invoke(new Action(() => this._View.WaitForAndroidActivityCheckbox.Checked = value)); } }

        /// <summary>true if an application path will be used</summary>
        public bool UseApplicationPath { get { return this._View.ApplicationPathCheckbox.Checked; } set { this._View.Invoke(new Action(() => this._View.ApplicationPathCheckbox.Checked = value)); } }

        /// <summary>true if a remote server will be used</summary>
        public bool UseRemoteServer { get { return this._View.UseRemoteServerCheckbox.Checked; } set { this._View.Invoke(new Action(() => this._View.UseRemoteServerCheckbox.Checked = value)); } }

        /// <summary>status bar text</summary>
        public string StatusBarText { get { return this._View.StatusBarText.Text; } set { this._View.Invoke(new Action(() => this._View.StatusBarText.Text = value)); } }
        #endregion
    }
}
