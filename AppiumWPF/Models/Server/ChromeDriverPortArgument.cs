
namespace Appium.Models.Server
{
    public sealed class ChromeDriverPortArgument : AppiumServerUintArgument
    {
        private const string CMD_SWITCH = "--chromedriver-port";

        public ChromeDriverPortArgument(uint arguments)
        {
            Init(CMD_SWITCH, arguments);
        }

    }
}
