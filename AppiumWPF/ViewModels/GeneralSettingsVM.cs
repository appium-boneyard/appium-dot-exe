using Appium.Engine;
using Appium.Models;

namespace Appium.ViewModels
{
    class GeneralSettingsVM : BaseVM
    {
        #region Private Member Variables
        /// <summary>Settings object</summary>
        private IAppiumAppSettings _Settings;
        #endregion Private Member Variables

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public GeneralSettingsVM(IAppiumAppSettings settings)
        {
            _Settings = settings;
        }
        #endregion Constructor

        #region Public Properties

        #region Server

        #region Address
        /// <summary>
        /// IP address as a string
        /// </summary>
        public string IPAddressString
        {
            get { return _Settings.IPAddress; }
            set
            {
                if (value != _Settings.IPAddress)
                {
                    _Settings.IPAddress = value;
                    FirePropertyChanged(() => IPAddressString);
                }
            }
        }

        /// <summary>
        /// Port used for the connection
        /// </summary>
        public uint Port
        {
            get { return _Settings.Port; }
            set
            {
                if (value != _Settings.Port)
                {
                    _Settings.Port = value;
                    FirePropertyChanged(() => Port);
                }
            }
        }
        #endregion Address

        #region Check Update
        /// <summary>true if appium should check for updates</summary>
        public bool CheckUpdate
        {
            get { return _Settings.CheckForUpdates; }
            set
            {
                if (value != _Settings.CheckForUpdates)
                {
                    _Settings.CheckForUpdates = value;
                    FirePropertyChanged(() => CheckUpdate);

                    // check to see if there are new updates if the flag has been switched
                    if (_Settings.CheckForUpdates)
                    {
                        AppiumEngine.Instance.CheckForUpdate();
                    }
                }
            }
        }
        #endregion Check Update

        public bool OverrideExistingSessions
        {
            get { return _Settings.OverrideExistingSessions; }
            set
            {
                if (value != _Settings.OverrideExistingSessions)
                {
                    _Settings.OverrideExistingSessions = value;
                    FirePropertyChanged(() => OverrideExistingSessions);
                }
            }
        }

        public bool KillProcessUsingServerPortBeforeLaunch
        {
            get { return _Settings.KillProcessUsingServerPortBeforeLaunch; }
            set
            {
                if (value != _Settings.KillProcessUsingServerPortBeforeLaunch)
                {
                    _Settings.KillProcessUsingServerPortBeforeLaunch = value;
                    FirePropertyChanged(() => KillProcessUsingServerPortBeforeLaunch);
                }
            }
        }

        public bool UseRemoteServer
        {
            get { return _Settings.UseRemoteServer; }
            set
            {
                if (value != _Settings.UseRemoteServer)
                {
                    _Settings.UseRemoteServer = value;
                    FirePropertyChanged(() => UseRemoteServer);
                }
            }
        }

        #region Grid Selenium Config File
        public bool UseGridSeleniumConfigFile
        {
            get { return _Settings.UseGridSeleniumConfigFile; }
            set
            {
                if (value != _Settings.UseGridSeleniumConfigFile)
                {
                    _Settings.UseGridSeleniumConfigFile = value;
                    FirePropertyChanged(() => UseGridSeleniumConfigFile);
                }
            }
        }

        public string GridSeleniumConfigFile
        {
            get { return _Settings.GridSeleniumConfigFile; }
            set
            {
                if (value != _Settings.GridSeleniumConfigFile)
                {
                    _Settings.GridSeleniumConfigFile = value;
                    FirePropertyChanged(() => GridSeleniumConfigFile);
                }
            }
        }
        #endregion Grid Selenium Config File

        /// <summary>true if appium should prelaunch the application</summary>
        public bool IsPrelaunchEnabled
        {
            get { return _Settings.PrelaunchApplication; }
            set
            {
                if (value != _Settings.PrelaunchApplication)
                {
                    _Settings.PrelaunchApplication = value;
                    FirePropertyChanged(() => IsPrelaunchEnabled);
                }
            }
        }

        #endregion Server


        #region Logging

        /// <summary>true if quiet logigng should be used</summary>
        public bool UseQuietLogging
        {
            get { return _Settings.QuietLogging; }
            set
            {
                if (value != _Settings.QuietLogging)
                {
                    _Settings.QuietLogging = value;
                    FirePropertyChanged(() => UseQuietLogging);
                }
            }
        }

        public bool ShowTimestamps
        {
            get { return _Settings.ShowTimestamps; }
            set
            {
                if (value != _Settings.ShowTimestamps)
                {
                    _Settings.ShowTimestamps = value;
                    FirePropertyChanged(() => ShowTimestamps);
                }
            }
        }

        #region Log To File
        public bool UseLogToFile
        {
            get { return _Settings.UseLogToFile; }
            set
            {
                if (value != _Settings.UseLogToFile)
                {
                    _Settings.UseLogToFile = value;
                    FirePropertyChanged(() => UseLogToFile);
                }
            }
        }

        public string LogToFile
        {
            get { return _Settings.LogToFile; }
            set
            {
                if (value != _Settings.LogToFile)
                {
                    _Settings.LogToFile = value;
                    FirePropertyChanged(() => LogToFile);
                }
            }
        }
        #endregion Log To File

        #region Log To WebHook
        public bool UseLogToWebHook
        {
            get { return _Settings.UseLogToWebHook; }
            set
            {
                if (value != _Settings.UseLogToWebHook)
                {
                    _Settings.UseLogToWebHook = value;
                    FirePropertyChanged(() => UseLogToWebHook);
                }
            }
        }

        public string LogToWebHook
        {
            get { return _Settings.LogToWebHook; }
            set
            {
                if (value != _Settings.LogToWebHook)
                {
                    _Settings.LogToWebHook = value;
                    FirePropertyChanged(() => LogToWebHook);
                }
            }
        }
        #endregion Log To WebHook

        #region Local Timezone
        /// <summary>
        /// Use the Local Timezone
        /// </summary>
        public bool UseLocalTimezone
        {
            get { return _Settings.UseLocalTimezone; }
            set
            {
                if (value != _Settings.UseLocalTimezone)
                {
                    _Settings.UseLocalTimezone = value;
                    FirePropertyChanged(() => UseLocalTimezone);
                }
            }
        }
        #endregion Local Timezone


        #endregion Logging

        #endregion Public Properties

    }
}
