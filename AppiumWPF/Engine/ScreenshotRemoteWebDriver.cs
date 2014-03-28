using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace Appium.Engine
{
    /// <summary>overrides remote web driver in order to allow screenshots</summary>
    public class ScreenshotRemoteWebDriver : RemoteWebDriver, ITakesScreenshot
    {
        #region Constructor
        /// <summary>constructor</summary>
        /// <param name="remoteAddress">web driver remote server address</param>
        /// <param name="capabilities">desired capabilities</param>
        public ScreenshotRemoteWebDriver(Uri remoteAddress, ICapabilities capabilities) : base(remoteAddress, capabilities) { }
        #endregion Constructor

        #region Public Methods
        /// <summary>takes a screenshot</summary>
        /// <returns>a screenshot</returns>
        public Screenshot GetScreenshot()
        {
            Response screenshotResponse = this.Execute(DriverCommand.Screenshot, null);
            string base64 = screenshotResponse.Value.ToString();
            return new Screenshot(base64);
        }
        #endregion Public Methods
    }
}
