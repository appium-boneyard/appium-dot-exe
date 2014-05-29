
namespace Appium.Models.Server
{
    public sealed class PlatformVersionArgument : AppiumServerStringArgument
    {
        private const string _CommandSwitch = "--platform-version";

        public PlatformVersionArgument(string platformVersion)
        {
            Init(_CommandSwitch, platformVersion);
        }
    }
}
