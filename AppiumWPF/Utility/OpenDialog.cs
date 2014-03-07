using System.IO;
using System.Windows.Forms;

namespace Appium.Utility
{
    class OpenDialog
    {
        /// <summary>
        /// Opens a file dialog box to get a file 
        /// </summary>
        /// <param name="filePath">current file path location, if null or not found, will open in default location</param>
        /// <param name="title">title of the file dialog window</param>
        /// <param name="defaultExt">default extensions to show</param>
        /// <param name="filters">filter strings that specifies the file types and descriptions to display in the dialog</param>
        /// <returns>path of the file if successful, otherwise null</returns>
        public static string OpenFileDialog(string filePath, string title, string defaultExt, string filters)
        {
            string filename = null;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            try
            {
                dlg.InitialDirectory = Path.GetDirectoryName(filePath);
            }
            catch
            {
                // do nothing since the given path was invalid 
            }

            dlg.Title = title;
            dlg.Multiselect = false;
            dlg.CheckFileExists = true;

            // default file type extension
            dlg.DefaultExt = defaultExt;
            // Filter the files by extension
            dlg.Filter = filters;

            // Process open file dialog box results
            if (true == dlg.ShowDialog())
            {
                filename = dlg.FileName;
            }

            return filename;
        }

        /// <summary>
        /// Windows Forms folder browser dialog 
        /// </summary>
        /// <param name="description">Description of what the folder dialog is intended for</param>
        /// <returns>a path to the folder chosen if picked, else null</returns>
        public static string OpenFolderDialog(string description)
        {
            string filename = null;
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            dlg.ShowNewFolderButton = false;
            dlg.Description = description;

            // Process open file dialog box results
            if (DialogResult.OK == dlg.ShowDialog())
            {
                filename = dlg.SelectedPath;
            }

            return filename;
        }
    }
}
