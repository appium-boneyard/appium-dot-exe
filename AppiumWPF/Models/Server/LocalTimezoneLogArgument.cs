namespace Appium.Models.Server
{
    /// <summary>
    /// Argument class for the Local time zone
    /// </summary>
    class LocalTimezoneLogArgument : AppiumServerArgument
    {
        private const string CMD_SWITCH = "--local-timezone";

        public LocalTimezoneLogArgument()
        {
            _cmdSwitch = CMD_SWITCH;
        }
    }
}
