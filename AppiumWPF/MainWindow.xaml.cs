using Appium.ViewModels;
using Appium.Views;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;
using System.Collections.ObjectModel;

namespace Appium
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowVM _VM;

        ObservableCollection<String> _output;

        public MainWindow()
        {
            InitializeComponent();
            _VM = new MainWindowVM();
            Closing += _VM.OnWindowClosing;
            DataContext = _VM;

            _VM.OutputLines.CollectionChanged += OutputLines_CollectionChanged;
            appendLines(_VM.OutputLines.ToList());
        }

        private void OutputLines_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:

                    if (e.NewItems != null)
                    {
                        appendLines(e.NewItems);
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    this.Dispatcher.Invoke(() =>
                   {
                       rtbOutput.Clear();
                   });
                    break;
            }
        }

        private void appendLines(System.Collections.IList lines)
        {
            try
            {
                this.Dispatcher.Invoke(() =>
                {
                    foreach (String s in lines)
                    {
                        rtbOutput.AppendText(s + "\n");
                    }
                    double offset = rtbOutput.ExtentHeight - rtbOutput.VerticalOffset;

                    // Only scroll further if the window is at the bottom
                    if (offset <= rtbOutput.ViewportHeight)
                    {
                        rtbOutput.ScrollToEnd();
                    }
                });
            }
            catch (TaskCanceledException)
            {
                // Ignore this, it seems to only happen at shutdown
            }
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

            // open it with show to allow for both appium and inspector windows to be moveable and clickable
            win.Show();
            _VM.IsInspectorWindowOpen = true;
            win.Closed += _VM.OnInspectorWindowClosed;
        }
#endregion Call Back Method
    }
}
