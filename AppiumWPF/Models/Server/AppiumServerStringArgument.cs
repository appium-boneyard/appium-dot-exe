using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppiumWPF.Models.Server
{
	public abstract class AppiumServerStringArgument : AppiumServerArgument
	{
		protected string _value;
		public string Value { get { return _value; } }
		
		protected void Init(string cmdSwitch, string value)
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
