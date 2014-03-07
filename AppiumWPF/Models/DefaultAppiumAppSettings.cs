using System;
//using AutoMapper;
using AppiumWPF.Models.Capability;
using AutoMapper;

namespace AppiumWPF.Models
{
    public class DefaultAppiumAppSettings : IAppiumAppSettings
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

        public bool BreakOnApplicationStart { get; set; }

        public bool CheckForUpdates { get; set; }

        public string ExternalNodeJSBinary { get; set; }

        public string ExternalAppiumPackage { get; set; }

        public bool KeepArtifacts { get; set; }

        public uint NodeJSDebugPort { get; set; }

        public bool PrelaunchApplication { get; set; }

        public bool QuietLogging { get; set; }

        public bool ResetApplicationState { get; set; }

        public bool UseDeveloperMode { get; set; }

        public bool UseExternalNodeJSBinary { get; set; }

        public bool UseExternalAppiumPackage { get; set; }

        public bool UseNodeJSDebugging { get; set; }

        public Device InspectorDeviceCapability { get; set; }

        /// <summary>
        /// Saves Appium Server settings into default settings file
        /// </summary>
        public void Save()
        {
            Mapper.Map<IAppiumAppSettings, AppiumWPF.Properties.Settings>(this, AppiumWPF.Properties.Settings.Default);
            AppiumWPF.Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Loads Appium Server settings from default settings file
        /// </summary>
        public void Load()
        {
            Mapper.Map<AppiumWPF.Properties.Settings,IAppiumAppSettings>(AppiumWPF.Properties.Settings.Default,this);
        }
    }
}
