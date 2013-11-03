
using Appium.Models;
using Appium.Models.Server;
using Appium.Tests.Mocks;
using NUnit.Framework;
namespace Appium.Tests
{
	[TestFixture]
	public class TestAppiumServerConfiguration
	{
		private const string NODE_RUNNER = "node.exe";
		private const string WORKING_DIR = "testWD";

		/// <summary>
		/// Validates fix for issue#14 (https://github.com/appium/appium-dot-exe/issues/14), full-reset and no-reset options are mutual exclusive
		/// </summary>
		[Test]
		public void TestFullResetNoResetMutualExclusivity()
		{
			DefaultAppiumServerSettings settings = new DefaultAppiumServerSettings();
			settings.PerformFullAndroidReset = true;
			settings.ResetApplicationState = false;

			AppiumServerRunner setup = new AppiumServerRunner("", "", settings);

			Assert.That(setup.ContainsArgument<FullResetArgument>(), Is.True);
			Assert.That(setup.ContainsArgument<NoResetArgument>(), Is.False);
		}

		/// <summary>
		/// All developer settings should be ignored when developer mode is disabled
		/// </summary>
		[Test]
		public void TestAllDeveloperSettingsIgnoredWhenDevModeDisabled()
		{
			DefaultAppiumServerSettings settings = new DefaultAppiumServerSettings();
			settings.UseDeveloperMode = false;
			settings.UseExternalNodeJSBinary = true;
			settings.ExternalNodeJSBinary = "testNodeJs";
			settings.UseExternalAppiumPackage = true;
			settings.ExternalAppiumPackage = "extApp";
			settings.UseNodeJSDebugging = true;
			settings.NodeJSDebugPort = 1234;
			settings.BreakOnApplicationStart = true;

			AppiumServerRunner setup = new AppiumServerRunner(NODE_RUNNER, WORKING_DIR, settings);

			Assert.That(setup.Filename, Is.EqualTo(NODE_RUNNER));
			Assert.That(setup.WorkingDirectory, Is.EqualTo(WORKING_DIR));
			Assert.That(setup.ContainsArgument<NodeJSDebugArgument>(), Is.False);
			Assert.That(setup.ContainsArgument<BreakOnAppStartArgument>(), Is.False);
		}

		[Test]
		public void TestDeveloperModeExternalNodeJsLibraryIsUsed()
		{
			string testNodeJs = "testNodeJs"; 
			DefaultAppiumServerSettings settings = new DefaultAppiumServerSettings();
			settings.UseDeveloperMode = true;
			settings.UseExternalNodeJSBinary = true;
			settings.ExternalNodeJSBinary = testNodeJs;

			AppiumServerRunner setup = new AppiumServerRunner(NODE_RUNNER, WORKING_DIR, settings);

			Assert.That(setup.Filename, Is.EqualTo(testNodeJs));

			//reset for negative case
			settings.UseExternalNodeJSBinary = false;
			setup.Setup(settings);
			Assert.That(setup.Filename, Is.EqualTo(NODE_RUNNER));
		}

		[Test]
		public void TestDeveloperModeExternalAppiumPackageIsUsed()
		{
			string test = "app";
			DefaultAppiumServerSettings settings = new DefaultAppiumServerSettings();
			settings.UseDeveloperMode = true;
			settings.UseExternalAppiumPackage = true;
			settings.ExternalAppiumPackage = test;

			AppiumServerRunner setup = new AppiumServerRunner(NODE_RUNNER, WORKING_DIR, settings);

			Assert.That(setup.WorkingDirectory, Is.EqualTo(test));

			// reset for negative case
			settings.UseExternalAppiumPackage = false;
			setup.Setup(settings);
			Assert.That(setup.WorkingDirectory, Is.EqualTo(WORKING_DIR));
		}

		[Test]
		public void TestDeveloperModeNodeJsDebuggingPortUsage()
		{
			uint port = 1234;
			DefaultAppiumServerSettings settings = new DefaultAppiumServerSettings();
			settings.UseDeveloperMode = true;
			settings.UseNodeJSDebugging = true;
			settings.NodeJSDebugPort = port;

			AppiumServerRunnerMock setup = new AppiumServerRunnerMock(NODE_RUNNER, WORKING_DIR, settings);

			Assert.That(setup.ContainsArgument<NodeJSDebugArgument>(), Is.True);
			Assert.That(setup.GetArgumentForTest<NodeJSDebugArgument>().Value, Is.EqualTo(port));

			// reset for negative case
			settings.UseNodeJSDebugging = false;
			setup.Setup(settings);
			Assert.That(setup.ContainsArgument<NodeJSDebugArgument>(), Is.False);
		}

		[Test]
		public void TestDeveloperModeBreakOnAppStartUsage()
		{
			DefaultAppiumServerSettings settings = new DefaultAppiumServerSettings();
			settings.UseDeveloperMode = true;
			settings.BreakOnApplicationStart = true;

			AppiumServerRunner setup = new AppiumServerRunner(NODE_RUNNER, WORKING_DIR, settings);

			Assert.That(setup.ContainsArgument<BreakOnAppStartArgument>(), Is.True);
			
			// reset for negative case
			settings.BreakOnApplicationStart = false;
			setup.Setup(settings);
			Assert.That(setup.ContainsArgument<BreakOnAppStartArgument>(), Is.False);
		}

		[Test]
		public void TestAndroidActivityUsage()
		{
			string test = "testActivity";
			DefaultAppiumServerSettings settings = new DefaultAppiumServerSettings();
			settings.AndroidActivity = test;

			AppiumServerRunnerMock setup = new AppiumServerRunnerMock(NODE_RUNNER, WORKING_DIR, settings);
			
			// activity not enabled by default
			Assert.That(setup.ContainsArgument<AndroidActivityArgument>(),Is.False);

			// activity enabled
			settings.UseAndroidActivity = true;
			setup.Setup(settings);

			Assert.That(setup.ContainsArgument<AndroidActivityArgument>(),Is.True);
			Assert.That(setup.GetArgumentForTest<AndroidActivityArgument>().Value, Is.EqualTo(test));
		}

		[Test]
		public void TestAndroidPackageUsage()
		{
			string test = "testPackage";
			DefaultAppiumServerSettings settings = new DefaultAppiumServerSettings();
			settings.AndroidPackage = test;

			AppiumServerRunnerMock setup = new AppiumServerRunnerMock(NODE_RUNNER, WORKING_DIR, settings);

			// package not enabled by default
			Assert.That(setup.ContainsArgument<AndroidPackageArgument>(), Is.False);

			// package enabled
			settings.UseAndroidPackage = true;
			setup.Setup(settings);

			Assert.That(setup.ContainsArgument<AndroidPackageArgument>(), Is.True);
			Assert.That(setup.GetArgumentForTest<AndroidPackageArgument>().Value, Is.EqualTo(test));
		}

		[Test]
		public void TestLaunchAVDUsage()
		{
			string test = "AVD";
			DefaultAppiumServerSettings settings = new DefaultAppiumServerSettings();
			settings.AVDToLaunch = test;

			AppiumServerRunnerMock setup = new AppiumServerRunnerMock(NODE_RUNNER, WORKING_DIR, settings);

			// avd not enabled by default
			Assert.That(setup.ContainsArgument<AVDToLaunchArgument>(), Is.False);

			// avd enabled
			settings.LaunchAVD = true;
			setup.Setup(settings);

			Assert.That(setup.ContainsArgument<AVDToLaunchArgument>(), Is.True);
			Assert.That(setup.GetArgumentForTest<AVDToLaunchArgument>().Value, Is.EqualTo(test));
		}

		[Test]
		public void TestAndroidWaitActivityUsage()
		{
			string test = "wait";
			DefaultAppiumServerSettings settings = new DefaultAppiumServerSettings();
			settings.AndroidWaitActivity = test;

			AppiumServerRunnerMock setup = new AppiumServerRunnerMock(NODE_RUNNER, WORKING_DIR, settings);

			// activity not enabled by default
			Assert.That(setup.ContainsArgument<AndroidWaitActivityArgument>(), Is.False);

			// activity enabled
			settings.UseAndroidWaitActivity = true;
			setup.Setup(settings);

			Assert.That(setup.ContainsArgument<AndroidWaitActivityArgument>(), Is.True);
			Assert.That(setup.GetArgumentForTest<AndroidWaitActivityArgument>().Value, Is.EqualTo(test));
		}

		[Test]
		public void TestAndroidDeviceReadyTimeoutUsage()
		{
			uint test = 1234;
			DefaultAppiumServerSettings settings = new DefaultAppiumServerSettings();
			settings.AndroidDeviceReadyTimeout = test;

			AppiumServerRunnerMock setup = new AppiumServerRunnerMock(NODE_RUNNER, WORKING_DIR, settings);

			// timeout not enabled by default
			Assert.That(setup.ContainsArgument<AndroidDeviceReadyTimeoutArgument>(), Is.False);

			// timeout enabled
			settings.UseAndroidDeviceReadyTimeout = true;
			setup.Setup(settings);

			Assert.That(setup.ContainsArgument<AndroidDeviceReadyTimeoutArgument>(), Is.True);
			Assert.That(setup.GetArgumentForTest<AndroidDeviceReadyTimeoutArgument>().Value, Is.EqualTo(test));
		}

		[Test]
		public void TestQuietLoggingUsage()
		{
			DefaultAppiumServerSettings settings = new DefaultAppiumServerSettings();

			AppiumServerRunner setup = new AppiumServerRunner(NODE_RUNNER, WORKING_DIR, settings);

			// quiet logging not enabled by default
			Assert.That(setup.ContainsArgument<QuietLoggingArgument>(), Is.False);

			// quiet logging enabled
			settings.QuietLogging = true;
			setup.Setup(settings);

			Assert.That(setup.ContainsArgument<QuietLoggingArgument>(), Is.True);
		}

		[Test]
		public void TestKeepArtifactsUsage()
		{
			DefaultAppiumServerSettings settings = new DefaultAppiumServerSettings();

			AppiumServerRunner setup = new AppiumServerRunner(NODE_RUNNER, WORKING_DIR, settings);

			// keep artifacts not enabled by default
			Assert.That(setup.ContainsArgument<KeepArtifactsArgument>(), Is.False);

			// keep artifacts enabled
			settings.KeepArtifacts = true;
			setup.Setup(settings);

			Assert.That(setup.ContainsArgument<KeepArtifactsArgument>(), Is.True);
		}

		[Test]
		public void TestPrelauchApplicationUsage()
		{
			DefaultAppiumServerSettings settings = new DefaultAppiumServerSettings();

			AppiumServerRunner setup = new AppiumServerRunner(NODE_RUNNER, WORKING_DIR, settings);

			// keep artifacts not enabled by default
			Assert.That(setup.ContainsArgument<PrelauchApplicationArgument>(), Is.False);

			// keep artifacts enabled
			settings.PrelaunchApplication = true;
			setup.Setup(settings);

			Assert.That(setup.ContainsArgument<PrelauchApplicationArgument>(), Is.True);
		}



	}
}
