using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appium.Models.Server
{
	public abstract class AppiumServerUintArgument : AppiumServerArgument
	{
		protected uint _value;
		public uint Value { get { return _value; } }
		
		protected void Init(string cmdSwitch, uint value)
		{
			_cmdSwitch = cmdSwitch;
			_value = value;
		}

		public override string AssembleCommandLine()
		{
			return String.Format("{0} {1}",CmdSwitch,_value);
		}
	}
}
