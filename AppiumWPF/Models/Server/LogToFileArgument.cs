
namespace Appium.Models.Server
{
    public sealed class LogToFileArgument : AppiumServerStringArgument
    {        
        private const string CMD_SWITCH = "--log";

        public LogToFileArgument(string arguments)
        {
            Init(CMD_SWITCH, arguments);
        }
    }
}
