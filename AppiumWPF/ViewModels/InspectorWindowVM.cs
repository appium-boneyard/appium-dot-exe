using Appium.Models;
using Appium.Models.Inspector;
using Appium.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace Appium.ViewModels
{
    /// <summary>
    /// View Model for the Inspector
    /// </summary>
    class InspectorWindowVM : BaseVM
    {
        #region Private Member Variables
        private IAppiumAppSettings _Settings;
        #endregion Private Member Variables

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">Current Settings</param>
        public InspectorWindowVM(IAppiumAppSettings settings)
        {
            _Settings = settings;
            Message = "Click \"Refresh\" button to start inspecting";
        }
        #endregion Constructor

        #region  Properties

        #region Commands
        private ICommand _RefreshCommand;
        /// <summary>Refresh Command to refresh the tree</summary>
        public ICommand RefreshCommand
        {
            get
            {
                return _RefreshCommand ?? (_RefreshCommand = new RelayCommand(() => _ExecuteRefreshCommand()));
            }
        }

        private ICommand _RecordCommand;
        /// <summary>Refresh Command to refresh the tree</summary>
        public ICommand RecordCommand
        {
            get
            {
                return _RecordCommand ?? (_RecordCommand = new RelayCommand(() => _ExecuteRecordCommand()));
            }
        }

        private ICommand _TapCommand;
        /// <summary>Command to tap on an item</summary>
        public ICommand TapCommand
        {
            get
            {
                return _TapCommand ?? (_TapCommand = new RelayCommand(() => _ExecuteTapCommand()));
            }
        }

        private ICommand _SendKeysCommand;
        /// <summary>Command to send keys to a node</summary>
        public ICommand SendKeysCommand
        {
            get
            {
                return _SendKeysCommand ?? (_SendKeysCommand = new RelayCommand(() => _ExecuteSendKeysCommand(), () => { return (!string.IsNullOrEmpty(TextInput) && (SelectedNode != null)); }));
            }
        }

        #endregion Commands
        private AppiumDriver _Driver;
        /// <summary>
        /// Driver which is lazy loaded
        /// </summary>

        private NodeTree<UIAutomatorNodeVM> _RootNode;
        /// <summary>Collection of root nodes</summary>
        public NodeTree<UIAutomatorNodeVM> RootNode
        {
            get { return _RootNode; }
        }

        private UIAutomatorNodeVM _SelectedNode;
        public UIAutomatorNodeVM SelectedNode
        {
            get { return _SelectedNode; }
            set
            {
                if (value != _SelectedNode)
                {
                    _SelectedNode = value;
                    FirePropertyChanged(() => SelectedNode);
                }
            }
        }

        private byte[] _ImageByteArray;
        /// <summary>
        /// Image byte array
        /// </summary>
        public byte[] ImageByteArray
        {
            get { return _ImageByteArray; }
            set
            {
                if (value != _ImageByteArray)
                {
                    _ImageByteArray = value;
                    FirePropertyChanged(() => ImageByteArray);
                }
            }

        }

        private string _Message;
        /// <summary>Message to display to the user</summary>
        public string Message
        {
            get { return _Message; }
            set
            {
                if (value != _Message)
                {
                    _Message = value;
                    FirePropertyChanged((() => Message));
                }
            }
        }

        private string _TextInput;
        /// <summary>Text to be sent to the App</summary>
        public string TextInput
        {
            get { return _TextInput; }
            set
            {
                if (value != _TextInput)
                {
                    _TextInput = value;
                    FirePropertyChanged((() => TextInput));
                }
            }
        }

        #endregion  Properties

        #region Private Methods

        #region Command Methods
        private void _ExecuteRefreshCommand()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(
                new System.Threading.WaitCallback(_RefreshItems));
        }

        private void _ExecuteSendKeysCommand()
        {
            UIAutomatorNodeVM vmCurrent = SelectedNode;

            try
            {
                _Driver.FindElementById(SelectedNode.Id).SendKeys(TextInput);
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("Error performing send keys action: {0}", e.Message), "Inspector Action Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _ExecuteRefreshCommand();
            }
        }

        private void _ExecuteTapCommand()
        {
            UIAutomatorNodeVM vmCurrent = SelectedNode;

            try
            {
                _Driver.FindElementById(SelectedNode.Id).Click();
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("Error performing tap action: {0}", e.Message), "Inspector Action Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _ExecuteRefreshCommand();
            }
        }

        private void _RefreshItems(object state)
        {
            bool firstTime = false;
            // start the driver
            if (_Driver == null)
            {
                firstTime = true;
                Message = "Attempting to connect to Appium";

                try
                {
                    Dictionary<string, object> capsDef = new Dictionary<string, object>();

                    // Only set automation name if it isn't equal to the default
                    if (_Settings.AutomationName != "Appium")
                    {
                        capsDef.Add("automationName", _Settings.AutomationName);
                    }

                    if (_Settings.UseDeviceName && _Settings.DeviceName != "")
                    {
                        capsDef.Add("deviceName", _Settings.DeviceName);
                    }

                    if (!_Settings.UseAndroidBrowser)
                    {
                        if (_Settings.UseApplicationPath && _Settings.ApplicationPath != "")
                        {
                            capsDef.Add("app", _Settings.ApplicationPath);
                        }

                        if (_Settings.UseAndroidActivity && _Settings.AndroidActivity != "")
                        {
                            capsDef.Add("appActivity", _Settings.AndroidActivity);
                        }

                        if (_Settings.UseAndroidPackage && _Settings.AndroidPackage != "")
                        {
                            capsDef.Add("appPackage", _Settings.AndroidPackage);
                        }

                        if (_Settings.UseAndroidWaitForActivity && _Settings.AndroidWaitForActivity != "")
                        {
                            capsDef.Add("appWaitActivity", _Settings.AndroidWaitForActivity);
                        }

                        if (_Settings.UseAndroidWaitForPackage && _Settings.AndroidWaitForPackage != "")
                        {
                            capsDef.Add("appWaitPackage", _Settings.AndroidWaitForPackage);
                        }
                    }
                    else
                    {
                        capsDef.Add("browserName", _Settings.AndroidBrowser);
                    }

                    if (_Settings.UseAndroidDeviceReadyTimeout && _Settings.AndroidDeviceReadyTimeout.ToString() != "")
                    {
                        capsDef.Add("deviceReadyTimeout", _Settings.AndroidDeviceReadyTimeout.ToString());
                    }

                    if (_Settings.UseCoverageClass && _Settings.CoverageClass != "")
                    {
                        capsDef.Add("androidCoverage", _Settings.CoverageClass);
                    }

                    if (_Settings.UseAndroidIntentAction && _Settings.AndroidIntentAction != "")
                    {
                        capsDef.Add("intentAction", _Settings.AndroidIntentAction);
                    }

                    if (_Settings.UseAndroidIntentCategory && _Settings.AndroidIntentCategory != "")
                    {
                        capsDef.Add("intentCategory", _Settings.AndroidIntentCategory);
                    }

                    if (_Settings.UseAndroidIntentFlags && _Settings.AndroidIntentFlags != "")
                    {
                        capsDef.Add("intentFlags", _Settings.AndroidIntentFlags);
                    }

                    if (_Settings.UseAndroidIntentArguments && _Settings.AndroidIntentArguments != "")
                    {
                        capsDef.Add("optionalIntentArguments", _Settings.AndroidIntentArguments);
                    }

                    // Include the platform if any of the capabilities were set
                    if (capsDef.Count != 0 && _Settings.PlatformName != "")
                    {
                        capsDef.Add("platformName", _Settings.PlatformName);
                    }

                    _Driver = new AppiumDriver(new Uri(String.Format("http://{0}:{1}/wd/hub", _Settings.IPAddress, _Settings.Port)), new DesiredCapabilities(capsDef));
                }
                catch
                {
                    MessageBox.Show("Failed to connect to the server. Please check that it is running.", "Inspector Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Message = "Failed to connect to Appium";
                }

                if (_Driver == null)
                {
                    if (_Settings.UseRemoteServer)
                    {
                        Message = "Failed to connect to remote Appium server";
                    }
                    else
                    {
                        Message = "Failed to connect to Appium";
                    }

                    return;
                }
            }

            Message = "Updating";

            string pageSource = null;
            // grab the page source and parse it
            try
            {
                if (!string.IsNullOrWhiteSpace(pageSource = _Driver.PageSource))
                {
                    _CleanUpRoot();
                    var root = _ConvertToUIAutomatorNode(pageSource);
                    _RootNode = new NodeTree<UIAutomatorNodeVM>();
                    UIAutomatorNodeVM vm = new UIAutomatorNodeVM(root, vm_SelectionChanged);
                    _RootNode.Add(vm);
                    FirePropertyChanged(() => RootNode);
                }
                else
                {
                    Message = "Error getting page source";
                }
            }
            catch (Exception e)
            {
                Message = String.Format("Error getting source page: {0}", e.Message);
            }

            if (firstTime)
            {
                // added sleep timer here since the setup I had was pulling in the "previous" picture
                System.Threading.Thread.Sleep(300);
            }

            // grab the image
            Screenshot screenshot = null;
            try
            {
                screenshot = _Driver.GetScreenshot();
            }
            catch (Exception e)
            {
                Message = String.Format("Error getting screenshot: {0}", e.Message);
            }
            finally
            {
                if (screenshot != null)
                {
                    ImageByteArray = screenshot.AsByteArray;

                    if (ImageByteArray == null)
                    {
                        Message = "Error getting screenshot";
                    }
                }
                else
                {
                    Message = "Error getting screenshot";
                }
            }
            
            // Show success message if the "Updating" message was not changed
            if (Message.Equals("Updating"))
            {
                Message = string.Format("Last successfully updated on {0}", DateTime.Now);
            }
        }

        /// <summary>Clean up all the root nodes</summary>
        private void _CleanUpRoot()
        {
            // clean up all the attached event's, reduce memory leaks
            if (null != _RootNode)
            {
                foreach (UIAutomatorNodeVM roots in _RootNode)
                {
                    roots.Dispose();
                }
            }
        }

        /// <summary>
        /// Call back method to inform this vm which item is selected
        /// </summary>
        /// <param name="node">node that the selection was changed</param>
        private void vm_SelectionChanged(UIAutomatorNodeVM node)
        {
            if (null != node && node.IsSelected)
            {
                SelectedNode = node;
            }
        }

        /// <summary>Start recording the clicking commands</summary>
        private void _ExecuteRecordCommand()
        {
            throw new NotImplementedException();
        }
        #endregion Command Methods

        /// <summary>
        /// converts the xml page source into a UIAutomatorNode (with it's children)
        /// </summary>
        /// <param name="pageSource">xml page source</param>
        /// <returns>INode representation of the DOM</returns>
        private static INode _ConvertToUIAutomatorNode(string pageSource)
        {
            var nodeStack = new Stack<AbstractNode>();
            INode root = null;
            bool isApple = false;

            using (var reader = XmlReader.Create(new StringReader(pageSource)))
            {
                while (reader.Read())
                {
                    if ("hierarchy" == reader.Name)
                    {
                        continue;
                    }
                    else if ("AppiumAUT" == reader.Name)
                    {
                        isApple = true;
                        continue;
                    }

                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            AbstractNode node = null;
                            if (isApple)
                            {
                                node = new UIAutomatorAppleNode(reader);
                            }
                            else
                            {
                                node = new UIAutomatorAndroidNode(reader);
                            }

                            if (reader.IsEmptyElement)
                            {
                                if (0 == nodeStack.Count)
                                {
                                    root = node;
                                }
                                else
                                {
                                    nodeStack.Peek().Children.Add(node);
                                }
                            }
                            else
                            {
                                nodeStack.Push(node);
                            }
                            break;
                        case XmlNodeType.EndElement:
                            var child = nodeStack.Pop();
                            if (nodeStack.Count == 0)
                            {
                                root = child;
                            }
                            else
                            {
                                var parent = nodeStack.Peek();
                                parent.Children.Add(child);
                            }
                            break;
                    }
                }
            }
            return root;
        }


        #endregion Private Methods
    }
}
