
namespace Appium.Models.Server
{
	public sealed class AndroidActivityArgument : AppiumServerStringArgument
	{
		private const string CMD_SWITCH = "--app-activity";

		public AndroidActivityArgument(string activityName)
		{
			Init(CMD_SWITCH, activityName);
		}
	}
}
