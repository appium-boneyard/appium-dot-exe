using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Appium
{
    public partial class MainForm : Form
    {
        /// <summary>process for the appium server</summary>
        public Process AppiumServerProcess;
        
        /// <summary>thread that runs setup actions after the form loads</summary>
        private Thread _LoadActionsThread;
        
        /// <summary>thread that monitors if the server is still running</summary>
        private Thread _ServerExitMonitorThread;

        #region Paths
        /// <summary>path to android sdk</summary>
        public static string AndroidSDKPath = Environment.GetEnvironmentVariable("ANDROID_HOME");

        /// <summary>lazy appium folder path</summary>
        private static Lazy<string> _AppiumRootFolder = new Lazy<string>(() => Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof(MainForm)).Location));
        
        /// <summary>appium folder path</summary>
        public static string AppiumRootFolder { get { return _AppiumRootFolder.Value; } }
        
        /// <summary>path to the appium package folder</summary>
        public static string AppiumPackageFolder { get { return Path.Combine(NodeModulesFolder, "appium"); } }
        
        /// <summary>path to node.exe</summary>
        public static string NodePath { get { return Path.Combine(AppiumRootFolder, "node.exe"); } }
        
        /// <summary>path to node package manager</summary>
        public static string NPMPath { get { return Path.Combine(AppiumRootFolder, "npm.cmd"); } }
        
        /// <summary>path to the node modules folder</summary>
        public static string NodeModulesFolder { get { return Path.Combine(AppiumRootFolder, "node_modules"); } }
        #endregion

        #region Server Settings
        /// <summary>android activity</summary>
        public string AndroidActivity { get { return this.AndroidActivityCheckbox.Text; } set { this.AndroidActivityCheckbox.Text = value; } }
        
        /// <summary>android package</summary>
        public string AndroidPackage { get { return this.AndroidPackageTextBox.Text; } set { this.AndroidPackageTextBox.Text = value; } }
        
        /// <summary>android activity to wait for</summary>
        public string AndroidWaitActivity { get { return this.WaitForAndroidActivityTextBox.Text; } set { this.WaitForAndroidActivityTextBox.Text = value; } }
        
        /// <summary>avd to launch</summary>
        public string AVDToLaunch { get { return this.LaunchAVDComboBox.SelectedItem.ToString(); }
            set
            { 
                this.LaunchAVDComboBox.Text = value;
                try
                {
                    foreach (var item in this.LaunchAVDComboBox.Items)
                    {
                        if (item.ToString() == value)
                        {
                            this.LaunchAVDComboBox.SelectedItem = item;
                            break;
                        }
                    }
                }
                catch {}
            }
        }

        /// <summary>list of available avds</summary>
        public string[] AVDs
        {
            get
            {
                List<string> avds = new List<string>();
                foreach (var item in this.LaunchAVDComboBox.Items)
                    avds.Add(item.ToString());
                return avds.ToArray();
            }
            set
            {
                this.LaunchAVDComboBox.Items.Clear();
                this.LaunchAVDComboBox.Items.AddRange(value);
            }
        }
        
        /// <summary>path to the application</summary>
        public string ApplicationPath { get { return this.ApplicationPathTextBox.Text; } set { this.ApplicationPathTextBox.Text = value; } }
        
        /// <summary>ip address to listen on</summary>
        public string IPAddress { get { return this.IPAddressTextBox.Text; } set { this.IPAddressTextBox.Text = value; } }
        
        /// <summary>true if an AVD will be launched</summary>
        public bool LaunchAVD { get { return this.LaunchAVDCheckbox.Checked; } set { this.LaunchAVDCheckbox.Checked = value; } }
        
        /// <summary>text displayed on the launch button</summary>
        public string LaunchButtonText { get { return this.LaunchButton.Text; } set { this.LaunchButton.Text = value; } }
        
        /// <summary>port to listen on</summary>
        public uint Port { get { return Convert.ToUInt32(this.PortTextBox.Value); } set { this.PortTextBox.Value = value; } }
        
        /// <summary>true if an android activity is supplied</summary>
        public bool UseAndroidActivity { get { return this.AndroidActivityCheckbox.Checked; } set { this.AndroidActivityCheckbox.Checked = value; } }
        
        /// <summary>true if an android package is supplied</summary>
        public bool UseAndroidPackage { get { return this.AndroidPackageCheckbox.Checked; } set { this.AndroidPackageCheckbox.Checked = value; } }
        
        /// <summary>true if an android wait activity is supplied</summary>
        public bool UseAndroidWaitActivity { get { return this.WaitForAndroidActivityCheckbox.Checked; } set { this.WaitForAndroidActivityCheckbox.Checked = value; } }
        
        /// <summary>true if an application path will be used</summary>
        public bool UseApplicationPath { get { return this.ApplicationPathCheckbox.Checked; } set { this.ApplicationPathCheckbox.Checked = value; } }
        
        /// <summary>true if a remote server will be used</summary>
        public bool UseRemoteServer { get { return this.UseRemoteServerCheckbox.Checked; } set { this.UseRemoteServerCheckbox.Checked = value; } }
        #endregion
    }
}
