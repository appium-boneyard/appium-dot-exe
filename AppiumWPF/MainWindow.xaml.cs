using Appium.ViewModels;
using Appium.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Appium
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowVM _VM;

        public MainWindow()
        {
            InitializeComponent();
            _VM = new MainWindowVM();
            Closing += _VM.OnWindowClosing;
            DataContext = _VM;
        }

        #region Call Back Method
        /// <summary>
        /// Inspector menu item clicked - open the inspector window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _InspectorClick(object sender, RoutedEventArgs e)
        {
            Window win = new InspectorWindow() { DataContext = new InspectorWindowVM(_VM.Settings) };
            if (null != win)
            {
                // open it with show to allow for both appium and inspector windows to be moveable and clickable
                win.Show();
                _VM.IsInspectorWindowOpen = true;
                win.Closed += _VM.OnInspectorWindowClosed;
            }
        }

        /// <summary>
        /// Scroll to the bottom of the TextBlock
        /// </summary>
        /// <param name="sender">object who fired this event</param>
        /// <param name="e">NOT USED</param>
        private void _ScrollToBottom(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            TextBox tb;
            ScrollViewer sv;
            if (null == (tb = sender as TextBox))
            {
                // do nothing
            }
            else if (null == (sv = tb.Parent as ScrollViewer))
            {
                // do nothing
            }
            else
            {
                sv.ScrollToBottom();
            }
        }
        #endregion Call Back Method
    }
}
