using System;
using System.IO;
using System.Windows.Forms;

namespace Appium
{
    public partial class MainForm : Form
    {
        #region Paths
        /// <summary>lazy appium folder path</summary>
        private static Lazy<string> _AppiumFolder = new Lazy<string>(() => Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof(MainForm)).Location));
        /// <summary>appium folder path</summary>
        public static string AppiumRootFolder { get { return _AppiumFolder.Value; } }
        /// <summary>path to node.exe</summary>
        public static string NodePath { get { return Path.Combine(AppiumRootFolder, "node.exe"); } }
        /// <summary>path to node package manager</summary>
        public static string NPMPath { get { return Path.Combine(AppiumRootFolder, "npm.cmd"); } }
        /// <summary>path to the node modules folder</summary>
        public static string NodeModulesFolder { get { return Path.Combine(AppiumRootFolder, "node_modules"); } }
        /// <summary>path to the appium package folder</summary>
        public static string AppiumPackageFolder { get { return Path.Combine(NodeModulesFolder, "appium"); } }
        #endregion

        #region Server Settings
        /// <summary>ip address to listen on</summary>
        public string IPAddress { get { return this.IPAddressTextBox.Text; } set { this.IPAddressTextBox.Text = value; } }
        /// <summary>port to listen on</summary>
        public uint Port { get { return Convert.ToUInt32(this.PortTextBox.Value); } set { this.PortTextBox.Value = value; } }
        /// <summary>text displayed on the launch button</summary>
        public string LaunchButtonText { get { return this.LaunchButton.Text; } set { this.LaunchButton.Text = value; } }
        #endregion
    }
}
