using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Appium.Engine;
using Microsoft.Win32;

namespace Appium.Utility
{
    public class AndroidSDKCommands
    {
        /// <summary>VBoxManage path</summary>
        private const string _VBoxManagePath = "Oracle\\VirtualBox\\VBoxManage.exe";

        #region Public Methods
        /// <summary>
        /// Get the list of AVD's by running the android.bat file in tools
        /// </summary>
        /// <returns>list of avd's found, empty list if none found</returns>
        public static List<string> GetAvdList()
        {
            var avdList = new List<string>();
            try
            {
                // use the android command to list the avds
                ProcessStartInfo avdDetectionProcessInfo = new ProcessStartInfo();
                //avdDetectionProcessInfo.FileName = Path.Combine(androidSdkPath, "tools", "android.bat");
                avdDetectionProcessInfo.FileName = _PathToAndroidBinary("android.bat");

                if (File.Exists(avdDetectionProcessInfo.FileName))
                {
                    avdDetectionProcessInfo.Arguments = "list avd -c";
                    avdDetectionProcessInfo.UseShellExecute = false;
                    avdDetectionProcessInfo.CreateNoWindow = true;
                    avdDetectionProcessInfo.RedirectStandardOutput = true;
                    var avdDetectionProcess = Process.Start(avdDetectionProcessInfo);
                    avdDetectionProcess.WaitForExit();

                    // read the output
                    var output = string.Empty;
                    using (var myOutput = avdDetectionProcess.StandardOutput)
                    {
                        output = myOutput.ReadToEnd();
                    }

                    foreach (var line in output.Split(new char[] { '\r', '\n' }))
                    {
                        if (line.Length > 0)
                        {
                            avdList.Add(line);
                        }
                    }
                }

                // get genymotion avds
                var genymotionAvdDetectionProc = new ProcessStartInfo();
                var vboxPath = (String)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion", "ProgramW6432Dir", "");
                if (vboxPath.Length == 0)
                    vboxPath = Environment.GetEnvironmentVariable("ProgramFiles");
                vboxPath = Path.Combine(vboxPath, _VBoxManagePath);
                genymotionAvdDetectionProc.FileName = vboxPath;
                if (File.Exists(genymotionAvdDetectionProc.FileName))
                {
                    genymotionAvdDetectionProc.Arguments = "list vms";
                    genymotionAvdDetectionProc.UseShellExecute = false;
                    genymotionAvdDetectionProc.CreateNoWindow = true;
                    genymotionAvdDetectionProc.RedirectStandardOutput = true;
                    var genymotionAvdDetectionProcess = Process.Start(genymotionAvdDetectionProc);
                    genymotionAvdDetectionProcess.WaitForExit();

                    // read the output
                    var output = string.Empty;
                    using (var myOutput = genymotionAvdDetectionProcess.StandardOutput)
                    {
                        output = myOutput.ReadToEnd().TrimEnd();
                    }

                    foreach (var line in output.Split(new char[] { '\r', '\n' }))
                    {
                        var startQuote = line.IndexOf('"');
                        var endQuote = line.LastIndexOf('"');
                        if (startQuote != -1 && endQuote != -1 && startQuote < endQuote)
                        {
                            avdList.Add(line.Substring(startQuote + 1, endQuote - startQuote - 1));
                        }
                    }
                }
            }
            catch
            {
            }
            return avdList;
        }

        /// <summary>
        /// return a list of packages and activities that are associated with the android application
        /// </summary>
        /// <param name="appPath">application path</param>
        /// <param name="activityList">Activity List - count 0 if not found</param>
        /// <param name="packageList">Package List - count 0 if not found</param>
        public static void GetActivitiesAndPackages(string appPath, out List<string> activityList, out List<string> packageList)
        {
            activityList = new List<string>();
            packageList = new List<string>();

            if (string.IsNullOrWhiteSpace(appPath))
            {
                return;
            }
            appPath = appPath.Trim();
            if (!File.Exists(appPath))
            {
                return;
            }

            try
            {
                var aaptPath = new ProcessStartInfo();
                aaptPath.FileName = _PathToAndroidBinary("aapt.exe");

                if (File.Exists(aaptPath.FileName))
                {
                    aaptPath.Arguments = string.Format("dump xmltree {0} AndroidManifest.xml", appPath);
                    aaptPath.UseShellExecute = false;
                    aaptPath.CreateNoWindow = true;
                    aaptPath.RedirectStandardOutput = true;
                    var aaptProcess = Process.Start(aaptPath);

                    // read the output
                    string output;
                    var isElementNodeActivity = false;
                    while (null != (output = aaptProcess.StandardOutput.ReadLine()))
                    {
                        output = output.Trim();
                        // determine when an activity element has started or ended
                        if (output.StartsWith("E:"))
                        {
                            isElementNodeActivity = output.StartsWith("E: activity (line=");
                        }
                        // determine when the activity name has appeared
                        else if (isElementNodeActivity && output.StartsWith("A: android:name("))
                        {
                            var arr = output.Split('"');
                            if (3 <= arr.Length)
                            {
                                activityList.Add(arr[1]);
                            }
                        }
                        else if (output.StartsWith("A: package="))
                        {
                            var arr = output.Split('"');
                            if (3 <= arr.Length)
                            {
                                packageList.Add(arr[1]);
                            }

                        }
                    }
                }
            }
            catch
            {
            }
        }
        #endregion Public Methods

        #region Private Methods
        /// <summary>
        /// Search Android SDK directory to find the binary needed
        /// </summary>
        /// <param name="binaryName">binary file to find</param>
        /// <returns>path to the binary file (filename included)</returns>
        private static string _PathToAndroidBinary(string binaryName)
        {
            var sdkPath = AppiumEngine.Instance.AndroidSDKPath;
            string binaryPath = null;

            var tmpPath = string.Empty;
            // check platform folder
            if (File.Exists(tmpPath = Path.Combine(sdkPath, "platform-tools", binaryName)))
            {
                binaryPath = tmpPath;
            }
            // check tools folder
            else if (File.Exists(tmpPath = Path.Combine(sdkPath, "tools", binaryName)))
            {
                binaryPath = tmpPath;
            }
            // check the build-tools subdirectories
            else
            {
                binaryPath = _RecursiveFind(Path.Combine(sdkPath, "build-tools"), binaryName);
            }

            return binaryPath;
        }

        /// <summary>
        /// Recursively search directories until file is found
        /// </summary>
        /// <param name="dir">directory to seach</param>
        /// <param name="fileToFind">file to find (i.e. "aapt.exe" or "*.bat" or "*.exe")</param>
        /// <returns>path to file</returns>
        private static string _RecursiveFind(string dir, string fileToFind)
        {
            string pathFound = null;
            try
            {
                foreach (var d in Directory.GetDirectories(dir))
                {
                    var matchingFiles = Directory.GetFiles(d, fileToFind);
                    if (0 < matchingFiles.Length)
                    {
                        pathFound = matchingFiles[0];
                        break;
                    }
                    _RecursiveFind(d, fileToFind);
                }
            }
            catch { }

            return pathFound;
        }
        #endregion Private Methods
    }
}
