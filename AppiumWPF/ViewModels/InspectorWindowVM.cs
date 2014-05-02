using Appium.Engine;
using Appium.Models;
using Appium.Models.Inspector;
using Appium.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;

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
        #endregion Commands
        private SeleniumDriver __Driver = null;
        /// <summary>
        /// Driver which is lazy loaded
        /// </summary>
        private SeleniumDriver _Driver { get { return __Driver ?? (__Driver = new SeleniumDriver(_Settings)); } }

        private ObservableCollection<UIAutomatorNodeVM> _RootNode;
        /// <summary>Collection of root nodes</summary>
        public ObservableCollection<UIAutomatorNodeVM> RootNode
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

        #endregion  Properties

        #region Private Methods

        #region Command Methods
        private void _ExecuteRefreshCommand()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(
                new System.Threading.WaitCallback(_RefreshItems));
        }

        private void _RefreshItems(object state)
        {
            bool firstTime = false;
            // start the driver
            if (!_Driver.IsStarted)
            {
                firstTime = true;
                string errorMessage;
                Message = "Starting Selenium Driver";
                if (!_Driver.Start(out errorMessage))
                {
                    Console.WriteLine("error: {0}", errorMessage);
                    Message = "Error Starting Selenium Driver";
                    return;
                }
            }
            Message = "Updating";

            string pageSource = null;
            // grab the page source and parse it
            if (!string.IsNullOrWhiteSpace(pageSource = _Driver.GetPageSource()))
            {
                _CleanUpRoot();
                var root = _ConvertToUIAutomatorNode(pageSource);
                _RootNode = new ObservableCollection<UIAutomatorNodeVM>();
                UIAutomatorNodeVM vm = new UIAutomatorNodeVM(root, vm_SelectionChanged);
                _RootNode.Add(vm);
                FirePropertyChanged(() => RootNode);
            }

            if (firstTime)
            {
                // added sleep timer here since the setup I had was pulling in the "previous" picture
                System.Threading.Thread.Sleep(300);
            }

            // grab the image
            ImageByteArray = _Driver.GetScreenshot();
            Message = string.Format("Last updated on {0}", DateTime.Now);
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
        /// converts the xml page source into a UIAutomatorNode
        /// </summary>
        /// <param name="pageSource">xml page source</param>
        /// <returns>UI Automator Node representation of the DOM</returns>
        private static UIAutomatorNode _ConvertToUIAutomatorNode(string pageSource)
        {
            var nodeStack = new Stack<UIAutomatorNode>();
            UIAutomatorNode root = null;

            using (var reader = XmlReader.Create(new StringReader(pageSource)))
            {
                while (reader.Read())
                {
                    if ("hierarchy" == reader.Name || "AppiumAUT" == reader.Name)
                    {
                        continue;
                    }

                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            var node = new UIAutomatorNode(reader);

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
