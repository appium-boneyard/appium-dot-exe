using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Appium.Models.Inspector
{
    /// <summary>
    /// Represents tree node of the app building blocks
    /// </summary>
    public class UIAutomatorNode : INode
    {
        public string Index;
        public string Text;
        public string ResourceId;
        public string Class;
        public string Package;
        public string ContentDescription;
        public bool Checkable;
        public bool Checked;
        public bool Clickable;
        public bool Enabled;
        public bool Focusable;
        public bool Focused;
        public bool Scrollable;
        public bool LongClickable;
        public bool Password;
        public bool Selected;
        public string Bounds;

        private List<UIAutomatorNode> _Children = new List<UIAutomatorNode>();
        /// <summary>
        /// Node descendants representation
        /// </summary>
        public List<UIAutomatorNode> Children
        {
            get
            {
                return _Children;
            }
        }

        //private Lazy<Rectangle> _Outline;

        public UIAutomatorNode(XmlReader reader)
        {
            Text = reader.GetAttribute("text");
            Index = reader.GetAttribute("index");
            Class = reader.GetAttribute("class");
            Package = reader.GetAttribute("package");
            ContentDescription = reader.GetAttribute("content-desc");
            Checkable = bool.Parse(reader.GetAttribute("checkable") ?? "false");
            Checked = bool.Parse(reader.GetAttribute("checked") ?? "false");
            Clickable = bool.Parse(reader.GetAttribute("clickable") ?? "false");
            Enabled = bool.Parse(reader.GetAttribute("enabled") ?? "false");
            Focusable = bool.Parse(reader.GetAttribute("focusable") ?? "false");
            Focused = bool.Parse(reader.GetAttribute("focused") ?? "false");
            Scrollable = bool.Parse(reader.GetAttribute("scrollable") ?? "false");
            LongClickable = bool.Parse(reader.GetAttribute("long-clickable") ?? "false");
            Password = bool.Parse(reader.GetAttribute("password") ?? "false");
            Selected = bool.Parse(reader.GetAttribute("selected") ?? "false");
            Bounds = reader.GetAttribute("bounds");
        }

        /// <summary>
        /// Returns name to be displayed in the tree view
        /// </summary>
        /// <returns>Name representation of the node</returns>
        public string GetDisplayName()
        {
            return String.Format("({0}) {1}{2}{3} {4}", Index, Class, String.IsNullOrEmpty(Text) ? "" : ":", Text, Bounds);
        }

        public string GetDetails()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("index: " + Index ?? "");
            sb.AppendLine("text: " + Text ?? "");
            sb.AppendLine("class: " + Class ?? "");
            sb.AppendLine("content-desc: " + ContentDescription ?? "");
            sb.AppendLine("package: " + Package ?? "");
            sb.AppendLine("resource id: " + ResourceId ?? "");
            sb.AppendLine("checkable: " + Checkable.ToString().ToLower());
            sb.AppendLine("checked: " + Checked.ToString().ToLower());
            sb.AppendLine("clickable: " + Clickable.ToString().ToLower());
            sb.AppendLine("enabled: " + Enabled.ToString().ToLower());
            sb.AppendLine("focusable: " + Focusable.ToString().ToLower());
            sb.AppendLine("focused: " + Focused.ToString().ToLower());
            sb.AppendLine("scrollable: " + Scrollable.ToString().ToLower());
            sb.AppendLine("long-clickable: " + LongClickable.ToString().ToLower());
            sb.AppendLine("is password: " + Password.ToString().ToLower());
            sb.AppendLine("selected: " + Selected.ToString().ToLower());
            //sb.AppendFormat("location: [{0},{1}]", _Outline.Value.Location.X, _Outline.Value.Location.Y);
            //sb.AppendLine();
            //sb.AppendFormat("size: {0}x{1}", _Outline.Value.Width, _Outline.Value.Height);
            //sb.AppendLine();
            return sb.ToString();
        }

        public List<INode> GetChildren()
        {
            return new List<INode>(_Children);
        }

        //public Rectangle GetOutline()
        //{
        //    return _Outline.Value;
        //}
    }
}
