
namespace Appium.Models.Server
{
    public sealed class AndroidIntentActionArgument : AppiumServerStringArgument
    {
        private const string _CommandSwitch = "--intent-action";

        public AndroidIntentActionArgument(string action)
        {
            Init(_CommandSwitch, action);
        }
    }
}
