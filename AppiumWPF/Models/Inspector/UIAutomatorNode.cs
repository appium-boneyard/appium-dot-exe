using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

// ASTRO: FIX this class


namespace Appium.Models.Inspector
{
    /// <summary>
    /// Represents tree node of the app building blocks
    /// </summary>
    public class UIAutomatorNode : INode
    {
        [JsonProperty(PropertyName = "@index")]
        public string Index;
        [JsonProperty(PropertyName = "@class")]
        public string Class;
        [JsonProperty(PropertyName = "@text")]
        public string Text;
        [JsonProperty(PropertyName = "@content-desc")]
        public string ContentDescription;
        [JsonProperty(PropertyName = "@bounds")]
        public string Bounds;
        [JsonProperty(PropertyName = "@package")]
        public string Package;
        [JsonProperty(PropertyName = "@enabled")]
        public bool IsEnabled;
        [JsonProperty(PropertyName = "@selected")]
        public bool IsSelected;
        [JsonProperty(PropertyName = "@focused")]
        public bool IsFocused;
        
        /// <summary>
        /// Only for parsing purposes, use <see cref="UIAutomatorNode.Children" /> instead.
        /// </summary>
        [JsonProperty(PropertyName = "node")]
        public JContainer SubNode;
        /// <summary>
        /// Node descendants representation
        /// </summary>
        [JsonIgnore]
        private List<UIAutomatorNode> _Children
        {
            get
            {
                List<UIAutomatorNode> result = new List<UIAutomatorNode>();
                if (SubNode == null)
                {
                    return result;
                }

                if (SubNode.Type.Equals(JTokenType.Array))
                {
                    result = SubNode.ToObject<List<UIAutomatorNode>>();
                }
                else if (SubNode.Type.Equals(JTokenType.Object))
                {
                    UIAutomatorNode n = SubNode.ToObject<UIAutomatorNode>();
                    result.Add(n);
                }
                return result;
            }
        }

        //private Lazy<Rectangle> _Outline;

        public UIAutomatorNode()
        {
            // TODO: return this to original part
            //_Outline = null;
            //_Outline = new Lazy<Rectangle>(() =>
            //{
            //    if (String.IsNullOrEmpty(Bounds))
            //        return new Rectangle();

            //    // parse bounds [0,0][480,800]
            //    Regex reg = new Regex(@"\[(\d+),(\d+)\]");
            //    MatchCollection mc = reg.Matches(Bounds);

            //    // failed to match bounds definition
            //    if (mc.Count != 2)
            //        return new Rectangle();

            //    // we have valid outline definition
            //    Point topLeft = new Point(Int32.Parse(mc[0].Groups[1].Value),
            //        Int32.Parse(mc[0].Groups[2].Value));
            //    Point bottomRight = new Point(Int32.Parse(mc[1].Groups[1].Value),
            //        Int32.Parse(mc[1].Groups[2].Value));
                
            //    return new Rectangle(topLeft, new Size(bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y));
            //});

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
            sb.AppendLine("package: " + Package ?? "");
            //sb.AppendLine("enabled: " + IsEnabled.ToString().ToLower());
            //sb.AppendLine("focused: " + IsFocused.ToString().ToLower());
            //sb.AppendLine("selected: " + IsSelected.ToString().ToLower());
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
