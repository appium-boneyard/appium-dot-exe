using Appium.Engine;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
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
			string archiveTarget = "";
			string archiveOut = "";

			for (int i = 0; i < e.Args.Length; i++)
			{
				if (e.Args[i] == "/installapps")
				{
					try 
					{
						AppiumEngine.Instance.DownloadAndInstallNodeJS();
						AppiumEngine.Instance.NPMInstallAppium();
					} 
					catch (Exception ex)
					{
						Console.WriteLine(ex);
					}
					finally
					{
						Environment.Exit(0);
					}
				}
				else if (e.Args[i].StartsWith("/z="))
				{
					archiveTarget = e.Args[i].Split('=')[1];
				}
				else if (e.Args[i].StartsWith("/zo="))
				{
					archiveOut = e.Args[i].Split('=')[1];
				}
			}

			if (!String.IsNullOrEmpty(archiveTarget)) 
			{
				try 
				{
					using (var zip = new ZipFile(archiveOut, Encoding.UTF8)) {
						zip.AddFile(archiveTarget, "");
						zip.Save();
					}
				} 
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
				finally 
				{
					Environment.Exit(0);
				}	   
			}

		}
    }
}
