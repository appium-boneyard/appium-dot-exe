using Appium.Engine;
using Appium.Models;
using Appium.Utility;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;

namespace Appium.ViewModels
{
    class MainWindowVM : BaseVM
    {
        #region Private Member Variables
        /// <summary>Appium Engine</summary>
        private readonly AppiumEngine _AppiumEngine = null;

        /// <summary>View Model for Android Specific Settings</summary>
        private AndroidSettingsVM _AndroidSettingsVM = null;

        /// <summary>View Model for General Settings</summary>
        private GeneralSettingsVM _GeneralSettingsVM = null;

        /// <summary>View Model for Developer Settings</summary>
        private DeveloperSettingsVM _DeveloperSettingsVM = null;

        /// <summary>Appium Settings</summary>
        private readonly IAppiumAppSettings _Settings;
        #endregion Private Member Variables

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindowVM()
        {

            AutomapperConfiguration.Configure();

            // create the settings for the application
            _Settings = new DefaultAppiumAppSettings();

            // get the appium engine and wire up to it
            _AppiumEngine = AppiumEngine.Instance;
            _AppiumEngine.RunningStatusChanged += _AppiumEngine_RunningStatusChanged;
            _AppiumEngine.OutputDataReceived += _OutputDataReceived;
            _AppiumEngine.ErrorDataReceived += _OutputDataReceived;
            _AppiumEngine.Init(_Settings);

            if (_Settings.CheckForUpdates)
            {
                _AppiumEngine.CheckForUpdate();
            }
        }
        #endregion Constructor

        #region Public Properties
        private bool _IsAndroidSettingsOpen;
        /// <summary>
        /// Is the Android Settings Popup Open
        /// </summary>
        public bool IsAndroidSettingsOpen
        {
            get { return _IsAndroidSettingsOpen; }
            set
            {
                if (value != _IsAndroidSettingsOpen)
                {
                    _IsAndroidSettingsOpen = value;
                    FirePropertyChanged(() => IsAndroidSettingsOpen);
                }
            }
        }

        private bool _IsGeneralSettingsOpen;
        /// <summary>
        /// Is the General Settings Popup Open
        /// </summary>
        public bool IsGeneralSettingsOpen
        {
            get { return _IsGeneralSettingsOpen; }
            set
            {
                if (value != _IsGeneralSettingsOpen)
                {
                    _IsGeneralSettingsOpen = value;
                    FirePropertyChanged(() => IsGeneralSettingsOpen);
                }
            }
        }

        private bool _IsDeveloperSettingsOpen;
        /// <summary>
        /// Is the Developer Settings Popup Open
        /// </summary>
        public bool IsDeveloperSettingsOpen
        {
            get { return _IsDeveloperSettingsOpen; }
            set
            {
                if (value != _IsDeveloperSettingsOpen)
                {
                    _IsDeveloperSettingsOpen = value;
                    FirePropertyChanged(() => IsDeveloperSettingsOpen);
                }
            }
        }

        private bool _IsAboutOpen;
        /// <summary>
        /// Is the About Popup Open
        /// </summary>
        public bool IsAboutOpen
        {
            get { return _IsAboutOpen; }
            set
            {
                if (value != _IsAboutOpen)
                {
                    _IsAboutOpen = value;
                    FirePropertyChanged(() => IsAboutOpen);
                }

                if (value)
                {
                    FirePropertyChanged(() => Version);
                }
            }
        }

        private string _Version;
        /// <summary>
        /// Version Number of the assembly
        /// </summary>
        public string Version
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_Version))
                {
                    _Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                }
                return _Version;
            }
        }

        private string _Company;
        /// <summary>
        /// Company from the assembly
        /// </summary>
        public string Company
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_Company))
                {
                    _Company = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCompanyAttribute), false)).Company;
                }
                return _Company;
            }
        }

        private string _Copyright;
        /// <summary>
        /// Copyright info from the assembly
        /// </summary>
        public string Copyright
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_Copyright))
                {
                    _Copyright = ((AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCopyrightAttribute), false)).Copyright;
                }
                return _Copyright;
            }
        }


        /// <summary>Appium Settings</summary>
        public IAppiumAppSettings Settings { get { return _Settings; } }

        #region View Models
        /// <summary>
        /// Android Settings VM
        /// </summary>
        public AndroidSettingsVM AndroidSettingsVM
        {
            get { return _AndroidSettingsVM ?? (_AndroidSettingsVM = new AndroidSettingsVM(_Settings, _AppiumEngine.AVDs)); }
        }

        /// <summary>
        /// General Settings VM
        /// </summary>
        public GeneralSettingsVM GeneralSettingsVM
        {
            get { return _GeneralSettingsVM ?? (_GeneralSettingsVM = new GeneralSettingsVM(_Settings)); }
        }

        /// <summary>
        /// Developer Settings VM
        /// </summary>
        public DeveloperSettingsVM DeveloperSettingsVM
        {
            get { return _DeveloperSettingsVM ?? (_DeveloperSettingsVM = new DeveloperSettingsVM(_Settings)); }
        }
        #endregion View Models

        #region Commands
        private ICommand _CheckCommand;
        /// <summary>Run the check doctor command</summary>
        public ICommand CheckCommand
        {
            get { return _CheckCommand ?? (_CheckCommand = new RelayCommand(() => _ExecuteCheckCommand())); }
        }

        private ICommand _LaunchCommand;
        /// <summary>Launch the Appium Node Server</summary>
        public ICommand LaunchCommand
        {
            get { return _LaunchCommand ?? (_LaunchCommand = new RelayCommand(() => _ExecuteLaunchCommand(), () => _CanExecuteLaunchCommand())); }
        }

        private ICommand _ClearOutputCommand;
        /// <summary>Open the File Dialog Command</summary>
        public ICommand ClearOutputCommand
        {
            get { return _ClearOutputCommand ?? (_ClearOutputCommand = new RelayCommand(() => _ExecuteClearOutput())); }
        }

        private ICommand _AndroidSettingsCommand;
        /// <summary>Open the File Dialog Command</summary>
        public ICommand AndroidSettingsCommand
        {
            get { return _AndroidSettingsCommand ?? (_AndroidSettingsCommand = new RelayCommand(() => _ExecuteOpenAndroidSettings())); }
        }

        private ICommand _GeneralSettingsCommand;
        /// <summary>Open the File Dialog Command</summary>
        public ICommand GeneralSettingsCommand
        {
            get { return _GeneralSettingsCommand ?? (_GeneralSettingsCommand = new RelayCommand(() => _ExecuteOpenGeneralSettings())); }
        }

        private ICommand _DeveloperSettingsCommand;
        /// <summary>Open the File Dialog Command</summary>
        public ICommand DeveloperSettingsCommand
        {
            get { return _DeveloperSettingsCommand ?? (_DeveloperSettingsCommand = new RelayCommand(() => _ExecuteOpenDeveloperSettings())); }
        }

        private ICommand _AboutCommand;
        /// <summary>Open the File Dialog Command</summary>
        public ICommand AboutCommand
        {
            get { return _AboutCommand ?? (_AboutCommand = new RelayCommand(() => _ExecuteOpenAbout())); }
        }


        #endregion Commands

        public bool IsRunning
        {
            get { return _AppiumEngine.IsRunning; }
        }

        private string _Output = string.Empty;
        /// <summary>
        /// output from running the commands
        /// </summary>
        public string Output
        {
            get { return _Output; }
            set
            {
                if (value != _Output)
                {
                    _Output = value;
                    FirePropertyChanged(() => Output);
                }
            }
        }

        private bool _IsInspectorWindowOpen;
        /// <summary>
        /// Is Inspector window open
        /// </summary>
        public bool IsInspectorWindowOpen
        {
            get { return _IsInspectorWindowOpen; }
            set
            {
                if (value != _IsInspectorWindowOpen)
                {
                    _IsInspectorWindowOpen = value;
                    FirePropertyChanged(() => IsInspectorWindowOpen);
                }
            }
        }
        #endregion Public Properties

        #region Public Call Back Method
        /// <summary>
        /// On window closing event, stop the appium engine and save the settings
        /// </summary>
        /// <param name="sender">sender of the event</param>
        /// <param name="e">event args</param>
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            _AppiumEngine.Stop();
            _Settings.Save();
        }

        /// <summary>
        /// On inspector closed event, update the the property
        /// </summary>
        /// <param name="sender">NOT USED</param>
        /// <param name="e">NOT USED</param>
        public void OnInspectorWindowClosed(object sender, EventArgs e)
        {
            IsInspectorWindowOpen = false;
        }
        #endregion Public Call Back Method

        #region Private Methods

        #region Execute Commands
        /// <summary>
        /// Execute the Check Doctor command
        /// </summary>
        private void _ExecuteCheckCommand()
        {
            // TODO
            Console.WriteLine("Check Command");
        }

        /// <summary>
        /// Can Execute the launch command (checks if the engine is initialized)
        /// </summary>
        /// <returns>true if can launch, false otherwise</returns>
        private bool _CanExecuteLaunchCommand()
        {
            return _AppiumEngine.IsInitialized;
        }

        /// <summary>
        /// Execute the launching of the node server
        /// </summary>
        private void _ExecuteLaunchCommand()
        {
            if (_AppiumEngine.IsRunning)
            {
                _AppiumEngine.Stop();
            }
            else
            {
                _AppiumEngine.Start();
            }
        }

        /// <summary>Clear the output string</summary>
        private void _ExecuteClearOutput()
        {
            Output = string.Empty;
        }

        /// <summary>
        /// Opens the Android Settings Popup
        /// </summary>
        private void _ExecuteOpenAndroidSettings()
        {
            IsAndroidSettingsOpen = true;
        }

        /// <summary>
        /// Opens the General Settings Popup
        /// </summary>
        private void _ExecuteOpenGeneralSettings()
        {
            IsGeneralSettingsOpen = true;
        }

        /// <summary>
        /// Opens the Developer Settings Popup
        /// </summary>
        private void _ExecuteOpenDeveloperSettings()
        {
            IsDeveloperSettingsOpen = true;
        }

        /// <summary>
        /// Opens the About Popup
        /// </summary>
        private void _ExecuteOpenAbout()
        {
            IsAboutOpen = true;
        }
        #endregion Execute Commands

        #region Call Back Methods
        /// <summary>
        /// callback to Update the Display from the Appium Engine's output data event
        /// </summary>
        /// <param name="output">output string to display</param>
        private void _OutputDataReceived(string output)
        {
            Output += string.Format("> {0}\n", output);
        }

        /// <summary>
        /// Running status changed so update the buttons
        /// </summary>
        private void _AppiumEngine_RunningStatusChanged()
        {
            FirePropertyChanged(() => IsRunning);
        }
        #endregion Call Back Methods

        #endregion Private Methods

    }
}
