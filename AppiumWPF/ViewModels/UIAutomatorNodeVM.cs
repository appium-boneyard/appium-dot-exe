using Appium.Models.Inspector;
using System;
using System.Collections.ObjectModel;
using System.Text;

namespace Appium.ViewModels
{
    public delegate void SelectionChangedEventHandler(UIAutomatorNodeVM node);

    /// <summary>
    /// Base class for all ViewModel classes displayed by TreeViewItems.  
    /// This acts as an adapter between a raw data object and a TreeViewItem.
    /// </summary>
    public class UIAutomatorNodeVM : BaseVM , IDisposable
    {
        #region Data
        private readonly ObservableCollection<UIAutomatorNodeVM> _Children;
        private readonly UIAutomatorNode _Model;
        private SelectionChangedEventHandler _EH;
        // flag: Has Dispose already been called?
        bool disposed = false;
        #endregion // Data

        #region Constructors
        /// <summary>
        /// Create a new tree view item 
        /// </summary>
        /// <param name="parent">link to the parent</param>
        /// <param name="lazyLoadChildren">true to lazy load the children or false otherwise</param>
        public UIAutomatorNodeVM(UIAutomatorNode node, SelectionChangedEventHandler eh)
        {
            _Model = node;
            _EH = eh;
            SelectionChanged += _EH;
            _Children = new ObservableCollection<UIAutomatorNodeVM>();
            _LoadChildren();
        }
        #endregion // Constructors

        #region Destructor
        ~UIAutomatorNodeVM()
        {
            Dispose(false);
        }
        #endregion Destructor

        #region Properties

        /// <summary>
        /// Returns the logical child items of this object.
        /// </summary>
        public ObservableCollection<UIAutomatorNodeVM> Children
        {
            get { return _Children; }
        }

        private bool _IsSelected;
        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                if (value != _IsSelected)
                {
                    _IsSelected = value;
                    FirePropertyChanged(() => IsSelected);
                    OnSelectionChanged();
                }
            }
        }

        ///<summary></summary>
        public string Name
        {
            get { return _Model.GetDisplayName(); }
        }

        #endregion Properties

        #region IDispose Methods
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            // free any other managed objects here
            if (disposing)
            {
                foreach (UIAutomatorNodeVM child in _Children)
                {
                    child.Dispose();
                }

                SelectionChanged -= _EH;
                _EH = null;
            }

            // free any unmanaged objects here

            disposed = true;
        }
        #endregion IDispose Methods

        #region Event Handlers
        public event SelectionChangedEventHandler SelectionChanged;
        /// <summary>
        /// On a selection changed, notify listeners
        /// </summary>
        protected virtual void OnSelectionChanged()
        {
            if (null != SelectionChanged)
            {
                SelectionChanged(this);
            }
        }
        #endregion Event Handlers

        #region Public Methods
        /// <summary>
        /// Get the details of the node
        /// </summary>
        /// <returns></returns>
        public string GetDetails()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_Model.GetDetails());
            return sb.ToString();
        }
        #endregion Public Methods

        #region Private Methods
        /// <summary>
        /// load the children into memory
        /// </summary>
        private void _LoadChildren()
        {
            if (null != _Model)
            {
                foreach (INode node in _Model.GetChildren())
                {
                    if (node is UIAutomatorNode)
                    {
                        Children.Add(new UIAutomatorNodeVM(node as UIAutomatorNode, _EH));
                    }
                }
            }
        }
        #endregion Private Methods

    }
}
