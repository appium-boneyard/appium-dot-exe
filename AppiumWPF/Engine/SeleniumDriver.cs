using Appium.Models;
using Appium.ViewModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;

namespace Appium.Engine
{
    class SeleniumDriver
    {
        #region Private Member Variables
        /// <summary>Lock object when accessing the driver object so we do not try to start/stop/use simultaneously</summary>
        private readonly object _Locket = new object();

        /// <summary>Remote Web Driver - our selenium driver</summary>
        /// <remarks>null driver means it's stopped</remarks>
        private RemoteWebDriver _Driver;

        /// <summary>Application Settings</summary>
        private readonly IAppiumAppSettings _Settings;
        #endregion Private Member Variables

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings"></param>
        public SeleniumDriver(IAppiumAppSettings settings)
        {
            _Settings = settings;
        }
        #endregion Constructor

        #region Properties
        /// <summary>Is the Driver started yet</summary>
        public bool IsStarted { get { return null != _Driver; } }
        #endregion Properties

        #region Public Methods
        /// <summary>
        /// Start the Selenium Engine, if it hasn't been started
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns>true if started already or start has been completed, false if not started or is started</returns>
        public bool Start(out string errorMessage)
        {
            bool retVal = false;
            errorMessage = null;

            lock (_Locket)
            {
                if (null == _Driver)
                {
                    try
                    {
                        Dictionary<string, object> capsDef = new Dictionary<string, object>();

                        // Only set automation name if it isn't equal to the default
                        if (_Settings.AutomationName != "Appium")
                        {
                            capsDef.Add("automationName", _Settings.AutomationName);
                        }

                        if (_Settings.UseDeviceName && _Settings.DeviceName != "")
                        {
                            capsDef.Add("deviceName", _Settings.DeviceName);
                        }

                        if (!_Settings.UseAndroidBrowser)
                        {
                            if (_Settings.UseApplicationPath && _Settings.ApplicationPath != "")
                            {
                                capsDef.Add("app", _Settings.ApplicationPath);
                            }

                            if (_Settings.UseAndroidActivity && _Settings.AndroidActivity != "")
                            {
                                capsDef.Add("appActivity", _Settings.AndroidActivity);
                            }

                            if (_Settings.UseAndroidPackage && _Settings.AndroidPackage != "")
                            {
                                capsDef.Add("appPackage", _Settings.AndroidPackage);
                            }

                            if (_Settings.UseAndroidWaitForActivity && _Settings.AndroidWaitForActivity != "")
                            {
                                capsDef.Add("appWaitActivity", _Settings.AndroidWaitForActivity);
                            }

                            if (_Settings.UseAndroidWaitForPackage && _Settings.AndroidWaitForPackage != "")
                            {
                                capsDef.Add("appWaitPackage", _Settings.AndroidWaitForPackage);
                            }
                        }
                        else
                        {
                            capsDef.Add("browserName", _Settings.AndroidBrowser);
                        }

                        if (_Settings.UseAndroidDeviceReadyTimeout && _Settings.AndroidDeviceReadyTimeout.ToString() != "")
                        {
                            capsDef.Add("deviceReadyTimeout", _Settings.AndroidDeviceReadyTimeout.ToString());
                        }

                        if (_Settings.UseCoverageClass && _Settings.CoverageClass != "")
                        {
                            capsDef.Add("androidCoverage", _Settings.CoverageClass);
                        }

                        if (_Settings.UseAndroidIntentAction && _Settings.AndroidIntentAction != "")
                        {
                            capsDef.Add("intentAction", _Settings.AndroidIntentAction);
                        }

                        if (_Settings.UseAndroidIntentCategory && _Settings.AndroidIntentCategory != "")
                        {
                            capsDef.Add("intentCategory", _Settings.AndroidIntentCategory);
                        }

                        if (_Settings.UseAndroidIntentFlags && _Settings.AndroidIntentFlags != "")
                        {
                            capsDef.Add("intentFlags", _Settings.AndroidIntentFlags);
                        }

                        if (_Settings.UseAndroidIntentArguments && _Settings.AndroidIntentArguments != "")
                        {
                            capsDef.Add("optionalIntentArguments", _Settings.AndroidIntentArguments);
                        }

                        // Include the platform if any of the capabilities were set
                        if (capsDef.Count != 0 && _Settings.PlatformName != "")
                        {
                            capsDef.Add("platformName", _Settings.PlatformName);
                        }

                        ICapabilities capabilities = new DesiredCapabilities(capsDef);
                        string uri = string.Format("http://{0}:{1}/wd/hub", _Settings.IPAddress, _Settings.Port);
                        _Driver = new ScreenshotRemoteWebDriver(new Uri(uri), capabilities);
                        // add increased timeout for inspector connection
                        Dictionary<string, int> args = new Dictionary<string, int>();
                        //args.Add("timeout", 900);
                        //_Driver.ExecuteScript("mobile: setCommandTimeout", new object[] { args });
                        retVal = true;
                    }
                    catch (Exception e)
                    {
                        errorMessage = e.Message;
                    }
                }
                else
                {
                    errorMessage = "Already started";
                    retVal = true;
                }
            }

            return retVal;
        }

        /// <summary>Stop the Selenium Engine</summary>
        /// <param name="errorMessage">returns an error if there was one, else null</param>
        /// <returns>true if it worked, false otherwise</returns>
        public bool Stop(out string errorMessage)
        {
            bool retVal = false;
            errorMessage = null;
            lock (_Locket)
            {
                if (null != _Driver)
                {
                    _Driver.Quit();
                    _Driver = null;
                    retVal = true;
                }
                else
                {
                    errorMessage = "Already stopped";
                }
            }
            return retVal;
        }


        /// <summary>
        /// Refresh the Selenium DOM's pagesource
        /// </summary>
        /// <returns>the refreshed DOM pagesource</returns>
        public string GetPageSource()
        {
            string data = null;
            lock (_Locket)
            {
                if (null != _Driver)
                {
                    try
                    {
                        data = _Driver.PageSource;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error getting page source: {0}", e.Message);
                        return null;
                    }
                }
            }
            return data;
        }

        /// <summary>
        /// Get the Screenshot from the selenium client
        /// </summary>
        /// <returns>byte array of the screenshot image if available, else null</returns>
        public byte[] GetScreenshot()
        {
            byte[] retVal = null;
            lock (_Locket)
            {
                if (null != _Driver)
                {
                    Screenshot screenshot = ((ITakesScreenshot)_Driver).GetScreenshot();

                    if (screenshot != null)
                    {
                        retVal = screenshot.AsByteArray;
                    }
                }
            }
            return retVal;
        }

        public bool SendKeys(UIAutomatorNodeVM node, string value)
        {
            var button = _Driver.FindElementByName(node.Id);
            try
            {
                button.SendKeys(value);
                return true;
            }
            catch (Exception ex)
            {    // Probably this item does not support keyboard input (the return from Appium 1.0.2 is too generic to recognise difference between unclickable and a system error).
                Console.WriteLine("Failed to execute click : {0}", ex.Message);
            }
            return false;
        }

        public bool Tap(UIAutomatorNodeVM node)
        {
            var button = _Driver.FindElementByName(node.Id);
            bool SuccessfullTap = false;
            try
            {
                button.Click();
                SuccessfullTap = true;
            }
            catch (Exception ex)
            {    // It's probable this item does not support tapping.
                Console.WriteLine("Failed to tap element : {0} (will try alternate method)", ex.Message);
            }
            if (!SuccessfullTap)
            {
                try
                {
                    // TODO: Alternate method of tapping.
                    /*                    _Driver.Driver.Mouse.Click( button.Location.X);
                                        d.SingleTap(button);
                                        _ExecuteRefreshCommand();
                                        SuccessfullTap = true;  */
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to use mouse to Tap element : {0}", ex.Message);
                }
            }
            return SuccessfullTap;
        }


        #endregion Public Methods

    }
}
