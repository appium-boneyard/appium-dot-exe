using Appium.Models;
using Appium.Models.Server;

namespace Appium.Tests.Mocks
{
	/// <summary>
	/// Mock only to provide quick access to server arguments for testing purposes
	/// </summary>
	public class AppiumServerRunnerMock : AppiumServerRunner
	{
		public AppiumServerRunnerMock(string nodeExec, string workingDir, IAppiumServerSettings settings) 
		: base(nodeExec, workingDir, settings) {}
		
		public T GetArgumentForTest<T>() where T : IAppiumServerArgument
		{
			return GetArgument<T>();
		}
	}
}
