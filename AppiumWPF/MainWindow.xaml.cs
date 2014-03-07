using Appium.ViewModels;
using Appium.Views;
using System;
using System.Windows;

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

        private void PreferenceClick(object sender, RoutedEventArgs e)
        {
            Window win = new PreferenceWindow() { DataContext = _VM.PreferenceWindowVM };

            if (null != win)
            {
                win.ShowDialog();
            }

        }

        private void MenuItem_DragEnter(object sender, DragEventArgs e)
        {

        }
    }
}
