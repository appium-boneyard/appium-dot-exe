using Appium.Engine;
using Appium.Models;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Appium.ViewModels
{
    class MainWindowVM : BaseVM
    {
        /// <summary>Main Settings View Model</summary>
        private MainSettingsVM _MainSettingsVM = null;

        /// <summary>View Model for Android Specific Settings</summary>
        private AndroidSettingsVM _AndroidSettingsVM = null;

        /// <summary>Appium Engine</summary>
        private AppiumEngine _AppiumEngine = null;

        /// <summary>Preference Window</summary>
        private PreferenceWindowVM _PreferenceWindowVM = null;

        /// <summary>Appium Settings</summary>
        private IAppiumAppSettings _Settings;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="closeAction">action called when we want to close the view</param>
        public MainWindowVM(Action closeAction)
        {
            AutomapperConfiguration.Configure();

            CloseAction = closeAction;

            // create the settings for the application
            _Settings = new DefaultAppiumAppSettings();

            // get the appium engine and wire up to it
            _AppiumEngine = AppiumEngine.Instance;
            _AppiumEngine.RunningStatusChanged += _AppiumEngine_RunningStatusChanged;
            _AppiumEngine.OutputDataReceived += _OutputDataReceived;
            _AppiumEngine.ErrorDataReceived += _OutputDataReceived;
            _AppiumEngine.Init(_Settings);
        }

        #endregion Constructor

        #region Public Properties

        #region View Models
        /// <summary>
        /// Main Settings VM
        /// contains such items as IP Address, port and application path
        /// </summary>
        public MainSettingsVM MainSettingsVM
        {
            get { return _MainSettingsVM ?? (_MainSettingsVM = new MainSettingsVM(_Settings)); }
        }

        /// <summary>
        /// Android Settings VM
        /// </summary>
        public AndroidSettingsVM AndroidSettingsVM
        {
            get { return _AndroidSettingsVM ?? (_AndroidSettingsVM = new AndroidSettingsVM(_Settings, _AppiumEngine.AVDs)); }
        }

        /// <summary>
        /// Preference Settings VM
        /// </summary>
        public PreferenceWindowVM PreferenceWindowVM
        {
            get { return _PreferenceWindowVM ?? (_PreferenceWindowVM = new PreferenceWindowVM(_Settings)); }
        }

        #endregion View Models

        #region Commands
        private ICommand _ExitCommand;
        /// <summary>the exit command</summary>
        public ICommand ExitCommand
        {
            get
            {
                return _ExitCommand ?? (_ExitCommand = new RelayCommand(() => _ExecuteExitCommand()));
            }
        }

        private ICommand _CheckCommand;
        /// <summary>Run the check doctor command</summary>
        public ICommand CheckCommand
        {
            get
            {
                return _CheckCommand ?? (_CheckCommand = new RelayCommand(() => _ExecuteCheckCommand()));
            }
        }

        private ICommand _LaunchCommand;
        /// <summary>Launch the Appium Node Server</summary>
        public ICommand LaunchCommand
        {
            get
            {
                return _LaunchCommand ?? (_LaunchCommand = new RelayCommand(() => _ExecuteLaunchCommand(), () => _CanExecuteLaunchCommand()));
            }
        }

        #endregion Commands

        /// <summary>The close action (should close the main window)</summary>
        public Action CloseAction { get; private set; }

        /// <summary>Launch String</summary>
        public string LaunchString
        {
            get { return _AppiumEngine.IsRunning ? "Stop" : "Launch"; }
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
        #endregion Public Call Back Method

        #region Private Methods

        #region Execute Commands
        /// <summary>
        /// Execute Exiting the application
        /// </summary>
        private void _ExecuteExitCommand()
        {
            // do not worry about saving here since we will catch it in the window's OnClosing event callback
            CloseAction();
        }

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
        #endregion Execute Commands

        #region Call Back Methods
        /// <summary>
        /// callback to Update the Display from the Appium Engine's output data event
        /// </summary>
        /// <param name="output">output string to display</param>
        private void _OutputDataReceived(string output)
        {
            Output += output + '\n';
        }

        /// <summary>
        /// Running status changed so update the buttons
        /// </summary>
        private void _AppiumEngine_RunningStatusChanged()
        {
            FirePropertyChanged(() => LaunchString);
        }
        #endregion Call Back Methods

        #endregion Private Methods

    }
}
