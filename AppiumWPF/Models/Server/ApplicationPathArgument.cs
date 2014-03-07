
namespace AppiumWPF.Models.Server
{
	public sealed class ApplicationPathArgument : AppiumServerStringArgument
	{
		private const string CMD_SWITCH = "--app";

		public ApplicationPathArgument(string appPath)
		{
			Init(CMD_SWITCH, appPath);
		}
	}
}
