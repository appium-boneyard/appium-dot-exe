using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appium.Models.Server
{
	public sealed class PrelauchApplicationArgument : AppiumServerArgument
	{
		private const string CMD_SWITCH = "--pre-launch";

		public PrelauchApplicationArgument()
		{
			_cmdSwitch = CMD_SWITCH;
		}
	}
}
