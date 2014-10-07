## A Windows GUI for Appium

If you are new to Appium then please see the [Getting started](http://appium.io/getting-started.html) guide for more information
about the project.

Pre-req:
* Need .NET Framework 4.5 redistributable libraries

To install:

1. Download the [latest version](https://bitbucket.org/appium/appium.app/downloads/AppiumForWindows.zip) from [Appium.io](http://appium.io/).
2. Extract the ZIP file.
3. Launch `appium.exe`.

### Parameter Guide

#### Main Window

![Appium Main Window](/README-files/windows-mainwindow.png "Appium Main Window")

* **Android Button**: Displays the Android settings.
* **Settings Button**: Displays the General settings.
* **Developer Button**: Displays the Developer settings.
* **About Button**: Displays the Appium version information.
* **Inspector Button**: Launches the Appium Inspector.
* **Launch / Stop Button**: Launches or stops the Appium server.
* **Clear Button**: Clears the display of all log output.

#### Android Settings

* **Application**
 * **App Path**: The path to the Android application (`.apk`) you wish to test.
 * **Choose Button**: Used to choose the path to your application.
 * **Package**: Java package of the Android app to run (e.g. `com.example.android.myApp`).
 * **Wait for Package**: Package name for the Android activity to wait for.
 * **Launch Activity**: Activity name for the Android activity to launch from your package (e.g. `MainActivity`).
 * **Wait for Activity**: Activity name for the Android activity to wait for.
 * **Full Reset**: Reset app state by uninstalling app instead of clearing app data and also remove the app after the
   session is complete.
 * **No Reset**: Prevent the device from being reset.
 * **Use Browser**: Launch the specified Android browser (e.g. `Chrome`).
 * **Intent Action**: Intent action which will be used to start the activity.
 * **Intent Category**: Intent category which will be used to start the activity.
 * **Intent Flags**: Flags that will be used to start the activity.
 * **Intent Arguments**: Additional intent arguments that will be used to start the activity.
* **Launch Device**
 * **Launch AVD**: Name of the AVD to launch.
 * **Device Ready Timeout**: Timeout in seconds while waiting for device to become ready.
 * **Arguments**: Additional emulator arguments to launch the avd.
* **Capabilities**
 * **Platform Name**: Name of the mobile platform.
 * **Automation Name**: Name of the automation tool (Appium or Selendroid).
 * **Platform Version**: Version of the mobile platform.
 * **Device Name**: Name of the mobile device to use.
 * **Language**: Language for the Android device.
 * **Locale**: Locale for the Android device.
* **Advanced**
 * **SDK Path**: Path to Android SDK.
 * **Coverage Class**: Fully qualified instrumentation class.
 * **Bootstrap Port**: Port to use on device to talk to Appium.
 * **Selendroid Port**: Local port used for communication with Selendroid.
 * **Chromedriver Port**: Port upon which ChromeDriver will run.

### Preference Guide

Preferences can be accessed by clicking on the appropriate button in the main window.

![Appium Preferences](/README-files/windows-preferences.png "Appium General Settings")

#### General Settings

* **Server**
 * **Server Address**: The IP address on which you want the Appium server to run (127.0.0.1 is localhost).
 * **Port**: The port on which the Appium server will listen for WebDriver commands (4723 is the default).
 * **Check For Updates**: Appium will automatically check for updates when starting.
 * **Pre-launch Application**: Appium will prelaunch the application before beginning to listen for WebDriver
	commands.
 * **Override Existing Session**: Any existing Appium sessions will be overridden.
 * **Use Remote Server**: Used to connect Appium Inpector to a server that is already running.
 * **Selenium Grid Configuration File**: Path to the configuration file for Selenium Grid.
* **Logging**
 * **Quiet Logging**: Don't use verbose logging output.
 * **Use Colors**: Use colors in console output.
 * **Show Timestamps**: Show timestamps in console output.
 * **Log to File**: Send log output to this file.
 * **Log to WebHook**: Send log output to this HTTP listener.

#### Developer Settings

* **Enabled**: If checked, developer settings will be observed.
* **Use External NodeJS Binary**: Appium will use the version of NodeJS supplied here instead of the one that ships
  with the application.
* **Use External Appium Package**: Appium will use the version of the Appium package supplied here instead of the one
  bundled with the application.
* **NodeJS Debug Port**: Port on which the NodeJS debugger will run.
* **Break on Application Start**: The NodeJS debug server will break at the application start. (equivalent to
  supplying the debug-brk switch to node)
* **Custom Server Flags**: Custom flags to be used when starting the Appium server. This should only be used if there is
  an option that cannot be adjusted using the Appium UI.

### Inspector / Recorder

Inspector can be accessed by clicking the magnifying glass next to the launch button once the Appium server has launched.
Appium must be running with an app open for inspector to work. Otherwise, it will not work.

The Inspector can be use to connect to an external Appium server. This can be specified in the General Settings.

![Appium Inspector](/README-files/windows-inspector.png "Appium Inspector")
