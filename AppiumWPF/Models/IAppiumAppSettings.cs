using AppiumWPF.Models.Capability;
namespace AppiumWPF.Models
{
	public interface IAppiumAppSettings : IAppiumServerSettings
	{
		Device InspectorDeviceCapability { get; set; }
	}
}
