namespace Appium.PreferencesWindow
{
    partial class PreferencesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreferencesForm));
            this.CheckForUpdatesCheckbox = new System.Windows.Forms.CheckBox();
            this.QuietLoggingCheckbox = new System.Windows.Forms.CheckBox();
            this.KeepArtifactsCheckbox = new System.Windows.Forms.CheckBox();
            this.ResetApplicationCheckbox = new System.Windows.Forms.CheckBox();
            this.PreLaunchApplicationCheckbox = new System.Windows.Forms.CheckBox();
            this.DeveloperModeCheckbox = new System.Windows.Forms.CheckBox();
            this.DeveloperSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.ExternalAppiumPackageBrowse = new System.Windows.Forms.Button();
            this.ExternalNodeJSBinaryBrowseButton = new System.Windows.Forms.Button();
            this.ExternalAppiumPackageTextBox = new System.Windows.Forms.TextBox();
            this.ExternalNodeJSBinaryTextBox = new System.Windows.Forms.TextBox();
            this.NodeJSDebugPortPicker = new System.Windows.Forms.NumericUpDown();
            this.BreakOnApplicationStartCheckbox = new System.Windows.Forms.CheckBox();
            this.NodeJSDebugPortCheckbox = new System.Windows.Forms.CheckBox();
            this.UseExternalAppiumPackageCheckbox = new System.Windows.Forms.CheckBox();
            this.UseExternalNodeJSBinaryCheckbox = new System.Windows.Forms.CheckBox();
            this.DeveloperSettingsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NodeJSDebugPortPicker)).BeginInit();
            this.SuspendLayout();
            // 
            // CheckForUpdatesCheckbox
            // 
            this.CheckForUpdatesCheckbox.AutoSize = true;
            this.CheckForUpdatesCheckbox.Checked = global::Appium.Properties.Settings.Default.CheckForUpdates;
            this.CheckForUpdatesCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckForUpdatesCheckbox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Appium.Properties.Settings.Default, "CheckForUpdates", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.CheckForUpdatesCheckbox.Location = new System.Drawing.Point(13, 13);
            this.CheckForUpdatesCheckbox.Name = "CheckForUpdatesCheckbox";
            this.CheckForUpdatesCheckbox.Size = new System.Drawing.Size(118, 17);
            this.CheckForUpdatesCheckbox.TabIndex = 0;
            this.CheckForUpdatesCheckbox.Text = "Check For Updates";
            this.CheckForUpdatesCheckbox.UseVisualStyleBackColor = true;
            // 
            // QuietLoggingCheckbox
            // 
            this.QuietLoggingCheckbox.AutoSize = true;
            this.QuietLoggingCheckbox.Checked = global::Appium.Properties.Settings.Default.QuietLogging;
            this.QuietLoggingCheckbox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Appium.Properties.Settings.Default, "QuietLogging", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.QuietLoggingCheckbox.Location = new System.Drawing.Point(12, 36);
            this.QuietLoggingCheckbox.Name = "QuietLoggingCheckbox";
            this.QuietLoggingCheckbox.Size = new System.Drawing.Size(92, 17);
            this.QuietLoggingCheckbox.TabIndex = 1;
            this.QuietLoggingCheckbox.Text = "Quiet Logging";
            this.QuietLoggingCheckbox.UseVisualStyleBackColor = true;
            // 
            // KeepArtifactsCheckbox
            // 
            this.KeepArtifactsCheckbox.AutoSize = true;
            this.KeepArtifactsCheckbox.Checked = global::Appium.Properties.Settings.Default.KeepArtifacts;
            this.KeepArtifactsCheckbox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Appium.Properties.Settings.Default, "KeepArtifacts", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.KeepArtifactsCheckbox.Location = new System.Drawing.Point(13, 59);
            this.KeepArtifactsCheckbox.Name = "KeepArtifactsCheckbox";
            this.KeepArtifactsCheckbox.Size = new System.Drawing.Size(92, 17);
            this.KeepArtifactsCheckbox.TabIndex = 2;
            this.KeepArtifactsCheckbox.Text = "Keep Artifacts";
            this.KeepArtifactsCheckbox.UseVisualStyleBackColor = true;
            // 
            // ResetApplicationCheckbox
            // 
            this.ResetApplicationCheckbox.AutoSize = true;
            this.ResetApplicationCheckbox.Checked = global::Appium.Properties.Settings.Default.ResetApplicationState;
            this.ResetApplicationCheckbox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Appium.Properties.Settings.Default, "ResetApplicationState", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ResetApplicationCheckbox.Location = new System.Drawing.Point(12, 82);
            this.ResetApplicationCheckbox.Name = "ResetApplicationCheckbox";
            this.ResetApplicationCheckbox.Size = new System.Drawing.Size(230, 17);
            this.ResetApplicationCheckbox.TabIndex = 3;
            this.ResetApplicationCheckbox.Text = "Reset Application State After Each Session";
            this.ResetApplicationCheckbox.UseVisualStyleBackColor = true;
            // 
            // PreLaunchApplicationCheckbox
            // 
            this.PreLaunchApplicationCheckbox.AutoSize = true;
            this.PreLaunchApplicationCheckbox.Checked = global::Appium.Properties.Settings.Default.PrelaunchApplication;
            this.PreLaunchApplicationCheckbox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Appium.Properties.Settings.Default, "PrelaunchApplication", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PreLaunchApplicationCheckbox.Location = new System.Drawing.Point(12, 105);
            this.PreLaunchApplicationCheckbox.Name = "PreLaunchApplicationCheckbox";
            this.PreLaunchApplicationCheckbox.Size = new System.Drawing.Size(129, 17);
            this.PreLaunchApplicationCheckbox.TabIndex = 4;
            this.PreLaunchApplicationCheckbox.Text = "Prelaunch Application";
            this.PreLaunchApplicationCheckbox.UseVisualStyleBackColor = true;
            // 
            // DeveloperModeCheckbox
            // 
            this.DeveloperModeCheckbox.AutoSize = true;
            this.DeveloperModeCheckbox.Checked = global::Appium.Properties.Settings.Default.DeveloperMode;
            this.DeveloperModeCheckbox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Appium.Properties.Settings.Default, "DeveloperMode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DeveloperModeCheckbox.Location = new System.Drawing.Point(12, 128);
            this.DeveloperModeCheckbox.Name = "DeveloperModeCheckbox";
            this.DeveloperModeCheckbox.Size = new System.Drawing.Size(105, 17);
            this.DeveloperModeCheckbox.TabIndex = 5;
            this.DeveloperModeCheckbox.Text = "Developer Mode";
            this.DeveloperModeCheckbox.UseVisualStyleBackColor = true;
            // 
            // DeveloperSettingsGroupBox
            // 
            this.DeveloperSettingsGroupBox.Controls.Add(this.ExternalAppiumPackageBrowse);
            this.DeveloperSettingsGroupBox.Controls.Add(this.ExternalNodeJSBinaryBrowseButton);
            this.DeveloperSettingsGroupBox.Controls.Add(this.ExternalAppiumPackageTextBox);
            this.DeveloperSettingsGroupBox.Controls.Add(this.ExternalNodeJSBinaryTextBox);
            this.DeveloperSettingsGroupBox.Controls.Add(this.NodeJSDebugPortPicker);
            this.DeveloperSettingsGroupBox.Controls.Add(this.BreakOnApplicationStartCheckbox);
            this.DeveloperSettingsGroupBox.Controls.Add(this.NodeJSDebugPortCheckbox);
            this.DeveloperSettingsGroupBox.Controls.Add(this.UseExternalAppiumPackageCheckbox);
            this.DeveloperSettingsGroupBox.Controls.Add(this.UseExternalNodeJSBinaryCheckbox);
            this.DeveloperSettingsGroupBox.Location = new System.Drawing.Point(13, 155);
            this.DeveloperSettingsGroupBox.Name = "DeveloperSettingsGroupBox";
            this.DeveloperSettingsGroupBox.Size = new System.Drawing.Size(437, 121);
            this.DeveloperSettingsGroupBox.TabIndex = 6;
            this.DeveloperSettingsGroupBox.TabStop = false;
            this.DeveloperSettingsGroupBox.Text = "Developer Settings";
            // 
            // ExternalAppiumPackageBrowse
            // 
            this.ExternalAppiumPackageBrowse.Location = new System.Drawing.Point(356, 46);
            this.ExternalAppiumPackageBrowse.Name = "ExternalAppiumPackageBrowse";
            this.ExternalAppiumPackageBrowse.Size = new System.Drawing.Size(75, 23);
            this.ExternalAppiumPackageBrowse.TabIndex = 13;
            this.ExternalAppiumPackageBrowse.Text = "Browse";
            this.ExternalAppiumPackageBrowse.UseVisualStyleBackColor = true;
            // 
            // ExternalNodeJSBinaryBrowseButton
            // 
            this.ExternalNodeJSBinaryBrowseButton.Location = new System.Drawing.Point(356, 17);
            this.ExternalNodeJSBinaryBrowseButton.Name = "ExternalNodeJSBinaryBrowseButton";
            this.ExternalNodeJSBinaryBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.ExternalNodeJSBinaryBrowseButton.TabIndex = 0;
            this.ExternalNodeJSBinaryBrowseButton.Text = "Browse";
            this.ExternalNodeJSBinaryBrowseButton.UseVisualStyleBackColor = true;
            // 
            // ExternalAppiumPackageTextBox
            // 
            this.ExternalAppiumPackageTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Appium.Properties.Settings.Default, "ExternalAppiumPackage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ExternalAppiumPackageTextBox.Location = new System.Drawing.Point(183, 45);
            this.ExternalAppiumPackageTextBox.Name = "ExternalAppiumPackageTextBox";
            this.ExternalAppiumPackageTextBox.Size = new System.Drawing.Size(167, 20);
            this.ExternalAppiumPackageTextBox.TabIndex = 12;
            this.ExternalAppiumPackageTextBox.Text = global::Appium.Properties.Settings.Default.ExternalAppiumPackage;
            // 
            // ExternalNodeJSBinaryTextBox
            // 
            this.ExternalNodeJSBinaryTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Appium.Properties.Settings.Default, "ExternalNodeJSBinary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ExternalNodeJSBinaryTextBox.Location = new System.Drawing.Point(182, 19);
            this.ExternalNodeJSBinaryTextBox.Name = "ExternalNodeJSBinaryTextBox";
            this.ExternalNodeJSBinaryTextBox.Size = new System.Drawing.Size(167, 20);
            this.ExternalNodeJSBinaryTextBox.TabIndex = 11;
            this.ExternalNodeJSBinaryTextBox.Text = global::Appium.Properties.Settings.Default.ExternalNodeJSBinary;
            // 
            // NodeJSDebugPortPicker
            // 
            this.NodeJSDebugPortPicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Appium.Properties.Settings.Default, "NodeJSDebugPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.NodeJSDebugPortPicker.Location = new System.Drawing.Point(133, 72);
            this.NodeJSDebugPortPicker.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.NodeJSDebugPortPicker.Name = "NodeJSDebugPortPicker";
            this.NodeJSDebugPortPicker.Size = new System.Drawing.Size(59, 20);
            this.NodeJSDebugPortPicker.TabIndex = 7;
            this.NodeJSDebugPortPicker.Value = global::Appium.Properties.Settings.Default.NodeJSDebugPort;
            // 
            // BreakOnApplicationStartCheckbox
            // 
            this.BreakOnApplicationStartCheckbox.AutoSize = true;
            this.BreakOnApplicationStartCheckbox.Checked = global::Appium.Properties.Settings.Default.BreakOnApplicationStart;
            this.BreakOnApplicationStartCheckbox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Appium.Properties.Settings.Default, "BreakOnApplicationStart", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BreakOnApplicationStartCheckbox.Location = new System.Drawing.Point(6, 96);
            this.BreakOnApplicationStartCheckbox.Name = "BreakOnApplicationStartCheckbox";
            this.BreakOnApplicationStartCheckbox.Size = new System.Drawing.Size(151, 17);
            this.BreakOnApplicationStartCheckbox.TabIndex = 10;
            this.BreakOnApplicationStartCheckbox.Text = "Break On Application Start";
            this.BreakOnApplicationStartCheckbox.UseVisualStyleBackColor = true;
            // 
            // NodeJSDebugPortCheckbox
            // 
            this.NodeJSDebugPortCheckbox.AutoSize = true;
            this.NodeJSDebugPortCheckbox.Checked = global::Appium.Properties.Settings.Default.UseNodeJSDebugging;
            this.NodeJSDebugPortCheckbox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Appium.Properties.Settings.Default, "UseNodeJSDebugging", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.NodeJSDebugPortCheckbox.Location = new System.Drawing.Point(6, 73);
            this.NodeJSDebugPortCheckbox.Name = "NodeJSDebugPortCheckbox";
            this.NodeJSDebugPortCheckbox.Size = new System.Drawing.Size(121, 17);
            this.NodeJSDebugPortCheckbox.TabIndex = 9;
            this.NodeJSDebugPortCheckbox.Text = "NodeJS Debug Port";
            this.NodeJSDebugPortCheckbox.UseVisualStyleBackColor = true;
            // 
            // UseExternalAppiumPackageCheckbox
            // 
            this.UseExternalAppiumPackageCheckbox.AutoSize = true;
            this.UseExternalAppiumPackageCheckbox.Checked = global::Appium.Properties.Settings.Default.UseExternalAppiumPackage;
            this.UseExternalAppiumPackageCheckbox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Appium.Properties.Settings.Default, "UseExternalAppiumPackage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.UseExternalAppiumPackageCheckbox.Location = new System.Drawing.Point(6, 50);
            this.UseExternalAppiumPackageCheckbox.Name = "UseExternalAppiumPackageCheckbox";
            this.UseExternalAppiumPackageCheckbox.Size = new System.Drawing.Size(170, 17);
            this.UseExternalAppiumPackageCheckbox.TabIndex = 8;
            this.UseExternalAppiumPackageCheckbox.Text = "Use External Appium Package";
            this.UseExternalAppiumPackageCheckbox.UseVisualStyleBackColor = true;
            // 
            // UseExternalNodeJSBinaryCheckbox
            // 
            this.UseExternalNodeJSBinaryCheckbox.AutoSize = true;
            this.UseExternalNodeJSBinaryCheckbox.Checked = global::Appium.Properties.Settings.Default.UseExternalNodeJSBinary;
            this.UseExternalNodeJSBinaryCheckbox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Appium.Properties.Settings.Default, "UseExternalNodeJSBinary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.UseExternalNodeJSBinaryCheckbox.Location = new System.Drawing.Point(6, 21);
            this.UseExternalNodeJSBinaryCheckbox.Name = "UseExternalNodeJSBinaryCheckbox";
            this.UseExternalNodeJSBinaryCheckbox.Size = new System.Drawing.Size(159, 17);
            this.UseExternalNodeJSBinaryCheckbox.TabIndex = 7;
            this.UseExternalNodeJSBinaryCheckbox.Text = "Use External NodeJS Binary";
            this.UseExternalNodeJSBinaryCheckbox.UseVisualStyleBackColor = true;
            // 
            // PreferencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 282);
            this.Controls.Add(this.DeveloperSettingsGroupBox);
            this.Controls.Add(this.DeveloperModeCheckbox);
            this.Controls.Add(this.PreLaunchApplicationCheckbox);
            this.Controls.Add(this.ResetApplicationCheckbox);
            this.Controls.Add(this.KeepArtifactsCheckbox);
            this.Controls.Add(this.QuietLoggingCheckbox);
            this.Controls.Add(this.CheckForUpdatesCheckbox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PreferencesForm";
            this.Text = "Appium Preferences";
            this.DeveloperSettingsGroupBox.ResumeLayout(false);
            this.DeveloperSettingsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NodeJSDebugPortPicker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.CheckBox CheckForUpdatesCheckbox;
        public System.Windows.Forms.CheckBox QuietLoggingCheckbox;
        public System.Windows.Forms.CheckBox KeepArtifactsCheckbox;
        public System.Windows.Forms.CheckBox ResetApplicationCheckbox;
        public System.Windows.Forms.CheckBox PreLaunchApplicationCheckbox;
        public System.Windows.Forms.CheckBox DeveloperModeCheckbox;
        public System.Windows.Forms.GroupBox DeveloperSettingsGroupBox;
        public System.Windows.Forms.CheckBox NodeJSDebugPortCheckbox;
        public System.Windows.Forms.CheckBox UseExternalAppiumPackageCheckbox;
        public System.Windows.Forms.CheckBox UseExternalNodeJSBinaryCheckbox;
        public System.Windows.Forms.CheckBox BreakOnApplicationStartCheckbox;
        public System.Windows.Forms.NumericUpDown NodeJSDebugPortPicker;
        public System.Windows.Forms.TextBox ExternalAppiumPackageTextBox;
        public System.Windows.Forms.TextBox ExternalNodeJSBinaryTextBox;
        private System.Windows.Forms.Button ExternalAppiumPackageBrowse;
        private System.Windows.Forms.Button ExternalNodeJSBinaryBrowseButton;
    }
}