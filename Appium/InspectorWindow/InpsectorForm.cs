using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Appium.Models.Inspector;
using System.Drawing.Drawing2D;

namespace Appium.InspectorWindow
{
	public partial class InpsectorForm : Form
	{
		private MainWindow.Model _Model;
		private RemoteWebDriver _Driver;
		private Graphics _ScreenShotGraphics;
		private Size _OriginalScreenshotSize;
		private const int _OutlineThickness = 3;

		public string LastMessage { get; set; }

		public InpsectorForm(MainWindow.Model model)
		{
			this._Model = model;
			InitializeComponent();
		}

		public bool Connect()
		{
			try
			{
				ICapabilities capabilities = new DesiredCapabilities();
				_Driver = new ScreenshotRemoteWebDriver(new Uri("http://" + this._Model.IPAddress + ":" + this._Model.Port.ToString() + "/wd/hub"), capabilities);
				// add increased timeout for inspector connection
				Dictionary<string, int> args = new Dictionary<string, int>();
				args.Add("timeout", 900);
				_Driver.ExecuteScript("mobile: setCommandTimeout", new object[] { args });
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
			Root rootNode = JsonConvert.DeserializeObject<Root>(pagesource);
			PopulateTree(rootNode.Hierarchy, DOMTreeView.Nodes);
		}

		private void PopulateTree(INode currentNode, TreeNodeCollection parentsNodes)
		{
			TreeNode newNode = new TreeNode(currentNode.GetDisplayName());
			newNode.Tag = currentNode;
			parentsNodes.Add(newNode);
			foreach (INode child in currentNode.GetChildren())
			{
				PopulateTree(child, newNode.Nodes);
			}
		}

		private void _AfterNodeSelect(object sender, TreeViewEventArgs e)
		{
			INode n = (INode)e.Node.Tag;
			DetailsTextBox.Text = n.GetDetails();
			Rectangle outlines = n.GetOutline();
			// correct outlines rectangle to have right and bottom line of the outline in the image
			if (outlines.Width > _OutlineThickness && outlines.Height > _OutlineThickness)
			{
				outlines.Width -= _OutlineThickness;
				outlines.Height-= _OutlineThickness;
			}
			if (_ScreenShotGraphics == null)
			{
				_ScreenShotGraphics = ScreenshotPictureBox.CreateGraphics();
			}
			else
			{
				// redraw to the original screenshot
				_ScreenShotGraphics.DrawImage(ScreenshotPictureBox.Image, 0, 0);
			}
			// draw rectangle
			using (Brush b = new SolidBrush(Color.Red))
			{
				using (Pen p = new Pen(Color.Red, _OutlineThickness))
				{
					GraphicsContainer prevStateContainer = _ScreenShotGraphics.BeginContainer();
					_ScreenShotGraphics.ScaleTransform((float)ScreenshotPictureBox.Image.Width/(float)_OriginalScreenshotSize.Width,
						(float)ScreenshotPictureBox.Image.Height/(float)_OriginalScreenshotSize.Height);
					_ScreenShotGraphics.DrawRectangle(p, outlines);
					_ScreenShotGraphics.EndContainer(prevStateContainer);
				}
			}
		}

		private void _SetScreenshot()
		{
			Image image = Image.FromStream(new MemoryStream(((ITakesScreenshot)_Driver).GetScreenshot().AsByteArray));
			_OriginalScreenshotSize = new Size(image.Width, image.Height);
			image = image.GetThumbnailImage(240, 320, null, IntPtr.Zero);
			ScreenshotPictureBox.Image = image;
		}

		public class Root
		{
			public UIAutomatorNode Hierarchy;
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
