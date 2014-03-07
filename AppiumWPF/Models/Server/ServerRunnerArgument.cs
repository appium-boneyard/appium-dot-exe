
namespace AppiumWPF.Models.Server
{
	public sealed class ServerRunnerArgument : AppiumServerArgument
	{
		private const string CMD_SWITCH = "lib\\server\\main.js";

		public ServerRunnerArgument()
		{
			_cmdSwitch = CMD_SWITCH;
		}		
	}
}
