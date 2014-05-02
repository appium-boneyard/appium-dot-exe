
namespace Appium.Models.Server
{
    public sealed class OverrideExistingSessionArgument : AppiumServerArgument
    {
        private const string CMD_SWITCH = "--session-override";

        public OverrideExistingSessionArgument()
        {
            _cmdSwitch = CMD_SWITCH;
        }

    }
}
