using AppiumWPF.Models;
using AppiumWPF.Utility;
using System.Windows.Input;

namespace AppiumWPF.ViewModels
{
    class DeveloperSettingsVM : BaseVM
    {
        #region Private Member Variables
        /// <summary>Settings object</summary>
        private IAppiumAppSettings _Settings;
        #endregion Private Member Variables

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public DeveloperSettingsVM(IAppiumAppSettings settings)
        {
            _Settings = settings;
        }
        #endregion Constructor

        #region Public Properties
        /// <summary>Use Developer Mode</summary>
        public bool UseDeveloperMode
        {
            get { return _Settings.UseDeveloperMode; }
            set
            {
                if (value != _Settings.UseDeveloperMode)
                {
                    _Settings.UseDeveloperMode = value;
                    FirePropertyChanged(() => UseDeveloperMode);
                }
            }
        }

        /// <summary>Use External Node JS Binary</summary>
        public bool UseExternalNodeJSBinary
        {
            get { return _Settings.UseExternalNodeJSBinary; }
            set
            {
                if (value != _Settings.UseExternalNodeJSBinary)
                {
                    _Settings.UseExternalNodeJSBinary = value;
                    FirePropertyChanged(() => UseExternalNodeJSBinary);
                }
            }
        }

        /// <summary>Location of External Node JS Binary</summary>
        public string ExternalNodeJSBinary
        {
            get { return _Settings.ExternalNodeJSBinary; }
            set
            {
                if (value != _Settings.ExternalNodeJSBinary)
                {
                    _Settings.ExternalNodeJSBinary = value;
                    FirePropertyChanged(() => ExternalNodeJSBinary);
                }
            }
        }

        /// <summary>Use the External Appium Package</summary>
        public bool UseExternalAppiumPackage
        {
            get { return _Settings.UseExternalAppiumPackage; }
            set
            {
                if (value != _Settings.UseExternalAppiumPackage)
                {
                    _Settings.UseExternalAppiumPackage = value;
                    FirePropertyChanged(() => UseExternalAppiumPackage);
                }
            }
        }

        /// <summary>External Appium Package Folder Location</summary>
        public string ExternalAppiumPackage
        {
            get { return _Settings.ExternalAppiumPackage; }
            set
            {
                if (value != _Settings.ExternalAppiumPackage)
                {
                    _Settings.ExternalAppiumPackage = value;
                    FirePropertyChanged(() => ExternalAppiumPackage);
                }
            }
        }

        /// <summary>Break on application start</summary>
        public bool IsBreakOnApplicationStart
        {
            get { return _Settings.BreakOnApplicationStart; }
            set
            {
                if (value != _Settings.BreakOnApplicationStart)
                {
                    _Settings.BreakOnApplicationStart = value;
                    FirePropertyChanged(() => IsBreakOnApplicationStart);
                }
            }
        }

        /// <summary>Enable the node JS debug port</summary>
        public bool UseNodeJSDebugPort
        {
            get { return _Settings.UseNodeJSDebugging; }
            set
            {
                if (value != _Settings.UseNodeJSDebugging)
                {
                    _Settings.UseNodeJSDebugging = value;
                    FirePropertyChanged(() => UseNodeJSDebugPort);
                }
            }
        }

        /// <summary>Node JS Debug Port</summary>
        public uint NodeJSDebugPort
        {
            get { return _Settings.NodeJSDebugPort; }
            set
            {
                if (value != _Settings.NodeJSDebugPort)
                {
                    _Settings.NodeJSDebugPort = value;
                    FirePropertyChanged(() => NodeJSDebugPort);
                }
            }
        }
        #endregion Public Properties

        #region Command Properties
        private ICommand _ChooseExternalNodeBinCommand;
        /// <summary>
        /// Open the File Dialog Command
        /// </summary>
        public ICommand ChooseExternalNodeBinCommand
        {
            get { return _ChooseExternalNodeBinCommand ?? (_ChooseExternalNodeBinCommand = new RelayCommand(() => _ExecuteBrowseNodeJSOpenFileDialog())); }
        }

        private ICommand _ChooseExternalAppiumCommand;
        /// <summary>
        /// Open the File Dialog Command
        /// </summary>
        public ICommand ChooseExternalAppiumCommand
        {
            get { return _ChooseExternalAppiumCommand ?? (_ChooseExternalAppiumCommand = new RelayCommand(() => _ExecuteBrowseExternalAppiumPackageOpenFileDialog())); }
        }
        #endregion Command Properties

        #region Private Methods
        /// <summary>
        /// Execute the Browse Node JS Open File Dialog Command 
        /// </summary>
        private void _ExecuteBrowseNodeJSOpenFileDialog()
        {
            ExternalNodeJSBinary = OpenDialog.OpenFileDialog(ExternalNodeJSBinary, "Select Your NodeJS Binary", ".exe", "NodeJS Binary (node.exe)|node.exe");
        }

        /// <summary>
        /// Execute the Browse External Appium Package Command 
        /// </summary>
        private void _ExecuteBrowseExternalAppiumPackageOpenFileDialog()
        {
            ExternalAppiumPackage = OpenDialog.OpenFolderDialog("Select Your Appium Package Folder");
        }
        #endregion Private Methods

    }

}
