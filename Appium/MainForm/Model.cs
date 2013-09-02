using System;
using System.Collections.Generic;
using Appium.Models;

namespace Appium.MainWindow
{
	public class Model
	{
		/// <summary>constructor</summary>
		/// <param name="form">main form</param>
		public Model(MainForm form, IAppiumServerSettings settings)
		{
			this._View = form;
			_Settings = settings;
			// load settings
			_Settings.Load();
		}

		private IAppiumServerSettings _Settings;

		/// <summary>main form</summary>
		public MainForm _View;

		#region Server Settings
		/// <summary>android activity</summary>
		public string AndroidActivity
		{
			get { return _Settings.AndroidActivity; }
			set 
			{ 
				_Settings.AndroidActivity = value;
				// autocheck UseAndroidActivity checkbox
				UseAndroidActivity = !String.IsNullOrEmpty(value); 
			}
		}

		/// <summary>android activity</summary>
		public uint AndroidDeviceReadyTimeout
		{
			get { return _Settings.AndroidDeviceReadyTimeout; }
			set { _Settings.AndroidDeviceReadyTimeout = value; }
		}

		/// <summary>android package</summary>
		public string AndroidPackage
		{
			get { return _Settings.AndroidPackage; }
			set	
			{ 
				_Settings.AndroidPackage = value; 
				// automatically check UseAndroidPackage checkbox
				UseAndroidPackage = !String.IsNullOrEmpty(value); 	
			}
		}

		/// <summary>android activity to wait for</summary>
		public string AndroidWaitActivity
		{
			get { return _Settings.AndroidWaitActivity; }
			set 
			{ 
				_Settings.AndroidWaitActivity = value; 
				// automatically check UseAndroidWaitActivity checkbox
				UseAndroidWaitActivity = !String.IsNullOrEmpty(value);
			}
		}

		/// <summary>avd to launch</summary>
		public string AVDToLaunch
		{
			get { return _Settings.AVDToLaunch; }
			set 
			{
				_Settings.AVDToLaunch = value;
				// check launch AVD checkbox automatically when value selected.
				LaunchAVD = !String.IsNullOrEmpty(value);
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
			get { return _Settings.ApplicationPath; }
			set { _Settings.ApplicationPath = value; }
		}

		/// <summary>ip address to listen on</summary>
		public string IPAddress
		{
			get { return _Settings.IPAddress; }
			set { _Settings.IPAddress = value; }
		}

		/// <summary>true if an AVD will be launched</summary>
		public bool LaunchAVD
		{
			get { return _Settings.LaunchAVD; }
			set { _Settings.LaunchAVD = value; }
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

		/// <summary>true if a full android reset will be performed</summary>
		public bool PerformFullAndroidReset
		{
			get { return _Settings.PerformFullAndroidReset; }
			set { _Settings.PerformFullAndroidReset = value; }
		}

		/// <summary>port to listen on</summary>
		public uint Port
		{
			get { return _Settings.Port; }
			set { _Settings.Port = value; }
		}

		/// <summary>true if an android activity is supplied</summary>
		public bool UseAndroidActivity
		{
			get { return _Settings.UseAndroidActivity; }
			set { _Settings.UseAndroidActivity = value; }
		}

		/// <summary>true if the android device ready timeout will be used</summary>
		public bool UseAndroidDeviceReadyTimeout
		{
			get { return _Settings.UseAndroidDeviceReadyTimeout; }
			set { _Settings.UseAndroidDeviceReadyTimeout = value; }
		}

		/// <summary>true if an android package is supplied</summary>
		public bool UseAndroidPackage
		{
			get { return _Settings.UseAndroidPackage; }
			set { _Settings.UseAndroidPackage = value; }
		}

		/// <summary>true if an android wait activity is supplied</summary>
		public bool UseAndroidWaitActivity
		{
			get { return _Settings.UseAndroidWaitActivity; }
			set { _Settings.UseAndroidWaitActivity = value; }
		}

		/// <summary>true if an application path will be used</summary>
		public bool UseApplicationPath
		{
			get { return _Settings.UseApplicationPath; }
			set { _Settings.UseApplicationPath = value; }			
		}

		/// <summary>true if a remote server will be used</summary>
		public bool UseRemoteServer 
		{ 
			get { return _Settings.UseRemoteServer; } 
			set { _Settings.UseRemoteServer = value; }
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
