
namespace Appium.Models.Server
{
    public sealed class LocaleArgument : AppiumServerStringArgument
    {
        private const string _CommandSwitch = "--locale";

        public LocaleArgument(string locale)
        {
            Init(_CommandSwitch, locale);
        }
    }
}
