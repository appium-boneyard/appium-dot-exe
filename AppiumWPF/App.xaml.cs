using Appium.Engine;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
<<<<<<< HEAD
using System.Runtime.InteropServices;
=======
>>>>>>> 1cb1818dde02eb5efd43a259c51323231a9c89a5
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Appium
{
	//[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
	//If you use the above you may encounter an invalid memory access exception (when using ANSI
	//or see nothing (when using unicode) when you use FOF_SIMPLEPROGRESS flag.
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct SHFILEOPSTRUCT
	{
		public IntPtr hwnd;
		public uint wFunc;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string pFrom;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string pTo;
		public ushort fFlags;
		[MarshalAs(UnmanagedType.Bool)]
		public bool fAnyOperationsAborted;
		public IntPtr hNameMappings;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string lpszProgressTitle;
	}

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		[DllImport("shell32.dll", CharSet = CharSet.Unicode)]
		static extern int SHFileOperation([In] ref SHFILEOPSTRUCT lpFileOp);

		/// <summary>
		/// Deep delete that can handle paths upto ~32k characters.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public void ShellDeleteFile(string path)
		{	
			var shFile = new SHFILEOPSTRUCT();
			shFile.hwnd = IntPtr.Zero;
			shFile.wFunc = 0x3; // FO_DELETE
			shFile.pFrom = path;
			shFile.pTo = "";
			shFile.fAnyOperationsAborted = false;
			shFile.hNameMappings = IntPtr.Zero;
			shFile.lpszProgressTitle = "";
			shFile.fFlags = 0x10;// FOF_NOCONFIRMATION;
			// TODO handle any errors
			SHFileOperation(ref shFile);

			// in the case of a directory path, SHFileOperation will delete
			// contents but seems to leave directory itself
  			if (Directory.Exists(path)) 
			{
				Directory.Delete(path);
			}
		}

		/// <summary>
		/// Handle arguments passed from the commandline
		/// </summary>
		/// <param name="e"></param>
		protected override void OnStartup(StartupEventArgs e)
<<<<<<< HEAD
		{  
			string[] archiveTargets = new string[0];
=======
		{
			string archiveTarget = "";
>>>>>>> 1cb1818dde02eb5efd43a259c51323231a9c89a5
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
<<<<<<< HEAD
					archiveTargets = e.Args[i].Split('=')[1].Split(',');
				}
				else if (e.Args[i].StartsWith("/zo="))
				{
					archiveOut = e.Args[i].Split('=')[1];
				}
				else if (e.Args[i].StartsWith("/d="))
				{
					var path = e.Args[i].Split('=')[1];

					try
					{
						ShellDeleteFile(path);							 
					} 
					finally
					{
						Environment.Exit(0);
					} 
				}
			}

			if (archiveTargets.Length > 0) 
			{
				try 
				{
					using (var zip = new ZipFile(archiveOut, Encoding.UTF8))
					{
						zip.AddFiles(archiveTargets, "");
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
=======
					archiveTarget = e.Args[i].Split('=')[1];
				}
				else if (e.Args[i].StartsWith("/zo="))
				{
					archiveOut = e.Args[i].Split('=')[1];
				}
>>>>>>> 1cb1818dde02eb5efd43a259c51323231a9c89a5
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
