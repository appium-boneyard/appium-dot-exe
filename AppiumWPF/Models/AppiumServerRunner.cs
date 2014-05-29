using Appium.Models.Capability;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Appium.Models.Server
{
    public class AppiumServerRunner
    {
        private string _defaultFilename;
        private string _defaultWorkingDir;

        private List<IAppiumServerArgument> _args;
        /// <summary>
        /// Read only collection of Appium server arguments. As there is specific logic behind setting up
        /// the arguments, if you want to change them, use <see cref="Setup"/> method instead.
        /// </summary>
        public ReadOnlyCollection<IAppiumServerArgument> Arguments { get { return _args.AsReadOnly(); } }

        private string _filename;
        /// <summary>
        /// Filename of the runner to execute
        /// </summary>
        public string Filename { get { return _filename; } }

        private string _workingDir;
        /// <summary>
        /// Working directory that should be used by the runner
        /// </summary>
        public string WorkingDirectory { get { return _workingDir; } }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="nodeJSPath">Path to default nodeJs exe file</param>
        /// <param name="workingDir">Default working directory</param>
        /// <param name="settings">Appium server settings to be used</param>
        public AppiumServerRunner(string nodeJSPath, string workingDir, IAppiumServerSettings settings)
        {
            //set defaults
            _defaultFilename = nodeJSPath;
            _defaultWorkingDir = workingDir;
            _args = new List<IAppiumServerArgument>();
            // setup runner
            Setup(settings);
        }

        /// <summary>
        /// Sets up the runner acording to given <paramref name="settings"/>.
        /// </summary>
        /// <param name="settings">Appium server settings</param>
        public void Setup(IAppiumServerSettings settings)
        {
            if (_args == null)
            {
                new List<IAppiumServerArgument>();
            }
            else
            {
                _args.Clear();
            }
            // basic runner setup
            _filename = _defaultFilename;
            _workingDir = _defaultWorkingDir;
            // add dev mode arguments
            if (settings.UseDeveloperMode)
            {
                if (settings.UseExternalNodeJSBinary)
                {
                    _filename = settings.ExternalNodeJSBinary;
                }
                if (settings.UseExternalAppiumPackage)
                {
                    _workingDir = settings.ExternalAppiumPackage;
                }
                if (settings.UseNodeJSDebugging)
                {
                    _args.Add(new NodeJSDebugArgument(settings.NodeJSDebugPort));
                }
                if (settings.BreakOnApplicationStart)
                {
                    _args.Add(new BreakOnAppStartArgument());
                }
            }
            // basic node args
            _args.Add(new ServerRunnerArgument());
            _args.Add(new ServerAddressArgument(settings.IPAddress));
            _args.Add(new ServerPortArgument(settings.Port));
            if (settings.UseApplicationPath)
            {
                _args.Add(new ApplicationPathArgument(settings.ApplicationPath));
            }

            // android specific args
            if (settings.UseAndroidActivity)
            {
                _args.Add(new AndroidActivityArgument(settings.AndroidActivity));
            }
            if (settings.UseAndroidPackage)
            {
                _args.Add(new AndroidPackageArgument(settings.AndroidPackage));
            }
            if (settings.LaunchAVD)
            {
                _args.Add(new AVDToLaunchArgument(settings.AVDToLaunch));
            }
            if (settings.UseAndroidWaitForActivity)
            {
                _args.Add(new AndroidWaitActivityArgument(settings.AndroidWaitForActivity));
            }
            if (settings.UseAndroidDeviceReadyTimeout)
            {
                _args.Add(new AndroidDeviceReadyTimeoutArgument(settings.AndroidDeviceReadyTimeout));
            }
            if (settings.UseAndroidWaitForPackage)
            {
                _args.Add(new AndroidWaitForPackageArgument(settings.AndroidWaitForPackage));
            }
            if (settings.PerformFullAndroidReset)
            {
                _args.Add(new FullResetArgument());
            }
            // preference-related arguments
            if (settings.QuietLogging)
            {
                _args.Add(new QuietLoggingArgument());
            }
            if (settings.NoReset)
            {
                _args.Add(new NoResetArgument());
            }
            if (settings.PrelaunchApplication)
            {
                _args.Add(new PrelauchApplicationArgument());
            }
            if (settings.UseAVDLaunchArguments)
            {
                _args.Add(new AVDArgsToLaunchArgument(settings.AVDLaunchArguments));
            }
            if (settings.UseSDKPath)
            {
                Environment.SetEnvironmentVariable("ANDROID_HOME", settings.SDKPath);
            }
            if (settings.UseCoverageClass)
            {
                _args.Add(new AndroidCoverageArgument(settings.CoverageClass));
            }
            if (settings.UseBootstrapPort)
            {
                _args.Add(new AndroidBootstrapPortArgument(settings.BootstrapPort));
            }
            if (settings.UseSelendroidPort)
            {
                _args.Add(new SelendroidPortArgument(settings.SelendroidPort));
            }
            if (settings.UseChromeDriverPort)
            {
                _args.Add(new ChromeDriverPortArgument(settings.ChromeDriverPort));
            }
            if (settings.ShowTimestamps)
            {
                _args.Add(new ShowTimestampLogArgument());
            }
            if (settings.UseLogToFile)
            {
                _args.Add(new LogToFileArgument(settings.LogToFile));
            }
            if (settings.UseLogToWebHook)
            {
                _args.Add(new LogToWebHookArgument(settings.LogToWebHook));
            }
            if (settings.OverrideExistingSessions)
            {
                _args.Add(new OverrideExistingSessionArgument());
            }
            if (settings.KillProcessUsingServerPortBeforeLaunch)
            {
                // TODO: Need to implement this and uncomment the UI component
                // 1) Find all processes running on the port
                // 2) kill the process running on the port
            }
            if (settings.UseGridSeleniumConfigFile)
            {
                _args.Add(new SeleniumGridArgument(settings.GridSeleniumConfigFile));
            }
            if (settings.UseCustomServerFlags)
            {
                _args.Add(new CustomDeveloperArguments(settings.CustomServerFlags));
            }

            #region Capabilities
            _args.Add(new PlatformNameArgument(settings.PlatformName));
            _args.Add(new PlatformVersionArgument(Dictionaries.GetPlatformVersion(settings.PlatformVersion)));
            _args.Add(new AutomationNameArgument(settings.AutomationName));
            if (settings.UseDeviceName)
            {
                _args.Add(new DeviceNameArgument(settings.DeviceName));
            }
            if (settings.UseLanguage)
            {
                _args.Add(new LanguageArgument(settings.Language));
            }
            if (settings.UseLocale)
            {
                _args.Add(new LocaleArgument(settings.Locale));
            }
            #endregion Capabilities
        }

        public string GetArgumentsCmdLine()
        {
            return String.Join<IAppiumServerArgument>(" ", _args);
        }

        /// <summary>
        /// Checks that argument of a specific type is present.
        /// </summary>
        /// <typeparam name="T">Type of argument to look for.</typeparam>
        /// <returns><value>True</value> when the arguments of given type is present, <value>False</value> otherwise.</returns>
        public bool ContainsArgument<T>() where T : IAppiumServerArgument
        {
            return GetArgument<T>() != null;
        }

        /// <summary>
        /// Gets argument of specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T GetArgument<T>() where T : IAppiumServerArgument
        {
            return (T)_args.Find(i => i.GetType().Equals(typeof(T)));
        }
    }
}
