using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppiumWPF.Models.Server
{
	public interface IAppiumServerArgument
	{
		string CmdSwitch { get; }
		string AssembleCommandLine();
	}
}
