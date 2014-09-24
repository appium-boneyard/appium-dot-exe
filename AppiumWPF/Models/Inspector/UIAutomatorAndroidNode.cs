using System;
using System.Text;
using System.Xml;

namespace Appium.Models.Inspector
{
    /// <summary>
    /// Represents tree node of the app building blocks
    /// </summary>
    class UIAutomatorAndroidNode : AbstractNode
    {
        #region Private Member Variables
        private string _Index;
        private string _Text;
        private string _ResourceId;
        private string _Class;
        private string _Package;
        private string _ContentDescription;
        private bool _Checkable;
        private bool _Checked;
        private bool _Clickable;
        private bool _Focusable;
        private bool _Focused;
        private bool _Scrollable;
        private bool _LongClickable;
        private bool _Password;
        private bool _Selected;
        private string _Bounds;
        #endregion Private Member Variables

        //private Lazy<Rectangle> _Outline;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reader">XmlReader pointing to an Xml Node representing an Android node</param>
        public UIAutomatorAndroidNode(XmlReader reader)
            : base(reader)
        {
            _Index = reader.GetAttribute("index");
            _Text = reader.GetAttribute("text");
            _ResourceId = reader.GetAttribute("resource-id");
            _Class = reader.GetAttribute("class");
            _Package = reader.GetAttribute("package");
            _ContentDescription = reader.GetAttribute("content-desc");
            _Checkable = bool.Parse(reader.GetAttribute("checkable") ?? "false");
            _Checked = bool.Parse(reader.GetAttribute("checked") ?? "false");
            _Clickable = bool.Parse(reader.GetAttribute("clickable") ?? "false");
            _Focusable = bool.Parse(reader.GetAttribute("focusable") ?? "false");
            _Focused = bool.Parse(reader.GetAttribute("focused") ?? "false");
            _Scrollable = bool.Parse(reader.GetAttribute("scrollable") ?? "false");
            _LongClickable = bool.Parse(reader.GetAttribute("long-clickable") ?? "false");
            _Password = bool.Parse(reader.GetAttribute("password") ?? "false");
            _Selected = bool.Parse(reader.GetAttribute("selected") ?? "false");
            _Bounds = reader.GetAttribute("bounds");
        }
        #endregion Constructor

        /// <summary>
        /// Name to be displayed in the tree view
        /// </summary>
        /// <returns>Name representation of the node</returns>
        public override string GetDisplayName()
        {
            return String.Format("({0}) {1}{2}{3} {4}", _Index, _Class, string.IsNullOrWhiteSpace(_Text) ? "" : ":", _Text, _Bounds);
        }

        /// <summary>
        /// Gets the Name or Id of the Node (in this case the Id from Android)
        /// </summary>
        /// <returns></returns>
        public override string GetNameId()     // Resource Id for Android
        {
            return _ResourceId;
        }

        /// <summary>
        /// Details for the node
        /// </summary>
        /// <returns>detailed string representing this node</returns>
        public override string GetDetails()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("index: " + _Index ?? "");
            sb.AppendLine("text: " + _Text ?? "");
            sb.AppendLine("class: " + _Class ?? "");
            sb.AppendLine("content-desc: " + _ContentDescription ?? "");
            sb.AppendLine("package: " + _Package ?? "");
            sb.AppendLine("resource id: " + _ResourceId ?? "");
            sb.AppendLine("checkable: " + _Checkable.ToString().ToLower());
            sb.AppendLine("checked: " + _Checked.ToString().ToLower());
            sb.AppendLine("clickable: " + _Clickable.ToString().ToLower());
            sb.AppendLine("enabled: " + _Enabled_.ToString().ToLower());
            sb.AppendLine("focusable: " + _Focusable.ToString().ToLower());
            sb.AppendLine("focused: " + _Focused.ToString().ToLower());
            sb.AppendLine("scrollable: " + _Scrollable.ToString().ToLower());
            sb.AppendLine("long-clickable: " + _LongClickable.ToString().ToLower());
            sb.AppendLine("is password: " + _Password.ToString().ToLower());
            sb.AppendLine("selected: " + _Selected.ToString().ToLower());
            //sb.AppendFormat("location: [{0},{1}]", _Outline.Value.Location.X, _Outline.Value.Location.Y);
            //sb.AppendLine();
            //sb.AppendFormat("size: {0}x{1}", _Outline.Value.Width, _Outline.Value.Height);
            //sb.AppendLine();
            return sb.ToString();
        }

        //public Rectangle GetOutline()
        //{
        //    return _Outline.Value;
        //}
    }
}
