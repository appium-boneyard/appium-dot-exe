using System;

namespace Appium.Models
{
	public class DefaultAppiumServerSettings : IAppiumServerSettings
	{
		public string ApplicationPath { get; set; }

		public string AndroidActivity { get; set; }

		public uint AndroidDeviceReadyTimeout { get; set; }

		public string AndroidPackage { get; set; }

		public string AndroidWaitActivity { get; set; }

		public string AVDToLaunch { get; set; }

		public string IPAddress { get; set; }

		public bool LaunchAVD { get; set; }

		public bool PerformFullAndroidReset { get; set; }

		public uint Port { get; set; }

		public bool UseAndroidActivity { get; set; }

		public bool UseAndroidDeviceReadyTimeout { get; set; }

		public bool UseAndroidPackage { get; set; }

		public bool UseAndroidWaitActivity { get; set; }

		public bool UseApplicationPath { get; set; }

		public bool UseRemoteServer { get; set; }

		/// <summary>
		/// Saves Appium Server settings into default settings file
		/// </summary>
		public void Save()
		{
			Appium.Properties.Settings.Default.ServerAddress = IPAddress;
			Appium.Properties.Settings.Default.ServerPort = Port;
			Appium.Properties.Settings.Default.UseRemoteServer = UseRemoteServer;
			Appium.Properties.Settings.Default.LaunchAVD = LaunchAVD;

			Appium.Properties.Settings.Default.Save();
		}

		/// <summary>
		/// Loads Appium Server settings from default settings file
		/// </summary>
		public void Load()
		{
			// use automapper here?

			ApplicationPath = Appium.Properties.Settings.Default.ApplicationPath;
			AndroidActivity = Appium.Properties.Settings.Default.AndroidActivity;

			AndroidDeviceReadyTimeout = Appium.Properties.Settings.Default.AndroidDeviceReadyTimeout;

			AndroidPackage = Appium.Properties.Settings.Default.AndroidPackage;

			AndroidWaitActivity = Appium.Properties.Settings.Default.AndroidWaitActivity;
			AVDToLaunch = Appium.Properties.Settings.Default.AVD;
			LaunchAVD = Appium.Properties.Settings.Default.LaunchAVD;
			PerformFullAndroidReset = Appium.Properties.Settings.Default.PerformFullAndroidReset;
			UseAndroidActivity = Appium.Properties.Settings.Default.UseAndroidActivity;
			UseAndroidDeviceReadyTimeout = Appium.Properties.Settings.Default.UseAndroidDeviceReadyTimeout;
			UseAndroidPackage = Appium.Properties.Settings.Default.UseAndroidPackage;
			UseAndroidWaitActivity = Appium.Properties.Settings.Default.UseAndroidWaitActivity;
			UseApplicationPath = Appium.Properties.Settings.Default.UseApplicationPath;


			IPAddress = Appium.Properties.Settings.Default.ServerAddress;
			Port = Appium.Properties.Settings.Default.ServerPort;
			UseRemoteServer = Appium.Properties.Settings.Default.UseRemoteServer;
			LaunchAVD = Appium.Properties.Settings.Default.LaunchAVD;
		}



	}
}
