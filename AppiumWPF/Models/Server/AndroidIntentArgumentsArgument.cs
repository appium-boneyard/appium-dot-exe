
namespace Appium.Models.Server
{
    public sealed class AndroidIntentArgumentsArgument : AppiumServerStringArgument
    {
        private const string _CommandSwitch = "--intent-args";

        public AndroidIntentArgumentsArgument(string arguments)
        {
            Init(_CommandSwitch, arguments);
        }
    }
}
