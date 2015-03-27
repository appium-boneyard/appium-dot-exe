using System.Windows;
using System.Windows.Documents;

namespace AppiumFake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Use the navigate url to open the browser to the NavigateUrl specified 
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routed event argument which contains the navigate url</param>
        private void Hyperlink_OnClick(object sender, RoutedEventArgs e)
        {
            Hyperlink linkUri = null;

            if (null == e)
            {
                // do nothing
            }
            else if (null == (linkUri = e.OriginalSource as Hyperlink))
            {
                // do nothing
            }
            else if (null == linkUri.NavigateUri)
            {
                // do nothing
            }
            else
            {
                // go to the url specified
                System.Diagnostics.Process.Start(linkUri.NavigateUri.ToString());
            }
        }
    }
}
