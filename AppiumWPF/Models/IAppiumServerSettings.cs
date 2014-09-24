
using OpenQA.Selenium;

namespace Appium.Models
{
    public interface IAppiumServerSettings
    {
        #region Core Server Settings Properties

        /// <summary>path to the application</summary>
        string ApplicationPath { get; set; }

        /// <summary>android activity</summary>
        string AndroidActivity { get; set; }

        /// <summary>android activity</summary>
        uint AndroidDeviceReadyTimeout { get; set; }

        /// <summary>android package</summary>
        string AndroidPackage { get; set; }

        /// <summary>android activity to wait for</summary>
        string AndroidWaitForActivity { get; set; }

        /// <summary>avd to launch</summary>
        string AVDToLaunch { get; set; }

        /// <summary>name of the android browser</summary>
        bool UseAndroidBrowser { get; set; }

        /// <summary>true if the given android browser should be used</summary>
        string AndroidBrowser { get; set; }

        /// <summary>use android intent action</summary>
        bool UseAndroidIntentAction { get; set; }

        /// <summary>android intent action</summary>
        string AndroidIntentAction { get; set; }

        /// <summary>use android intent category</summary>
        bool UseAndroidIntentCategory { get; set; }

        /// <summary>android intent category</summary>
        string AndroidIntentCategory { get; set; }

        /// <summary>use android intent flags</summary>
        bool UseAndroidIntentFlags { get; set; }

        /// <summary>android intent flags</summary>
        string AndroidIntentFlags { get; set; }

        /// <summary>use android intent arguments</summary>
        bool UseAndroidIntentArguments { get; set; }

        /// <summary>android intent arguments</summary>
        string AndroidIntentArguments { get; set; }

        /// <summary>ip address to listen on</summary>
        string IPAddress { get; set; }

        /// <summary>true if an AVD will be launched</summary>
        bool LaunchAVD { get; set; }

        /// <summary>true if a full android reset will be performed</summary>
        bool PerformFullAndroidReset { get; set; }

        /// <summary>false to not reset app state between sessions</summary>
        bool NoReset { get; set; }

        /// <summary>port to listen on</summary>
        uint Port { get; set; }

        /// <summary>true if an android activity is supplied</summary>
        bool UseAndroidActivity { get; set; }

        /// <summary>true if the android device ready timeout will be used</summary>
        bool UseAndroidDeviceReadyTimeout { get; set; }

        /// <summary>true if an android package is supplied</summary>
        bool UseAndroidPackage { get; set; }

        /// <summary>true if an android wait activity is supplied</summary>
        bool UseAndroidWaitForActivity { get; set; }

        /// <summary>Android Only - package name for the Android activity you want to wait for</summary>
        string AndroidWaitForPackage { get; set; }

        /// <summarytrue to use the android wait for package </summary>
        bool UseAndroidWaitForPackage { get; set; }

        /// <summary>true if an application path will be used</summary>
        bool UseApplicationPath { get; set; }

        /// <summary>true if a remote server will be used</summary>
        bool UseRemoteServer { get; set; }

        bool UseAVDLaunchArguments { get; set; }

        string AVDLaunchArguments { get; set;  }

        bool UseSDKPath { get; set; }

        string SDKPath { get; set; }

        bool UseCoverageClass { get; set; }

        string CoverageClass { get; set; }

        bool UseBootstrapPort { get; set; }

        uint BootstrapPort { get; set; }

        bool UseSelendroidPort { get; set; }

        uint SelendroidPort { get; set; }

        bool UseChromeDriverPort { get; set; }

        uint ChromeDriverPort { get; set; }

        bool ShowTimestamps { get; set; }

        bool UseLogToFile { get; set; }

        string LogToFile { get; set; }

        bool UseLogToWebHook { get; set; }

        string LogToWebHook { get; set; }

        /// <summary>Use the Local Timezone</summary>
        bool UseLocalTimezone { get; set; }

        bool OverrideExistingSessions { get; set; }
        
        bool KillProcessUsingServerPortBeforeLaunch { get; set; }

        bool UseGridSeleniumConfigFile { get; set; }
        
        string GridSeleniumConfigFile { get; set; }

        bool UseCustomServerFlags { get; set; }

        string CustomServerFlags { get; set; }

        #region Capabilities Section
        string PlatformName { get; set; }

        string AutomationName { get; set; }

        string PlatformVersion { get; set; }

        bool UseDeviceName { get; set; }

        string DeviceName { get; set; }

        bool UseLanguage { get; set; }

        string Language { get; set; }

        bool UseLocale { get; set; }

        string Locale { get; set; }
        #endregion Capabilities Section


        #endregion

        #region Preferences Properties

        /// <summary>true if the nodejs debugger will break on application start</summary>
        bool BreakOnApplicationStart { get; set; }

        /// <summary>true if appium should check for updates</summary>
        bool CheckForUpdates { get; set; }

        /// <summary>path to external node.exe</summary>
        string ExternalNodeJSBinary { get; set; }

        /// <summary>pack to external appium node js package</summary>
        string ExternalAppiumPackage { get; set; }

        /// <summary>port on which the nodejs debugger will run</summary>
        uint NodeJSDebugPort { get; set; }

        /// <summary>true if appium should prelaunch the application</summary>
        bool PrelaunchApplication { get; set; }

        /// <summary>true if quiet logging should be used</summary>
        bool QuietLogging { get; set; }

        /// <summary>true if application state should be reset between sessions</summary>
        bool ResetApplicationState { get; set; }

        /// <summary>true if developer mode is enabled</summary>
        bool UseDeveloperMode { get; set; }

        /// <summary>true if an external node js binary will be used</summary>
        bool UseExternalNodeJSBinary { get; set; }

        /// <summary>true if an external appium package will be used</summary>
        bool UseExternalAppiumPackage { get; set; }

        /// <summary>true if nodejs debugging will be used</summary>
        bool UseNodeJSDebugging { get; set; }

        #endregion

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
