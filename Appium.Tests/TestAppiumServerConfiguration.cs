
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
		}

		[Test]
		public void TestDeveloperModeNodeJsDebuggingPortIsUsed()
		{
			uint port = 1234;
			DefaultAppiumServerSettings settings = new DefaultAppiumServerSettings();
			settings.UseDeveloperMode = true;
			settings.UseNodeJSDebugging = true;
			settings.NodeJSDebugPort = port;

			AppiumServerRunnerMock setup = new AppiumServerRunnerMock(NODE_RUNNER, WORKING_DIR, settings);

			Assert.That(setup.ContainsArgument<NodeJSDebugArgument>(), Is.True);
			NodeJSDebugArgument arg = setup.GetArgumentForTest<NodeJSDebugArgument>();
			Assert.That(arg.Value, Is.EqualTo(port));
		}

		[Test]
		public void TestDeveloperModeBreakOnAppStartIsUsed()
		{
			DefaultAppiumServerSettings settings = new DefaultAppiumServerSettings();
			settings.UseDeveloperMode = true;
			settings.BreakOnApplicationStart = true;

			AppiumServerRunner setup = new AppiumServerRunner(NODE_RUNNER, WORKING_DIR, settings);

			Assert.That(setup.ContainsArgument<BreakOnAppStartArgument>(), Is.True);
		}




	}
}
