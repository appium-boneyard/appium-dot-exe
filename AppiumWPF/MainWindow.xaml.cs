using Appium.ViewModels;
using Appium.Views;
using System;
using System.Windows;
using System.Windows.Controls;

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
            _VM = new MainWindowVM(new Action(() => this.Close()));
            Closing += _VM.OnWindowClosing;
            DataContext = _VM;
        }

        /// <summary>
        /// Preference menu item clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreferenceClick(object sender, RoutedEventArgs e)
        {
            Window win = new PreferenceWindow() { DataContext = _VM.PreferenceWindowVM };

            if (null != win)
            {
                win.ShowDialog();
            }
       }

        /// <summary>
        /// Scroll to the bottom of the TextBlock
        /// </summary>
        /// <param name="sender">object who fired this event</param>
        /// <param name="e">NOT USED</param>
        private void _ScrollToBottom(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            TextBlock tb;
            ScrollViewer sv;
            if (null == (tb = sender as TextBlock))
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
    }
}
