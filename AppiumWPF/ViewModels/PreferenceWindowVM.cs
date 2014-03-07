using Appium.Models;

namespace Appium.ViewModels
{
    class PreferenceWindowVM
    {
        #region Private Member Variables
        /// <summary>Settings object</summary>
        private IAppiumAppSettings _Settings;

        /// <summary>General Preferences</summary>
        private GeneralPreferencesVM _GeneralPreferenceVM;

        #endregion Private Member Variables

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PreferenceWindowVM(IAppiumAppSettings settings)
        {
            _Settings = settings;
        }
        #endregion Constructor

       #region Public Properties
        /// <summary>General Preference View Model</summary>
        public GeneralPreferencesVM GeneralPreferencesVM
        {
            get { return _GeneralPreferenceVM ?? (_GeneralPreferenceVM = new GeneralPreferencesVM(_Settings)); }
        }
        #endregion Public Properties
    }
}
