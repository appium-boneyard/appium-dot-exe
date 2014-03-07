using AppiumWPF.Models;
using System.Collections.Generic;

namespace AppiumWPF.ViewModels
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
        }
        #endregion Constructor

        #region Public Properties
        /// <summary>
        /// Use the application package flag (--app-pkg)
        /// </summary>
        public bool IsPackageEnabled
        {
            get { return _Settings.UseAndroidPackage; }
            set
            {
                if (value != _Settings.UseAndroidPackage)
                {
                    _Settings.UseAndroidPackage = value;
                    FirePropertyChanged(() => IsPackageEnabled);
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

        /// <summary>
        /// Use the android activity flag (--app-activity)
        /// </summary>
        public bool IsActivityEnabled
        {
            get { return _Settings.UseAndroidActivity; }
            set
            {
                if (value != _Settings.UseAndroidActivity)
                {
                    _Settings.UseAndroidActivity= value;
                    FirePropertyChanged(() => IsActivityEnabled);
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

        /// <summary>
        /// enable the avd flag (--avd) 
        /// </summary>
        public bool IsLaunchAVDEnabled
        {
            get { return _Settings.LaunchAVD; }
            set
            {
                if (value != _Settings.LaunchAVD)
                {
                    _Settings.LaunchAVD = value;
                    FirePropertyChanged(() => IsLaunchAVDEnabled);
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

        /// <summary>
        ///  enable the application wait activity (--app-wait-activity [activity])
        /// </summary>
        public bool IsWaitForActivityEnabled
        {
            get { return _Settings.UseAndroidWaitActivity; }
            set
            {
                if (value != _Settings.UseAndroidWaitActivity)
                {
                    _Settings.UseAndroidWaitActivity = value;
                    FirePropertyChanged(() => IsWaitForActivityEnabled);
                }
            }
        }

        /// <summary>
        /// Activity name for the Android activity you want to wait for (e.g. SplashActivity)
        /// </summary>
        public string WaitForActivity
        {
            get { return _Settings.AndroidWaitActivity; }
            set
            {
                if (value != _Settings.AndroidWaitActivity)
                {
                    _Settings.AndroidWaitActivity = value;
                    FirePropertyChanged(() => WaitForActivity);
                }
            }
        }

        /// <summary>
        /// Enable the device timeout flag (--device-ready-timeout [number])
        /// </summary>
        public bool IsDeviceReadyTimeoutEnabled
        {
            get { return _Settings.UseAndroidDeviceReadyTimeout; }
            set
            {
                if (value != _Settings.UseAndroidDeviceReadyTimeout)
                {
                    _Settings.UseAndroidDeviceReadyTimeout = value;
                    FirePropertyChanged(() => IsDeviceReadyTimeoutEnabled);
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
        #endregion Public Properties
    }
}
