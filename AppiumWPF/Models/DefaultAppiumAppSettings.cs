//using AutoMapper;
using Appium.Models.Capability;
using AutoMapper;

namespace Appium.Models
{
    public class DefaultAppiumAppSettings : IAppiumAppSettings
    {
        public string ApplicationPath { get; set; }

        public string AndroidActivity { get; set; }

        public uint AndroidDeviceReadyTimeout { get; set; }

        public string AndroidPackage { get; set; }

        public string AndroidWaitForActivity { get; set; }

        public string AndroidWaitForPackage { get; set; }

        public string AVDToLaunch { get; set; }

        public string IPAddress { get; set; }

        public bool LaunchAVD { get; set; }

        public bool PerformFullAndroidReset { get; set; }

        public bool NoReset { get; set; }

        public uint Port { get; set; }

        public bool UseAndroidActivity { get; set; }

        public bool UseAndroidDeviceReadyTimeout { get; set; }

        public bool UseAndroidPackage { get; set; }

        public bool UseAndroidWaitForActivity { get; set; }

        public bool UseAndroidWaitForPackage { get; set; }

        public bool UseApplicationPath { get; set; }

        public bool UseRemoteServer { get; set; }

        public bool BreakOnApplicationStart { get; set; }

        public bool CheckForUpdates { get; set; }

        public string ExternalNodeJSBinary { get; set; }

        public string ExternalAppiumPackage { get; set; }

        public uint NodeJSDebugPort { get; set; }

        public bool PrelaunchApplication { get; set; }

        public bool QuietLogging { get; set; }

        public bool ResetApplicationState { get; set; }

        public bool UseAndroidBrowser { get; set; }

        public string AndroidBrowser { get; set; }

        public bool UseAndroidIntentAction { get; set; }

        public string AndroidIntentAction { get; set; }

        public bool UseAndroidIntentCategory { get; set; }

        public string AndroidIntentCategory { get; set; }

        public bool UseAndroidIntentFlags { get; set; }

        public string AndroidIntentFlags { get; set; }

        public bool UseAndroidIntentArguments { get; set; }

        public string AndroidIntentArguments { get; set; }

        public bool UseDeveloperMode { get; set; }

        public bool UseExternalNodeJSBinary { get; set; }

        public bool UseExternalAppiumPackage { get; set; }

        public bool UseNodeJSDebugging { get; set; }

        public bool UseAVDLaunchArguments { get; set; }

        public string AVDLaunchArguments { get; set; }

        public bool UseSDKPath { get; set; }

        public string SDKPath { get; set; }

        public bool UseCoverageClass { get; set; }

        public string CoverageClass { get; set; }

        public bool UseBootstrapPort { get; set; }

        public uint BootstrapPort { get; set; }

        public bool UseSelendroidPort { get; set; }

        public uint SelendroidPort { get; set; }

        public bool UseChromeDriverPort { get; set; }

        public uint ChromeDriverPort { get; set; }

        public bool ShowTimestamps { get; set; }

        public bool UseLogToFile { get; set; }

        public string LogToFile { get; set; }

        public bool UseLogToWebHook { get; set; }

        public string LogToWebHook { get; set; }

        public bool UseLocalTimezone { get; set; }

        public bool OverrideExistingSessions { get; set; }

        public bool KillProcessUsingServerPortBeforeLaunch { get; set; }

        public bool UseGridSeleniumConfigFile { get; set; }

        public string GridSeleniumConfigFile { get; set; }

        public bool UseCustomServerFlags { get; set; }

        public string CustomServerFlags { get; set; }

        #region Capabilities Section
        public string PlatformName { get; set; }

        public string AutomationName { get; set; }

        public string PlatformVersion { get; set; }

        public bool UseDeviceName { get; set; }

        public string DeviceName { get; set; }

        public bool UseLanguage { get; set; }

        public string Language { get; set; }

        public bool UseLocale { get; set; }

        public string Locale { get; set; }
        #endregion Capabilities Section

        public Device InspectorDeviceCapability { get; set; }

        /// <summary>
        /// Saves Appium Server settings into default settings file
        /// </summary>
        public void Save()
        {
            Mapper.Map<IAppiumAppSettings, Appium.Properties.Settings>(this, Appium.Properties.Settings.Default);
            Appium.Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Loads Appium Server settings from default settings file
        /// </summary>
        public void Load()
        {
            Mapper.Map<Appium.Properties.Settings, IAppiumAppSettings>(Appium.Properties.Settings.Default, this);
        }
    }
}
