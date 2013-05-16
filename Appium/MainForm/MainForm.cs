using System;
using System.Windows.Forms;

namespace Appium.MainWindow
{
    /// <summary>the main form</summary>
    public partial class MainForm : Form
    {
        private Controller _Controller;
        private Model _Model;

        /// <summary>constructor</summary>
        public MainForm()
        {
            // initialize
            _Model = new Model(this);
            _Controller = new Controller(this._Model);
            InitializeComponent();

            // install event handlers
            this.Load += new EventHandler(this._Controller.MainForm_Load);
            this.FileMenuExitItem.Click += new EventHandler(this._Controller.FileMenuExitItem_Click);
            this.FileMenuPreferencesItem.Click += new EventHandler(this._Controller.FileMenuPreferencesItem_Click);
            this.LaunchButton.Click += new EventHandler(this._Controller.LaunchButton_Click);
            this.ApplicationPathBrowseButton.Click += new EventHandler(this._Controller.AppPathBrowseButton_Click);
            this.FormClosing += new FormClosingEventHandler(this._Controller.FileMenuExitItem_Click);
        }
    }
}
