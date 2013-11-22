
namespace Appium.Models.Server
{
	public sealed class KeepArtifactsArgument : AppiumServerArgument
	{
		private const string CMD_SWITCH = "--keep-artifacts";

		public KeepArtifactsArgument()
		{
			_cmdSwitch = CMD_SWITCH;
		}
	}
}
