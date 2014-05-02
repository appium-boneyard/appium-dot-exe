
namespace Appium.Models.Server
{
    public sealed class AVDArgsToLaunchArgument : AppiumServerStringArgument
    {
        private const string CMD_SWITCH = "--avd-args";

        public AVDArgsToLaunchArgument(string arguments)
        {
            Init(CMD_SWITCH, arguments);
        }
    }
}
