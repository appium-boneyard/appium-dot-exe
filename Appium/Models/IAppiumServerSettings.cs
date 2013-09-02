using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appium.Models
{
	public interface IAppiumServerSettings
	{
		/// <summary>path to the application</summary>
		string ApplicationPath { get; set; }

		/// <summary>android activity</summary>
		string AndroidActivity { get; set; }

		/// <summary>android activity</summary>
		uint AndroidDeviceReadyTimeout { get; set; }

		/// <summary>android package</summary>
		string AndroidPackage { get; set; }

		/// <summary>android activity to wait for</summary>
		string AndroidWaitActivity { get; set; }

		/// <summary>avd to launch</summary>
		string AVDToLaunch { get; set; }

		/// <summary>ip address to listen on</summary>
		string IPAddress { get; set; }

		/// <summary>true if an AVD will be launched</summary>
		bool LaunchAVD { get; set; }

		/// <summary>true if a full android reset will be performed</summary>
		bool PerformFullAndroidReset { get; set; }

		/// <summary>port to listen on</summary>
		uint Port { get; set; }

		/// <summary>true if an android activity is supplied</summary>
		bool UseAndroidActivity { get; set; }

		/// <summary>true if the android device ready timeout will be used</summary>
		bool UseAndroidDeviceReadyTimeout { get; set; }

		/// <summary>true if an android package is supplied</summary>
		bool UseAndroidPackage { get; set; }

		/// <summary>true if an android wait activity is supplied</summary>
		bool UseAndroidWaitActivity { get; set; }

		/// <summary>true if an application path will be used</summary>
		bool UseApplicationPath { get; set; }

		/// <summary>true if a remote server will be used</summary>
		bool UseRemoteServer { get; set; }

		/// <summary>
		/// Saves settings to underlying data store
		/// </summary>
		void Save();

		/// <summary>
		/// Loads settings from underlying data store
		/// </summary>
		void Load();
	}
}
