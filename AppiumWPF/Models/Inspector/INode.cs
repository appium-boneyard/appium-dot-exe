using System.Collections.Generic;
using System.Drawing;

namespace AppiumWPF.Models.Inspector
{
	public interface INode
	{
		string GetDisplayName();
		List<INode> GetChildren();
		string GetDetails();
		Rectangle GetOutline();
	}
}
