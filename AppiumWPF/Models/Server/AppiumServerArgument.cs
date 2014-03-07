using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appium.Models.Server
{
	public abstract class AppiumServerArgument : IAppiumServerArgument
	{
		protected string _cmdSwitch;
		public string CmdSwitch
		{
			get { return _cmdSwitch; }
		}

		public virtual string AssembleCommandLine()
		{
			return CmdSwitch;
		}

		public override string ToString()
		{
			return AssembleCommandLine();
		}

	}
}
