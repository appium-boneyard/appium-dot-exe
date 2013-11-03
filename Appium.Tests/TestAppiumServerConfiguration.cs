
using Appium.Models;
using Appium.Models.Server;
using NUnit.Framework;
namespace Appium.Tests
{
	[TestFixture]
	public class TestAppiumServerConfiguration
	{
		/// <summary>
		/// Validates fix for issue#14 (https://github.com/appium/appium-dot-exe/issues/14), full-reset and no-reset options are mutual exclusive
		/// </summary>
		[Test]
		public void TestFullResetNoResetMutualExclusivity()
		{
			DefaultAppiumServerSettings settings = new DefaultAppiumServerSettings();
			settings.PerformFullAndroidReset=true;
			settings.ResetApplicationState=false;

			AppiumServerRunner setup = new AppiumServerRunner("","",settings);

			Assert.That(setup.ContainsArgument<FullResetArgument>(), Is.True);
			Assert.That(setup.ContainsArgument<NoResetArgument>(), Is.False);
		}
	}
}
