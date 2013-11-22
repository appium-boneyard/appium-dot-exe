using System.IO;
using System.Windows.Forms;

namespace Appium.PreferencesWindow
{
    public partial class PreferencesForm : Form, IPreferencesView
    {
		public PreferencesForm()
		{
			InitializeComponent();
		}

        /// <summary>called when the browse button for external nodejs binary is clicked</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ExternalNodeJSBinaryBrowseButton_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog chooseNodeJSBinaryDialog = new OpenFileDialog();
            if (File.Exists(this.ExternalNodeJSBinaryTextBox.Text))
            {
				chooseNodeJSBinaryDialog.InitialDirectory = Path.GetDirectoryName(this.ExternalNodeJSBinaryTextBox.Text);
            }
            chooseNodeJSBinaryDialog.CheckFileExists = true;
            chooseNodeJSBinaryDialog.Multiselect = false;
            chooseNodeJSBinaryDialog.Filter = "NodeJS Binary (node.exe)|node.exe";
            chooseNodeJSBinaryDialog.Title = "Select Your NodeJS Binary";
            var result = chooseNodeJSBinaryDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
				this.ExternalNodeJSBinaryTextBox.Text = chooseNodeJSBinaryDialog.FileName;
            }
        }

        /// <summary>called when the browse button for external appium package is clicked</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ExternalAppiumPackageBrowse_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog chooseAppiumPackageDialog = new OpenFileDialog();
            if (Directory.Exists(this.ExternalAppiumPackageTextBox.Text))
            {
				chooseAppiumPackageDialog.InitialDirectory = this.ExternalAppiumPackageTextBox.Text;
            }
            chooseAppiumPackageDialog.CheckPathExists = true;
            chooseAppiumPackageDialog.Multiselect = false;
            chooseAppiumPackageDialog.Title = "Select Your Appium Package Folder";
            var result = chooseAppiumPackageDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
				this.ExternalAppiumPackageTextBox.Text = chooseAppiumPackageDialog.FileName;
            }
        }

		private void ResetApplicationCheckbox_CheckedChanged(object sender, System.EventArgs e)
		{

		}

		private void CheckForUpdatesCheckbox_CheckedChanged(object sender, System.EventArgs e)
		{

		}

		public void BindPresentationModel(PreferencesPModel pModel)
		{
			if (preferencesPModelBindingSource.Count > 0)
				preferencesPModelBindingSource.Clear();

			preferencesPModelBindingSource.Add(pModel); 
		}

		public void Open()
		{
			if (!this.Visible) 
				this.Show();
		}
	}
}
