
namespace Appium.Models.Server
{
    public sealed class AutomationNameArgument : AppiumServerStringArgument
    {
        private const string _CommandSwitch = "--automation-name";

        public AutomationNameArgument(string automationName)
        {
            Init(_CommandSwitch, automationName);
        }
    }
}
