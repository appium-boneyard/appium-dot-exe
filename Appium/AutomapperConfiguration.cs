using System;
using Appium.Models;
using AutoMapper;

namespace Appium
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
			Mapper.CreateMap<Appium.Properties.Settings,IAppiumServerSettings>()
				.ForMember(d => d.AVDToLaunch, opt => opt.MapFrom(s => s.AVD))
				.ForMember(d => d.IPAddress, opt => opt.MapFrom(s => s.ServerAddress))
				.ForMember(d => d.Port, opt => opt.MapFrom(s => s.ServerPort))
				.ForMember(d => d.UseDeveloperMode, opt => opt.MapFrom(s => s.DeveloperMode))
				.ReverseMap()
				.ForMember(d => d.AVD, opt => opt.MapFrom(s => s.AVDToLaunch))
				.ForMember(d => d.ServerAddress, opt => opt.MapFrom(s => s.IPAddress))
				.ForMember(d => d.ServerPort, opt => opt.MapFrom(s => s.Port))
				.ForMember(d => d.DeveloperMode, opt => opt.MapFrom(s => s.UseDeveloperMode));
						
		} 
	}
}
