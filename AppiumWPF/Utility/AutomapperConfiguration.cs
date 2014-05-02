using Appium.Models;
using Appium.Models.Capability;
using AutoMapper;
using System;

namespace Appium.Utility
{
	/// <summary>
	/// Handles automapper configuration
	/// </summary>
	public class AutomapperConfiguration
	{
		/// <summary>
		/// Configures Automapper. Put all automapper configuration here.
		/// </summary>
		public static void Configure()
		{
			Mapper.CreateMap<Appium.Properties.Settings,IAppiumAppSettings>()
				.ForMember(d => d.AVDToLaunch, opt => opt.MapFrom(s => s.AVD))
				.ForMember(d => d.IPAddress, opt => opt.MapFrom(s => s.ServerAddress))
				.ForMember(d => d.Port, opt => opt.MapFrom(s => s.ServerPort))
				.ForMember(d => d.UseDeveloperMode, opt => opt.MapFrom(s => s.DeveloperMode))
				.ForMember(d => d.InspectorDeviceCapability, opt => opt.ResolveUsing(s => {
					Device srcDevice;
					if (Enum.TryParse<Device>(s.InspectorDeviceCapability, out srcDevice))
						return srcDevice;
					return Device.android;
				}))
				.ReverseMap()
				.ForMember(d => d.AVD, opt => opt.MapFrom(s => s.AVDToLaunch))
				.ForMember(d => d.ServerAddress, opt => opt.MapFrom(s => s.IPAddress))
				.ForMember(d => d.ServerPort, opt => opt.MapFrom(s => s.Port))
				.ForMember(d => d.DeveloperMode, opt => opt.MapFrom(s => s.UseDeveloperMode))
				.ForMember(d => d.InspectorDeviceCapability, opt => opt.MapFrom(s => s.InspectorDeviceCapability.ToString()));
						
		} 
	}
}
