using System;
using System.Collections.Generic;

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

        #region Server Settings
        /// <summary>android activity</summary>
        public string AndroidActivity
        {
            get { return this._View.AndroidActivityCheckbox.Text; }
            set
            {
                Appium.Properties.Settings.Default.AndroidActivity = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

        /// <summary>android package</summary>
        public string AndroidPackage
        {
            get { return this._View.AndroidPackageTextBox.Text; }
            set
            {
                Appium.Properties.Settings.Default.AndroidPackage = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

        /// <summary>android activity to wait for</summary>
        public string AndroidWaitActivity
        {
            get { return this._View.WaitForAndroidActivityTextBox.Text; }
            set
            {
                Appium.Properties.Settings.Default.AndroidWaitActivity = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

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
                            Appium.Properties.Settings.Default.AVD = value;
                            Appium.Properties.Settings.Default.Save();
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
        public string ApplicationPath
        {
            get { return this._View.ApplicationPathTextBox.Text; }
            set
            {
                Appium.Properties.Settings.Default.ApplicationPath = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

        /// <summary>ip address to listen on</summary>
        public string IPAddress
        {
            get { return this._View.IPAddressTextBox.Text; }
            set
            {
                Appium.Properties.Settings.Default.ServerAddress = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

        /// <summary>true if an AVD will be launched</summary>
        public bool LaunchAVD
        {
            get { return this._View.LaunchAVDCheckbox.Checked; }
            set
            {
                Appium.Properties.Settings.Default.LaunchAVD = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

        /// <summary>sets whether the launch button is enabled</summary>
        public bool LaunchButtonEnabled
        {
            get { return this._View.LaunchButton.Enabled; }
            set { this._View.Invoke(new Action(() => this._View.LaunchButton.Enabled = value)); }
        }

        /// <summary>text displayed on the launch button</summary>
        public string LaunchButtonText
        {
            get { return this._View.LaunchButton.Text; }
            set { this._View.Invoke(new Action(() => this._View.LaunchButton.Text = value)); }
        }

        /// <summary>port to listen on</summary>
        public uint Port
        {
            get { return Convert.ToUInt32(this._View.PortTextBox.Value); }
            set
            {
                Appium.Properties.Settings.Default.ServerPort = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

        /// <summary>true if an android activity is supplied</summary>
        public bool UseAndroidActivity
        {
            get { return this._View.AndroidActivityCheckbox.Checked; }
            set
            {
                Appium.Properties.Settings.Default.UseAndroidActivity = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

        /// <summary>true if an android package is supplied</summary>
        public bool UseAndroidPackage
        {
            get { return this._View.AndroidPackageCheckbox.Checked; }
            set
            {
                Appium.Properties.Settings.Default.UseAndroidPackage = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

        /// <summary>true if an android wait activity is supplied</summary>
        public bool UseAndroidWaitActivity
        {
            get { return this._View.WaitForAndroidActivityCheckbox.Checked; }
            set
            {
                Appium.Properties.Settings.Default.UseAndroidWaitActivity = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

        /// <summary>true if an application path will be used</summary>
        public bool UseApplicationPath
        {
            get { return this._View.ApplicationPathCheckbox.Checked; }
            set
            {
                Appium.Properties.Settings.Default.UseApplicationPath = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

        /// <summary>true if a remote server will be used</summary>
        public bool UseRemoteServer
        {
            get { return this._View.UseRemoteServerCheckbox.Checked; }
            set
            {
                Appium.Properties.Settings.Default.UseRemoteServer = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

        /// <summary>status bar text</summary>
        public string StatusBarText { get { return this._View.StatusBarText.Text; } set { this._View.Invoke(new Action(() => this._View.StatusBarText.Text = value)); } }
        #endregion

        #region Preferences
        /// <summary>true if the nodejs debugger will break on application start</summary>
        public bool BreakOnApplicationStart
        {
            get { return Appium.Properties.Settings.Default.BreakOnApplicationStart; }
            set
            {
                Appium.Properties.Settings.Default.BreakOnApplicationStart = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

        /// <summary>true if appium should check for updates</summary>
        public bool CheckForUpdates
        {
            get { return Appium.Properties.Settings.Default.CheckForUpdates; }
            set
            {
                Appium.Properties.Settings.Default.CheckForUpdates = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

        /// <summary>true if developer mode is enabled</summary>
        public bool DeveloperMode
        {
            get { return Appium.Properties.Settings.Default.DeveloperMode; }
            set
            {
                Appium.Properties.Settings.Default.DeveloperMode = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

        /// <summary>path to external node.exe</summary>
        public string ExternalNodeJSBinary
        {
            get { return Appium.Properties.Settings.Default.ExternalNodeJSBinary; }
            set
            {
                Appium.Properties.Settings.Default.ExternalNodeJSBinary = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

        /// <summary>pack to external appium node js package</summary>
        public string ExternalAppiumPackage
        {
            get { return Appium.Properties.Settings.Default.ExternalAppiumPackage; }
            set
            {
                Appium.Properties.Settings.Default.ExternalAppiumPackage = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

        /// <summary>true if artifacts will be kept after a session</summary>
        public bool KeepArtifacts
        {
            get { return Appium.Properties.Settings.Default.KeepArtifacts; }
            set
            {
                Appium.Properties.Settings.Default.KeepArtifacts = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

        /// <summary>port on which the nodejs debugger will run</summary>
        public uint NodeJSDebugPort
        {
            get { return Convert.ToUInt32(Appium.Properties.Settings.Default.NodeJSDebugPort); }
            set
            {
                Appium.Properties.Settings.Default.NodeJSDebugPort = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

        /// <summary>true if appium should prelaunch the application</summary>
        public bool PrelaunchApplication
        {
            get { return Appium.Properties.Settings.Default.PrelaunchApplication; }
            set
            {
                Appium.Properties.Settings.Default.PrelaunchApplication = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

        /// <summary>true if quiet logging should be used</summary>
        public bool QuietLogging
        {
            get { return Appium.Properties.Settings.Default.QuietLogging; }
            set
            {
                Appium.Properties.Settings.Default.QuietLogging = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

        /// <summary>true if application state should be reset between sessions</summary>
        public bool ResetApplicationState
        {
            get { return Appium.Properties.Settings.Default.ResetApplicationState; }
            set
            {
                Appium.Properties.Settings.Default.ResetApplicationState = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

        /// <summary>true if an external node js binary will be used</summary>
        public bool UseExternalNodeJSBinary
        {
            get { return Appium.Properties.Settings.Default.UseExternalNodeJSBinary; }
            set
            {
                Appium.Properties.Settings.Default.UseExternalNodeJSBinary = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

        /// <summary>true if an external appium package will be used</summary>
        public bool UseExternalAppiumPackage
        {
            get { return Appium.Properties.Settings.Default.UseExternalAppiumPackage; }
            set
            {
                Appium.Properties.Settings.Default.UseExternalAppiumPackage = value;
                Appium.Properties.Settings.Default.Save();
            }
        }

        /// <summary>true if nodejs debugging will be used</summary>
        public bool UseNodeJSDebugging
        {
            get { return Appium.Properties.Settings.Default.UseNodeJSDebugging; }
            set
            {
                Appium.Properties.Settings.Default.UseNodeJSDebugging = value;
                Appium.Properties.Settings.Default.Save();
            }
        }
        #endregion
    }
}
