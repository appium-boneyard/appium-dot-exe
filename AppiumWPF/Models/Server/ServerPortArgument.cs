
namespace AppiumWPF.Models.Server
{
	public sealed class ServerPortArgument : AppiumServerUintArgument
	{
		private const string CMD_SWITCH = "--port";

		public ServerPortArgument(uint port)
		{
			Init(CMD_SWITCH, port);
		}
	}
}
