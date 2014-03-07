using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppiumWPF.Models.Server
{
	public sealed class NoResetArgument : AppiumServerArgument
	{
		private const string CMD_SWITCH = "--no-reset";

		public NoResetArgument()
		{
			_cmdSwitch = CMD_SWITCH;
		}
	}
}
