using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Appium.InspectorWindow
{
    public partial class InpsectorForm : Form
    {
        private MainWindow.Model _Model;
        private RemoteWebDriver _Driver;

		public string LastMessage { get; set; }

        public InpsectorForm(MainWindow.Model model)
        {
            this._Model = model;
            InitializeComponent();
        }

		public bool Load()
		{
			try
			{
				ICapabilities capabilities = new DesiredCapabilities();
				_Driver = new ScreenshotRemoteWebDriver(new Uri("http://" + this._Model.IPAddress + ":" + this._Model.Port.ToString() + "/wd/hub"), capabilities);
			}
			catch (Exception e)
			{
				LastMessage = e.Message;
				return false;
			}
			return true;
		}

        private void Inpsector_FormClosing(object sender, FormClosingEventArgs e)
        {
            _Driver.Quit();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            DOMTreeView.Nodes.Clear();
            _SetScreenshot();
            string pagesource = _Driver.PageSource;
            Node rootNode = JsonConvert.DeserializeObject<Node>(pagesource);
            PopulateTree(rootNode, DOMTreeView.Nodes);
        }

        private void PopulateTree(Node currentNode, TreeNodeCollection parentsNodes)
        {
            TreeNode newNode = new TreeNode("[ " + currentNode.Type + "] " + currentNode.Value);
            newNode.Tag = currentNode;
            parentsNodes.Add(newNode);
            foreach (Node child in currentNode.Children)
                PopulateTree(child, newNode.Nodes);
        }

        private void _AfterNodeSelect(object sender, TreeViewEventArgs e)
        {
            Node n = (Node)e.Node.Tag;
            DetailsTextBox.Text = n.GetDetails();
        }

        private void _SetScreenshot()
        {
            Image image = Image.FromStream(new MemoryStream(((ITakesScreenshot)_Driver).GetScreenshot().AsByteArray));
            image = image.GetThumbnailImage(240, 320, null, IntPtr.Zero);
            ScreenshotPictureBox.Image = image;
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

            public string GetDetails()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Name: " + this.Name ?? "");
                sb.AppendLine("Type: " + this.Type ?? "");
                sb.AppendLine("Label: " + this.Label ?? "");
                sb.AppendLine("Value: " + this.Value ?? "");
                sb.AppendLine("Enabled: " + (this.Enabled ? "true" : "false"));
                sb.AppendLine("Visible: " + (this.Visible ? "true" : "false"));
                sb.AppendLine("Valid: " + (this.Valid ? "true" : "false"));
                sb.AppendLine("Location: " + (this.Rect != null ? "(" + Rect.Origin.X.ToString() + ", " + Rect.Origin.Y.ToString() + ")" : "" ));
                sb.AppendLine("Size: " + (this.Rect != null ? "(" + Rect.Size.Width.ToString() + ", " + Rect.Size.Height.ToString() + ")" : ""));
                return sb.ToString();
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
