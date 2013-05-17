using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Appium.InspectorWindow
{
    public partial class InpsectorForm : Form
    {
        private MainWindow.Model _Model;
        private RemoteWebDriver _Driver;

        public InpsectorForm(MainWindow.Model model)
        {
            this._Model = model;
            InitializeComponent();
        }

        private void Inpsector_Load(object sender, EventArgs e)
        {
            ICapabilities capabilities = new DesiredCapabilities();
            _Driver = new ScreenshotRemoteWebDriver(new Uri("http://" + this._Model.IPAddress + ":" + this._Model.Port.ToString() + "/wd/hub"), capabilities);
        }

        private void Inpsector_FormClosing(object sender, FormClosingEventArgs e)
        {
            _Driver.Quit();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            DOMTreeView.Nodes.Clear();
            ScreenshotPictureBox.Image = Image.FromStream(new MemoryStream(((ITakesScreenshot)_Driver).GetScreenshot().AsByteArray)); ;
            string pagesource = _Driver.PageSource;
            Node rootNode = JsonConvert.DeserializeObject<Node>(pagesource);
            PopulateTree(rootNode, DOMTreeView.Nodes);
        }

        private void PopulateTree(Node currentNode, TreeNodeCollection parentsNodes)
        {
            TreeNode newNode = new TreeNode("[ " + currentNode.Type + "] " + currentNode.Value);
            parentsNodes.Add(newNode);
            foreach (Node child in currentNode.Children)
                PopulateTree(child, newNode.Nodes);
        }

        public class Node
        {
            public string Name;
            public string Type;
            public string Label;
            public string Value;
            public bool Enabled;
            public bool Valid;
            public RectInfo Rect;
            public string Dom;
            public bool Visible;
            public Node[] Children;

            public class RectInfo
            {
                public OriginInfo Origin;
                public SizeInfo Size;

                public class OriginInfo
                {
                    public float X;
                    public float Y;
                }
                public class SizeInfo
                {
                    public float Width;
                    public float Height;
                }
            }
        }

        /// <summary>overrides remotewebdriver in order to allow screenshots</summary>
        public class ScreenshotRemoteWebDriver : RemoteWebDriver, ITakesScreenshot
        {
            /// <summary>contructor</summary>
            /// <param name="remoteAddress">webdriver remote server address</param>
            /// <param name="capabilities">desired capabilities</param>
            public ScreenshotRemoteWebDriver(Uri remoteAddress, ICapabilities capabilities) : base(remoteAddress, capabilities) { }

            /// <summary>takes a screenshot</summary>
            /// <returns>a screenshot</returns>
            public Screenshot GetScreenshot()
            {
                Response screenshotResponse = this.Execute(DriverCommand.Screenshot, null);
                string base64 = screenshotResponse.Value.ToString();
                return new Screenshot(base64);
            }
        }
    }
}
