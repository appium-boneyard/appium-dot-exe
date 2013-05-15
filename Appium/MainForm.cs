using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Appium
{
    public partial class MainForm : Form
    {
        /// <summary>constructor</summary>
        public MainForm()
        {
            InitializeComponent();
        }

        #region Model
        /// <summary>ip address to listen on</summary>
        public string IPAddress { get { return this.IPAddressTextBox.Text; } set { this.IPAddressTextBox.Text = value; } }
        /// <summary>port to listen on</summary>
        public uint Port { get { return Convert.ToUInt32(this.PortTextBox.Value); } set { this.PortTextBox.Value = value; } }
        /// <summary>text displayed on the launch button</summary>
        public string LaunchButtonText { get { return this.LaunchButton.Text; } set { this.LaunchButton.Text = value; } }
        #endregion

        #region Handlers
        /// <summary>called when the exit menu item is clicked on the file menu</summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void FileMenuExitItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>called when the launch button is clicked</summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void LaunchButton_Click(object sender, EventArgs e)
        {
            var appiumServerProcessStartInfo = new ProcessStartInfo();
            appiumServerProcessStartInfo.FileName = "C:\\Program Files\\nodejs\\node.exe";
            appiumServerProcessStartInfo.Arguments = "C:\\Users\\danc\\Documents\\Github\\appium\\server.js";
            appiumServerProcessStartInfo.CreateNoWindow = true;
            appiumServerProcessStartInfo.UseShellExecute = false;
            appiumServerProcessStartInfo.RedirectStandardOutput = true;
            appiumServerProcessStartInfo.RedirectStandardInput = true;
            appiumServerProcessStartInfo.RedirectStandardError = true;
            var appiumServerProcess =  Process.Start(appiumServerProcessStartInfo);
        }
        #endregion
    }
}
