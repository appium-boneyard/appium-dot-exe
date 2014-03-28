using System.Collections.Generic;
using System.Drawing;

namespace Appium.Models.Inspector
{
	public interface INode
	{
		string GetDisplayName();
		List<INode> GetChildren();
		string GetDetails();
        //Rectangle GetOutline();
	}
}
