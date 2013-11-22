
namespace Appium.Models.Server
{
	public sealed class NodeJSDebugArgument : AppiumServerUintArgument
	{
		private const string CMD_SWITCH = "--debug";

		public NodeJSDebugArgument(uint port)
		{
			Init(CMD_SWITCH, port);
		}

		public override string AssembleCommandLine()
		{
			return string.Format("{0}={1}", _cmdSwitch, _value);
		}
	}
}
