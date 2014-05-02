using Appium.Models;
using Appium.Utility;
using System.Windows.Input;

namespace Appium.ViewModels
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

        #region Use Developer Mode
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
        #endregion Use Developer Mode

        #region External Node JS Binary
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
        #endregion External Node JS Binary

        #region External Appium Package
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
        #endregion External Appium Package

        #region Break On Application Start
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
        #endregion Break On Application Start

        #region Node JS Debug Port
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
        #endregion Node JS Debug Port

        #region Custom Server Flags
        /// <summary>Use Custom Server Flags</summary>
        public bool UseCustomServerFlags
        {
            get { return _Settings.UseCustomServerFlags; }
            set
            {
                if (value != _Settings.UseCustomServerFlags)
                {
                    _Settings.UseCustomServerFlags = value;
                    FirePropertyChanged(() => UseCustomServerFlags);
                }
            }
        }

        /// <summary>Custom Server Flags</summary>
        public string CustomServerFlags
        {
            get { return _Settings.CustomServerFlags; }
            set
            {
                if (value != _Settings.CustomServerFlags)
                {
                    _Settings.CustomServerFlags = value;
                    FirePropertyChanged(() => CustomServerFlags);
                }
            }
        }
        #endregion Custom Server Flags

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
