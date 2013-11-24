using Appium.MainWindow;
using Appium.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;

namespace Appium.Tests
{
	/// <summary>
	/// Tests for main form model
	/// </summary>
	[TestFixture]
	public class TestMainFormModel
	{
		/// <summary>
		/// Tests SetupNewAppPath method. When changing AppPath we want to change UseAppPath in settings as well
		/// and we want to notify UI about the programmatic changes
		/// Test for Issue#27 fix.
		/// </summary>
		[Test]
		public void TestSetupAppPathChangesSettingsAndNotifies()
		{
			// setup
			DefaultAppiumServerSettings settings = new DefaultAppiumServerSettings();
			Model mainModel = new Model(null, settings);
			List<string> events = new List<string>();
			mainModel.PropertyChanged += delegate(object s, PropertyChangedEventArgs e)
			{
				events.Add(e.PropertyName);
			};
			const string appPath = "testMe";

			// test
			mainModel.SetupNewApplicationPath(appPath);

			Assert.That(settings.ApplicationPath, Is.EqualTo(appPath));
			Assert.That(settings.UseApplicationPath, Is.True);

			Assert.That(events[0], Is.EqualTo("ApplicationPath"), "Event for ApplicationPath property change has not been fired.");
			Assert.That(events[1], Is.EqualTo("UseApplicationPath"), "Event for UseApplicationPath property change has not been fired.");
		}
	}
}
