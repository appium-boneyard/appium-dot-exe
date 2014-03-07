using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appium.Models.Server
{
	public sealed class BreakOnAppStartArgument : AppiumServerArgument
	{
		private const string CMD_SWITCH = "--debug-brk";

		public BreakOnAppStartArgument(){
			_cmdSwitch = CMD_SWITCH;
		}
	}
}
