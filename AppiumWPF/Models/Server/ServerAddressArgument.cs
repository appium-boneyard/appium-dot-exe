
namespace AppiumWPF.Models.Server
{
	public sealed class ServerAddressArgument : AppiumServerStringArgument
	{
		private const string CMD_SWITCH = "--address";

		public ServerAddressArgument(string ipAddress)
		{
			Init(CMD_SWITCH, ipAddress);
		}
	}
}
