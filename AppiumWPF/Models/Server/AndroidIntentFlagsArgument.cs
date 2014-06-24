
namespace Appium.Models.Server
{
    public sealed class AndroidIntentFlagsArgument : AppiumServerStringArgument
    {
        private const string _CommandSwitch = "--intent-flags";

        public AndroidIntentFlagsArgument(string flags)
        {
            Init(_CommandSwitch, flags);
        }
    }
}
