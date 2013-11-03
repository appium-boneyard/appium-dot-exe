using System;
using Appium.Models;


namespace Appium.PreferencesWindow
{
	/// <summary>
	/// Presentation model for the Preferences Window
	/// </summary>
	public class PreferencesPModel
	{
		private IAppiumServerSettings _Settings;
		private IPreferencesView _View;

		public PreferencesPModel(IAppiumServerSettings settings, IPreferencesView view)
		{
			_Settings = settings;
			_View = view;
		}

		/// <summary>true if the nodejs debugger will break on application start</summary>
		public bool BreakOnApplicationStart
		{
			get { return _Settings.BreakOnApplicationStart; }
			set { _Settings.BreakOnApplicationStart = value; }
		}

		/// <summary>true if appium should check for updates</summary>
		public bool CheckForUpdates
		{
			get { return _Settings.CheckForUpdates; }
			set { _Settings.CheckForUpdates = value; }
		}

		/// <summary>true if developer mode is enabled</summary>
		public bool UseDeveloperMode
		{
			get { return _Settings.UseDeveloperMode; }
			set { _Settings.UseDeveloperMode = value; }
		}

		/// <summary>path to external node.exe</summary>
		public string ExternalNodeJSBinary
		{
			get { return _Settings.ExternalNodeJSBinary; }
			set { _Settings.ExternalNodeJSBinary = value; }
		}

		/// <summary>pack to external appium node js package</summary>
		public string ExternalAppiumPackage
		{
			get { return _Settings.ExternalAppiumPackage; }
			set { _Settings.ExternalAppiumPackage = value; }
		}

		/// <summary>true if artifacts will be kept after a session</summary>
		public bool KeepArtifacts
		{
			get { return _Settings.KeepArtifacts; }
			set	{ _Settings.KeepArtifacts = value; }
		}

		/// <summary>port on which the nodejs debugger will run</summary>
		public uint NodeJSDebugPort
		{
			get { return _Settings.NodeJSDebugPort; }
			set	{ _Settings.NodeJSDebugPort = value; }
		}

		/// <summary>true if appium should prelaunch the application</summary>
		public bool PrelaunchApplication
		{
			get { return _Settings.PrelaunchApplication; }
			set { _Settings.PrelaunchApplication = value; }
		}

		/// <summary>true if quiet logging should be used</summary>
		public bool QuietLogging
		{
			get { return _Settings.QuietLogging; }
			set { _Settings.QuietLogging = value; }
		}

		/// <summary>true if application state should be reset between sessions</summary>
		public bool ResetApplicationState
		{
			get { return _Settings.ResetApplicationState; }
			set { _Settings.ResetApplicationState = value; }
		}

		/// <summary>true if an external node js binary will be used</summary>
		public bool UseExternalNodeJSBinary
		{
			get { return _Settings.UseExternalNodeJSBinary; }
			set { _Settings.UseExternalNodeJSBinary = value; }
		}

		/// <summary>true if an external appium package will be used</summary>
		public bool UseExternalAppiumPackage
		{
			get { return _Settings.UseExternalAppiumPackage; }
			set	{ _Settings.UseExternalAppiumPackage = true; }
		}

		/// <summary>true if nodejs debugging will be used</summary>
		public bool UseNodeJSDebugging
		{
			get { return _Settings.UseNodeJSDebugging; }
			set	{ _Settings.UseNodeJSDebugging = value; }
		}

		public void OpenWindow()
		{
			_View.BindPresentationModel(this);
			_View.Open();
		}
	}
}
