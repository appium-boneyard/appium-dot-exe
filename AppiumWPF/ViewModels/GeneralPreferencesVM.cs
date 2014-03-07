using Appium.Models;

namespace Appium.ViewModels
{
    class GeneralPreferencesVM : BaseVM
    {
        #region Private Member Variables
        /// <summary>Settings object</summary>
        private IAppiumAppSettings _Settings;

        /// <summary>Developer Mode Settings VM</summary>
        private DeveloperSettingsVM _DeveloperSettingsVM;
        #endregion Private Member Variables

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public GeneralPreferencesVM(IAppiumAppSettings settings)
        {
            _Settings = settings;
        }
        #endregion Constructor

        #region Public Properties

        #region View Models
        /// <summary>Developer Mode Settings VM</summary>
        public DeveloperSettingsVM DeveloperSettingsVM { get { return _DeveloperSettingsVM ?? (_DeveloperSettingsVM = new DeveloperSettingsVM(_Settings)); } }
        #endregion View Models

        /// <summary>true if appium should check for updates</summary>
        public bool IsCheckUpdateEnabled
        {
            get { return _Settings.CheckForUpdates; }
            set
            {
                if (value != _Settings.CheckForUpdates)
                {
                    _Settings.CheckForUpdates = value;
                    FirePropertyChanged(() => IsCheckUpdateEnabled);
                }
            }
        }

        /// <summary>true if quiet logigng should be used</summary>
        public bool IsQuietLoggingEnabled
        {
            get { return _Settings.QuietLogging; }
            set
            {
                if (value != _Settings.QuietLogging)
                {
                    _Settings.QuietLogging = value;
                    FirePropertyChanged(() => IsQuietLoggingEnabled);
                }
            }
        }

        /// <summary>true if artifacts will be kept after a session</summary>
        public bool IsKeepArtifactsEnabled
        {
            get { return _Settings.KeepArtifacts; }
            set
            {
                if (value != _Settings.KeepArtifacts)
                {
                    _Settings.KeepArtifacts = value;
                    FirePropertyChanged(() => IsKeepArtifactsEnabled);
                }
            }
        }

        /// <summary>true if application state should be reset between sessions</summary>
        public bool IsResetApplicationStateEnabled 
        {
            get { return _Settings.ResetApplicationState; }
            set
            {
                if (value != _Settings.ResetApplicationState)
                {
                    _Settings.ResetApplicationState= value;
                    FirePropertyChanged(() => IsResetApplicationStateEnabled);
                }
            }
        }

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
        #endregion Public Properties

    }
}
