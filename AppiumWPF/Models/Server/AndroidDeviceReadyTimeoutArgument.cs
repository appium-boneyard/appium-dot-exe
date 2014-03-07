
namespace Appium.Models.Server
{
	public sealed class AndroidDeviceReadyTimeoutArgument : AppiumServerUintArgument
	{
		private const string CMD_SWITCH = "--device-ready-timeout";

		public AndroidDeviceReadyTimeoutArgument(uint timeout)
		{
			Init(CMD_SWITCH, timeout);
		}
	}
}
