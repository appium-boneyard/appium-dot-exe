using Appium.Engine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Appium
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		/// <summary>
		/// Handle arguments passed from the commandline
		/// </summary>
		/// <param name="e"></param>
		protected override void OnStartup(StartupEventArgs e)
		{
			for (int i = 0; i < e.Args.Length; i++)
			{
				if (e.Args[i] == "/installapps")
				{
					AppiumEngine.Instance.DownloadAndInstallNodeJS();
					AppiumEngine.Instance.NPMInstallAppium();
					Environment.Exit(0);
				}
				
			}
		}
    }
}
