using System;
using System.Collections.Generic;
using Appium.Models;
using Appium.PreferencesWindow;

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
		//TODO this is here just to test currently refactored functionality in steps, in the future, we should not allow direct access to settings
		public IAppiumServerSettings Settings { get { return _Settings; } }

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

		/// <summary>
		/// Invokes saving of all the settings to the underlying datastore
		/// </summary>
		public void SaveSettings()
		{
			_Settings.Save();
		}

		public void OpenPreferences()
		{
			PreferencesPModel preferences = new PreferencesPModel(_Settings, new PreferencesForm());
			preferences.OpenWindow();
		}
	}
}
