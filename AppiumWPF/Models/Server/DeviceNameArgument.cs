
namespace Appium.Models.Server
{
    public sealed class DeviceNameArgument : AppiumServerStringArgument
    {
        private const string _CommandSwitch = "--device-name";

        public DeviceNameArgument(string deviceName)
        {
            Init(_CommandSwitch, "\"" + deviceName + "\"");
        }
    }
}
