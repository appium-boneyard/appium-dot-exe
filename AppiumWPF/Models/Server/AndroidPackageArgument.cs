
namespace AppiumWPF.Models.Server
{
	public sealed class AndroidPackageArgument : AppiumServerStringArgument
	{
		private const string CMD_SWITCH = "--app-pkg";

		public AndroidPackageArgument(string packageName)
		{
			Init(CMD_SWITCH, packageName);
		}
	}
}
