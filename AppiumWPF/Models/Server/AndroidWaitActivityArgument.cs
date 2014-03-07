
namespace AppiumWPF.Models.Server
{
	public sealed class AndroidWaitActivityArgument : AppiumServerStringArgument
	{
		private const string CMD_SWITCH = "--app-wait-activity";

		public AndroidWaitActivityArgument(string activityName)
		{
			Init(CMD_SWITCH, activityName);
		}
	}
}
