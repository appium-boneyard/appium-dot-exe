
namespace Appium.Models.Server
{
    class ShowTimestampLogArgument : AppiumServerArgument
    {
        private const string CMD_SWITCH = "--log-timestamp";

        public ShowTimestampLogArgument()
        {
            _cmdSwitch = CMD_SWITCH;
        }

    }
}
