using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Appium.Engine;
using Appium.Models;
using Appium.Models.Inspector;
using Appium.Utility;
using Appium.ViewModels;
using System.Windows;

namespace Appium.Models.Inspector
{
    /// <summary>
    /// This class wrapps the Node Tree view to allow us to extend it with utility functions
    /// </summary>
    public class NodeTree<T> : ObservableCollection<T>        // ObservableCollection
    {
        public NodeTree()
        {
        }
    }
}
