using Appium.Models;
using Appium.Utility;
using System.Windows.Input;

namespace Appium.ViewModels
{
    class MainSettingsVM : BaseVM
    {
        private IAppiumAppSettings _Settings;
        public MainSettingsVM(IAppiumAppSettings settings)
        {
            _Settings = settings;

        }

        #region Public Properties
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

        /// <summary>
        /// Use the remove server
        /// </summary>
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


        #region  Command Properties
        private ICommand _ChooseFileCommand;
        /// <summary>
        /// Open the File Dialog Command
        /// </summary>
        public ICommand ChooseFileCommand
        {
            get { return _ChooseFileCommand ?? (_ChooseFileCommand = new RelayCommand(() => _ExecuteOpenFileDialog(), () => _CanExecuteOpenFileDialog())); }
        }
        #endregion  Command Properties

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
