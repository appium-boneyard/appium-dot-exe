using System;
using AutoMapper;
using NUnit.Framework;

namespace Appium.Tests
{
	[TestFixture]
	class TestAutomapper
	{
		/// <summary>
		/// Tests validity of automapper mapping configuration
		/// </summary>
		[Test]
		public void TestConfiguration()
		{
			Appium.AutomapperConfiguration.Configure();
			Mapper.AssertConfigurationIsValid();
		}

	}
}
