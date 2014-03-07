using Appium.Models.Capability;
namespace Appium.Models
{
	public interface IAppiumAppSettings : IAppiumServerSettings
	{
		Device InspectorDeviceCapability { get; set; }
	}
}
