
namespace Appium.Models.Server
{
    public sealed class AndroidCoverageArgument : AppiumServerStringArgument
    {
        private const string CMD_SWITCH = "--android-coverage";

        public AndroidCoverageArgument(string arguments)
        {
            Init(CMD_SWITCH, arguments);
        }
    }
}
