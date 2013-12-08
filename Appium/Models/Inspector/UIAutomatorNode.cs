using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

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

		public bool IsValid;
		//public RectInfo Rect;
		public string Rect;
		public string Dom;
		public bool IsVisible;
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
			sb.AppendLine("enabled: " + IsEnabled.ToString().ToLower());
			sb.AppendLine("visible: " + IsVisible.ToString().ToLower());
			sb.AppendLine("valid: " + IsValid.ToString().ToLower());
			//sb.AppendLine("location: " + (this.Rect != null ? "(" + Rect.Origin.X.ToString() + ", " + Rect.Origin.Y.ToString() + ")" : ""));
			//sb.AppendLine("size: " + (this.Rect != null ? "(" + Rect.Size.Width.ToString() + ", " + Rect.Size.Height.ToString() + ")" : ""));
			return sb.ToString();
		}

		public List<INode> GetChildren()
		{
			return new List<INode>(_Children);
		}

		public System.Drawing.Rectangle GetOutline()
		{
			throw new NotImplementedException();
		}
	}
}
