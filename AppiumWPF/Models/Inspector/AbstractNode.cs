using System.Collections.Generic;
using System.Xml;

namespace Appium.Models.Inspector
{
    abstract class AbstractNode : INode
    {
        #region Member Variables
        protected bool _Enabled_;
        #endregion Member Variables

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reader"></param>
        protected AbstractNode(XmlReader reader)
        {
            _Enabled_ = bool.Parse(reader.GetAttribute("enabled") ?? "false");
        }
        #endregion Constructor

        #region Public Properties
        private List<INode> _Children = new List<INode>();
        /// <summary>
        /// Node descendants representation
        /// </summary>
        public List<INode> Children
        {
            get { return _Children; }
        }
        #endregion Public Properties


        #region Public Methods
        /// <summary>
        /// Get Display Name
        /// </summary>
        /// <returns>display name</returns>
        public abstract string GetDisplayName();

        /// <summary>
        /// Get Node Details
        /// </summary>
        /// <returns>node details</returns>
        public abstract string GetDetails();

        /// <summary>
        /// Returns a copied list of children
        /// </summary>
        /// <returns>copied list of children</returns>
        public List<INode> GetChildren()
        {
            return new List<INode>(_Children);
        }
        #endregion Public Methods
    }
}
