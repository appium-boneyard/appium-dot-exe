using System.Text;
using System.Xml;

namespace Appium.Models.Inspector
{
    /// <summary>
    /// Node representing the xml from the page source
    /// </summary>
    class UIAutomatorAppleNode : AbstractNode
    {
        #region Private Member Variable
        private string _Type;
        private string _Name;
        private string _Label;
        private string _Value;
        private string _Dom;
        private bool _Valid;
        private bool _Visible;
        private string _Hint;
        private string _Path;
        private int? _XValue;
        private int? _YValue;
        private int? _Width;
        private int? _Height;
        #endregion Private Member Variable

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reader">XmlReader pointing to an Xml Node representing an Apple node</param>
        public UIAutomatorAppleNode(XmlReader reader)
            : base(reader)
        {
            _Type = reader.Name;
            _Name = reader.GetAttribute("name");
            _Label = reader.GetAttribute("label");
            _Value = reader.GetAttribute("value");
            _Dom = reader.GetAttribute("dom");
            _Valid = bool.Parse(reader.GetAttribute("valid") ?? "false");
            _Visible = bool.Parse(reader.GetAttribute("visible") ?? "false");
            _Hint = reader.GetAttribute("hint");
            _Path = reader.GetAttribute("path");
            int tmpVal;
            _XValue = int.TryParse(reader.GetAttribute("x"), out tmpVal) ? tmpVal : (int?)null;
            _YValue = int.TryParse(reader.GetAttribute("y"), out tmpVal) ? tmpVal : (int?)null;
            _Width = int.TryParse(reader.GetAttribute("width"), out tmpVal) ? tmpVal : (int?)null;
            _Height = int.TryParse(reader.GetAttribute("height"), out tmpVal) ? tmpVal : (int?)null;
        }
        #endregion Constructor

        #region Public Methods
        /// <summary>
        /// Name to be displayed in the tree view
        /// </summary>
        /// <returns>Display Name</returns>
        public override string GetDisplayName()
        {
            return string.Format("[{0}] {1}", _Type, _Name);
        }

        /// <summary>
        /// Get details for this node
        /// </summary>
        /// <returns>detailed string representing this node</returns>
        public override string GetDetails()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("name: " +  _Name ?? "");
            sb.AppendLine("type: " + _Type ?? "");
            sb.AppendLine("label: " + _Label ?? "");
            sb.AppendLine("value: " + _Value ?? "");
            sb.AppendLine("DOM: " + _Dom ?? "");
            sb.AppendLine("valid: " + _Valid.ToString().ToLower());
            sb.AppendLine("visible: " + _Visible.ToString().ToLower());
            sb.AppendLine("hint: " + _Hint ?? "");
            sb.AppendLine("path: " + _Path ?? "");
            sb.AppendLine("x: " + _XValue ?? "");
            sb.AppendLine("y: " + _YValue ?? "");
            sb.AppendLine("width: " + _Width ?? "");
            sb.AppendLine("height: " + _Height ?? "");
            return sb.ToString();
        }
        #endregion Public Methods
    }
}
