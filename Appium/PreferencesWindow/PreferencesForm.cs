using System.Windows.Forms;

namespace Appium.PreferencesWindow
{
    public partial class PreferencesForm : Form
    {
        private MainWindow.Model _Model;
        private PreferencesController _Controller;

        public PreferencesForm(MainWindow.Model model)
        {
            // initialize
            this._Model = model;
            this._Controller = new PreferencesController(this._Model);
            InitializeComponent();

            // install event handlers
            this.ExternalNodeJSBinaryBrowseButton.Click += new System.EventHandler(this._Controller.ExternalNodeJSBinaryBrowseButton_Click);
            this.ExternalAppiumPackageBrowse.Click += new System.EventHandler(this._Controller.ExternalAppiumPackageBrowse_Click);
        }
    }
}
