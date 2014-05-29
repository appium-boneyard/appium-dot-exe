using System.Collections.ObjectModel;
using System.Windows.Media;
using Appium.Models;
using Appium.Models.Capability;
using Appium.Utility;
using System.Collections.Generic;
using System.Windows.Input;

namespace Appium.ViewModels
{
    class AndroidSettingsVM : BaseVM
    {
        #region Private Member Variables
        /// <summary>Settings object</summary>
        private IAppiumAppSettings _Settings;
        #endregion Private Member Variables

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public AndroidSettingsVM(IAppiumAppSettings settings, List<string> avds)
        {
            _Settings = settings;
            LaunchAVDs = avds;
            if (null != LaunchAVDs && 0 < LaunchAVDs.Count)
            {
                SelectedLaunchAVD = LaunchAVDs[0];
            }

            PlatformNameList = Dictionaries.PlatformNameList;
            if (string.IsNullOrWhiteSpace(PlatformName))
            {
                PlatformName = PlatformNameList[0];
            }

            AutomationNameList = Dictionaries.AutomationNameList;
            if (string.IsNullOrWhiteSpace(AutomationName))
            {
                AutomationName = AutomationNameList[0];
            }

            PlatformVersionList = Dictionaries.PlatformVersionList;
            if (string.IsNullOrWhiteSpace(PlatformVersion))
            {
                PlatformVersion = PlatformVersionList[0];
            }

            LanguageList = Dictionaries.LanguageList;
            if (string.IsNullOrWhiteSpace(Language))
            {
                Language = LanguageList[0];
            }

            LocaleList = Dictionaries.LocaleList;
            if (string.IsNullOrWhiteSpace(Locale))
            {
                Locale = LocaleList[0];
            }

        }
        #endregion Constructor

        #region Public Properties

        #region Application Section

        #region Application Path
        /// <summary>
        /// Is the application path enabled (and will be used)
        /// </summary>
        public bool IsAppPathEnabled
        {
            get { return _Settings.UseApplicationPath; }
            set
            {
                if (value != _Settings.UseApplicationPath)
                {
                    _Settings.UseApplicationPath = value;
                    FirePropertyChanged(() => IsAppPathEnabled);
                }
            }
        }

        /// <summary>
        /// File Path to the application 
        /// </summary>
        public string FilePath
        {
            get { return _Settings.ApplicationPath; }
            set
            {
                if (value != _Settings.ApplicationPath)
                {
                    _Settings.ApplicationPath = value;
                    FirePropertyChanged(() => FilePath);
                }
            }
        }

        private ICommand _ChooseFileCommand;
        /// <summary>
        /// Open the File Dialog Command
        /// </summary>
        public ICommand ChooseFileCommand
        {
            get { return _ChooseFileCommand ?? (_ChooseFileCommand = new RelayCommand(() => _ExecuteOpenFileDialog(), () => _CanExecuteOpenFileDialog())); }
        }
        #endregion Application Path

        #region Package
        /// <summary>
        /// Use the application package flag (--app-pkg)
        /// </summary>
        public bool UsePackage
        {
            get { return _Settings.UseAndroidPackage; }
            set
            {
                if (value != _Settings.UseAndroidPackage)
                {
                    _Settings.UseAndroidPackage = value;
                    FirePropertyChanged(() => UsePackage);
                }
            }
        }

        /// <summary>
        /// Java package of the Android app you want to run (e.g., com.example.android.myApp)
        /// </summary>
        public string Package
        {
            get { return _Settings.AndroidPackage; }
            set
            {
                if (value != _Settings.AndroidPackage)
                {
                    _Settings.AndroidPackage = value;
                    FirePropertyChanged(() => Package);
                }
            }
        }
        #endregion Package

        #region Activity
        /// <summary>
        /// Use the android activity flag (--app-activity)
        /// </summary>
        public bool UseActivity
        {
            get { return _Settings.UseAndroidActivity; }
            set
            {
                if (value != _Settings.UseAndroidActivity)
                {
                    _Settings.UseAndroidActivity = value;
                    FirePropertyChanged(() => UseActivity);
                }
            }
        }

        /// <summary>
        /// Activity name for the Android activity you want to launch form your package (e.g., MainActivity)
        /// </summary>
        public string Activity
        {
            get { return _Settings.AndroidActivity; }
            set
            {
                if (value != _Settings.AndroidActivity)
                {
                    _Settings.AndroidActivity = value;
                    FirePropertyChanged(() => Activity);
                }
            }
        }
        #endregion Activity

        #region Wait For Activity
        /// <summary>
        ///  enable the application wait activity (--app-wait-activity [activity])
        /// </summary>
        public bool UseWaitForActivity
        {
            get { return _Settings.UseAndroidWaitForActivity; }
            set
            {
                if (value != _Settings.UseAndroidWaitForActivity)
                {
                    _Settings.UseAndroidWaitForActivity = value;
                    FirePropertyChanged(() => UseWaitForActivity);
                }
            }
        }

        /// <summary>
        /// Activity name for the Android activity you want to wait for (e.g. SplashActivity)
        /// </summary>
        public string WaitForActivity
        {
            get { return _Settings.AndroidWaitForActivity; }
            set
            {
                if (value != _Settings.AndroidWaitForActivity)
                {
                    _Settings.AndroidWaitForActivity = value;
                    FirePropertyChanged(() => WaitForActivity);
                }
            }
        }
        #endregion Wait For Activity

        #region Wait For Package
        /// <summary>
        ///  enable the application wait for package (--app-wait-package [activity])
        /// </summary>
        public bool UseWaitForPackage
        {
            get { return _Settings.UseAndroidWaitForPackage; }
            set
            {
                if (value != _Settings.UseAndroidWaitForPackage)
                {
                    _Settings.UseAndroidWaitForPackage = value;
                    FirePropertyChanged(() => UseWaitForPackage);
                }
            }
        }

        /// <summary>
        /// Package name for the Android package you want to wait for (e.g. com.example.android.myApp)
        /// </summary>
        public string WaitForPackage
        {
            get { return _Settings.AndroidWaitForPackage; }
            set
            {
                if (value != _Settings.AndroidWaitForPackage)
                {
                    _Settings.AndroidWaitForPackage = value;
                    FirePropertyChanged(() => WaitForPackage);
                }
            }
        }
        #endregion Wait For Package

        #region Reset
        /// <summary>
        /// Reset app state by un-installing app instead of clearing app data. This flag will also remove the app
        /// after the session is complete (--full-reset)
        /// </summary>
        public bool IsPerformFullReset
        {
            get { return _Settings.PerformFullAndroidReset; }
            set
            {
                if (value != _Settings.PerformFullAndroidReset)
                {
                    _Settings.PerformFullAndroidReset = value;
                    FirePropertyChanged(() => IsPerformFullReset);
                }
            }
        }

        /// <summary>
        /// Don't reset app state between sessions
        /// </summary>
        public bool NoReset
        {
            get { return _Settings.NoReset; }
            set
            {
                if (value != _Settings.NoReset)
                {
                    _Settings.NoReset = value;
                    FirePropertyChanged(() => NoReset);
                }
            }
        }
        #endregion Reset

        #endregion Application Section

        #region Launch Section

        #region Launch AVDs
        /// <summary>
        /// enable the avd flag (--avd) 
        /// </summary>
        public bool UseLaunchAVD
        {
            get { return _Settings.LaunchAVD; }
            set
            {
                if (value != _Settings.LaunchAVD)
                {
                    _Settings.LaunchAVD = value;
                    FirePropertyChanged(() => UseLaunchAVD);
                }
            }
        }

        /// <summary>
        /// List of all possible AVDs on this system
        /// </summary>
        public List<string> LaunchAVDs { get; private set; }

        /// <summary>
        /// name of the avd to launch
        /// </summary>
        public string SelectedLaunchAVD
        {
            get { return _Settings.AVDToLaunch; }
            set
            {
                if (value != _Settings.AVDToLaunch)
                {
                    _Settings.AVDToLaunch = value;
                    FirePropertyChanged(() => SelectedLaunchAVD);
                }
            }
        }
        #endregion Launch AVDs

        #region Device Ready
        /// <summary>
        /// Enable the device timeout flag (--device-ready-timeout [number])
        /// </summary>
        public bool UseDeviceReadyTimeout
        {
            get { return _Settings.UseAndroidDeviceReadyTimeout; }
            set
            {
                if (value != _Settings.UseAndroidDeviceReadyTimeout)
                {
                    _Settings.UseAndroidDeviceReadyTimeout = value;
                    FirePropertyChanged(() => UseDeviceReadyTimeout);
                }
            }
        }

        /// <summary>
        /// timeout in seconds while waiting for device to become ready
        /// </summary>
        public uint DeviceReadyTimeout
        {
            get { return _Settings.AndroidDeviceReadyTimeout; }
            set
            {
                if (value != _Settings.AndroidDeviceReadyTimeout)
                {
                    _Settings.AndroidDeviceReadyTimeout = value;
                    FirePropertyChanged(() => DeviceReadyTimeout);
                }
            }
        }
        #endregion Device Ready

        #region Launch Arguments
        public bool UseLaunchArguments
        {
            get { return _Settings.UseAVDLaunchArguments; }
            set
            {
                if (value != _Settings.UseAVDLaunchArguments)
                {
                    _Settings.UseAVDLaunchArguments = value;
                    FirePropertyChanged(() => UseLaunchArguments);
                }
            }
        }

        public string LaunchArguments
        {
            get { return _Settings.AVDLaunchArguments; }
            set
            {
                if (value != _Settings.AVDLaunchArguments)
                {
                    _Settings.AVDLaunchArguments = value;
                    FirePropertyChanged(() => LaunchArguments);
                }
            }
        }
        #endregion Launch Arguments

        #endregion Launch Section

        #region Capabilities Section

        #region Platform Name
        /// <summary>
        /// Platform Name
        /// </summary>
        public string PlatformName
        {
            get { return _Settings.PlatformName; }
            set
            {
                if (value != _Settings.PlatformName)
                {
                    _Settings.PlatformName = value;
                    FirePropertyChanged(() => PlatformName);
                }
            }
        }

        /// <summary>
        /// List of available Platform Names
        /// </summary>
        public ReadOnlyCollection<string> PlatformNameList { get; private set; }
        #endregion Platform Name

        #region Platform Version
        /// <summary>
        /// Platform Version
        /// </summary>
        public string PlatformVersion
        {
            get { return _Settings.PlatformVersion; }
            set
            {
                if (value != _Settings.PlatformVersion)
                {
                    _Settings.PlatformVersion = value;
                    FirePropertyChanged(() => PlatformVersion);
                }
            }
        }

        /// <summary>
        /// List of platform version
        /// </summary>
        public ReadOnlyCollection<string> PlatformVersionList { get; private set; }
        #endregion Platform Version

        #region Automation Name
        /// <summary>
        /// Automation Name
        /// </summary>
        public string AutomationName
        {
            get { return _Settings.AutomationName; }
            set
            {
                if (value != _Settings.AutomationName)
                {
                    _Settings.AutomationName = value;
                    FirePropertyChanged(() => AutomationName);
                }
            }
        }

        /// <summary>
        /// List of Automation Names
        /// </summary>
        public ReadOnlyCollection<string> AutomationNameList { get; private set; }
        #endregion Automation Name

        #region Device Name
        /// <summary>
        /// Use the Device Name
        /// </summary>
        public bool UseDeviceName
        {
            get { return _Settings.UseDeviceName; }
            set
            {
                if (value != _Settings.UseDeviceName)
                {
                    _Settings.UseDeviceName = value;
                    FirePropertyChanged(() => UseDeviceName);
                }
            }
        }

        /// <summary>
        /// Device Name
        /// </summary>
        public string DeviceName
        {
            get { return _Settings.DeviceName; }
            set
            {
                if (value != _Settings.DeviceName)
                {
                    _Settings.DeviceName = value;
                    FirePropertyChanged(() => DeviceName);
                }
            }
        }
        #endregion Device Name

        #region Language
        /// <summary>
        /// Use the given Language 
        /// </summary>
        public bool UseLanguage
        {
            get { return _Settings.UseLanguage; }
            set
            {
                if (value != _Settings.UseLanguage)
                {
                    _Settings.UseLanguage = value;
                    FirePropertyChanged(() => UseLanguage);
                }
            }
        }

        /// <summary>
        /// Language
        /// </summary>
        public string Language
        {
            get { return _Settings.Language; }
            set
            {
                if (value != _Settings.Language)
                {
                    _Settings.Language = value;
                    FirePropertyChanged(() => Language);
                }
            }
        }

        /// <summary>
        /// List of available Languages
        /// </summary>
        public ReadOnlyCollection<string> LanguageList { get; private set; }
        #endregion Language

        #region Locale
        /// <summary>
        /// Use the Locale 
        /// </summary>
        public bool UseLocale
        {
            get { return _Settings.UseLocale; }
            set
            {
                if (value != _Settings.UseLocale)
                {
                    _Settings.UseLocale = value;
                    FirePropertyChanged(() => UseLocale);
                }
            }
        }

        /// <summary>
        /// Locale
        /// </summary>
        public string Locale
        {
            get { return _Settings.Locale; }
            set
            {
                if (value != _Settings.Locale)
                {
                    _Settings.Locale = value;
                    FirePropertyChanged(() => Locale);
                }
            }
        }

        /// <summary>
        /// List of available locales
        /// </summary>
        public ReadOnlyCollection<string> LocaleList { get; private set; }
        #endregion Locale

        #endregion Capabilities Section

        #region Advanced Section

        #region SDK Path
        public bool UseSDKPath
        {
            get { return _Settings.UseSDKPath; }
            set
            {
                if (value != _Settings.UseSDKPath)
                {
                    _Settings.UseSDKPath = value;
                    FirePropertyChanged(() => UseSDKPath);
                }
            }
        }

        public string SDKPath
        {
            get { return _Settings.SDKPath; }
            set
            {
                if (value != _Settings.SDKPath)
                {
                    _Settings.SDKPath = value;
                    FirePropertyChanged(() => SDKPath);
                }
            }
        }
        #endregion SDK Path

        #region Coverage Class
        public bool UseCoverageClass
        {
            get { return _Settings.UseCoverageClass; }
            set
            {
                if (value != _Settings.UseCoverageClass)
                {
                    _Settings.UseCoverageClass = value;
                    FirePropertyChanged(() => UseCoverageClass);
                }
            }
        }

        public string CoverageClass
        {
            get { return _Settings.CoverageClass; }
            set
            {
                if (value != _Settings.CoverageClass)
                {
                    _Settings.CoverageClass = value;
                    FirePropertyChanged(() => CoverageClass);
                }
            }
        }
        #endregion Coverage Class

        #region Bootstrap Port
        public bool UseBootstrapPort
        {
            get { return _Settings.UseBootstrapPort; }
            set
            {
                if (value != _Settings.UseBootstrapPort)
                {
                    _Settings.UseBootstrapPort = value;
                    FirePropertyChanged(() => UseBootstrapPort);
                }
            }
        }

        public uint BootstrapPort
        {
            get { return _Settings.BootstrapPort; }
            set
            {
                if (value != _Settings.BootstrapPort)
                {
                    _Settings.BootstrapPort = value;
                    FirePropertyChanged(() => BootstrapPort);
                }
            }
        }
        #endregion Bootstrap Port

        #region Selendroid Port
        public bool UseSelendroidPort
        {
            get { return _Settings.UseSelendroidPort; }
            set
            {
                if (value != _Settings.UseSelendroidPort)
                {
                    _Settings.UseSelendroidPort = value;
                    FirePropertyChanged(() => UseSelendroidPort);
                }
            }
        }

        public uint SelendroidPort
        {
            get { return _Settings.SelendroidPort; }
            set
            {
                if (value != _Settings.SelendroidPort)
                {
                    _Settings.SelendroidPort = value;
                    FirePropertyChanged(() => SelendroidPort);
                }
            }
        }
        #endregion Selendroid Port

        #region Chrome Driver Port
        public bool UseChromeDriverPort
        {
            get { return _Settings.UseChromeDriverPort; }
            set
            {
                if (value != _Settings.UseChromeDriverPort)
                {
                    _Settings.UseChromeDriverPort = value;
                    FirePropertyChanged(() => UseChromeDriverPort);
                }
            }
        }

        public uint ChromeDriverPort
        {
            get { return _Settings.ChromeDriverPort; }
            set
            {
                if (value != _Settings.ChromeDriverPort)
                {
                    _Settings.ChromeDriverPort = value;
                    FirePropertyChanged(() => ChromeDriverPort);
                }
            }
        }
        #endregion Chrome Driver Port

        #endregion Advanced Section

        #endregion Public Properties

        #region Private Methods
        /// <summary>
        /// Open the File Dialog Window
        /// Display the previous location, or if the user inputted a partial filepath, open that filepath directory
        /// </summary>
        private void _ExecuteOpenFileDialog()
        {
            FilePath = OpenDialog.OpenFileDialog(FilePath, "Select Application", ".apk", "Android Apps (*.apk)|*.apk");
        }

        /// <summary>
        /// Can Execute the open file dialog 
        /// </summary>
        /// <returns></returns>
        private bool _CanExecuteOpenFileDialog()
        {
            return IsAppPathEnabled;
        }
        #endregion Private Methods

    }
}
