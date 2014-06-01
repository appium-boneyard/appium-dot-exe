
namespace Appium.Models.Server
{
    public sealed class AndroidBrowserArgument : AppiumServerStringArgument
    {
        private const string _CommandSwitch = "--browser-name";

        public AndroidBrowserArgument(string browser)
        {
            Init(_CommandSwitch, browser);
        }
    }
}
