using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppiumWPF.Models.Server
{
	public sealed class QuietLoggingArgument : AppiumServerArgument
	{
		private const string CMD_SWITCH = "--quiet";

		public QuietLoggingArgument()
		{
			_cmdSwitch = CMD_SWITCH;
		}
	}
}
