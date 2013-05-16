namespace Appium.MainWindow
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.IPAddressLabel = new System.Windows.Forms.Label();
            this.PortLabel = new System.Windows.Forms.Label();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenuPreferencesItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenuExitItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LaunchButton = new System.Windows.Forms.Button();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.StatusBarText = new System.Windows.Forms.ToolStripStatusLabel();
            this.ApplicationPathTextBox = new System.Windows.Forms.TextBox();
            this.AppPathLabel = new System.Windows.Forms.Label();
            this.ApplicationPathBrowseButton = new System.Windows.Forms.Button();
            this.UseRemoteServerCheckbox = new System.Windows.Forms.CheckBox();
            this.ApplicationPathCheckbox = new System.Windows.Forms.CheckBox();
            this.AndroidGroupBox = new System.Windows.Forms.GroupBox();
            this.LaunchAVDComboBox = new System.Windows.Forms.ComboBox();
            this.WaitForAndroidActivityTextBox = new System.Windows.Forms.TextBox();
            this.WaitForAndroidActivityCheckbox = new System.Windows.Forms.CheckBox();
            this.LaunchAVDCheckbox = new System.Windows.Forms.CheckBox();
            this.AndroidActivityTextBox = new System.Windows.Forms.TextBox();
            this.AndroidActivityCheckbox = new System.Windows.Forms.CheckBox();
            this.AndroidPackageTextBox = new System.Windows.Forms.TextBox();
            this.AndroidPackageCheckbox = new System.Windows.Forms.CheckBox();
            this.PortTextBox = new System.Windows.Forms.NumericUpDown();
            this.IPAddressTextBox = new System.Windows.Forms.TextBox();
            this.MainMenu.SuspendLayout();
            this.StatusBar.SuspendLayout();
            this.AndroidGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PortTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // IPAddressLabel
            // 
            this.IPAddressLabel.AutoSize = true;
            this.IPAddressLabel.Location = new System.Drawing.Point(19, 29);
            this.IPAddressLabel.Name = "IPAddressLabel";
            this.IPAddressLabel.Size = new System.Drawing.Size(61, 13);
            this.IPAddressLabel.TabIndex = 0;
            this.IPAddressLabel.Text = "IP Address:";
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(166, 29);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(29, 13);
            this.PortLabel.TabIndex = 3;
            this.PortLabel.Text = "Port:";
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(481, 24);
            this.MainMenu.TabIndex = 5;
            this.MainMenu.Text = "menuStrip1";
            // 
            // FileMenu
            // 
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuPreferencesItem,
            this.FileMenuExitItem});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(37, 20);
            this.FileMenu.Text = "File";
            // 
            // FileMenuPreferencesItem
            // 
            this.FileMenuPreferencesItem.Name = "FileMenuPreferencesItem";
            this.FileMenuPreferencesItem.Size = new System.Drawing.Size(135, 22);
            this.FileMenuPreferencesItem.Text = "Preferences";
            // 
            // FileMenuExitItem
            // 
            this.FileMenuExitItem.Name = "FileMenuExitItem";
            this.FileMenuExitItem.Size = new System.Drawing.Size(135, 22);
            this.FileMenuExitItem.Text = "Exit";
            // 
            // LaunchButton
            // 
            this.LaunchButton.Location = new System.Drawing.Point(388, 24);
            this.LaunchButton.Name = "LaunchButton";
            this.LaunchButton.Size = new System.Drawing.Size(81, 49);
            this.LaunchButton.TabIndex = 6;
            this.LaunchButton.Text = "Launch";
            this.LaunchButton.UseVisualStyleBackColor = true;
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusBarText});
            this.StatusBar.Location = new System.Drawing.Point(0, 165);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(481, 22);
            this.StatusBar.TabIndex = 7;
            this.StatusBar.Text = "statusStrip1";
            // 
            // StatusBarText
            // 
            this.StatusBarText.Name = "StatusBarText";
            this.StatusBarText.Size = new System.Drawing.Size(0, 17);
            // 
            // ApplicationPathTextBox
            // 
            this.ApplicationPathTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Appium.Properties.Settings.Default, "ApplicationPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ApplicationPathTextBox.Location = new System.Drawing.Point(86, 52);
            this.ApplicationPathTextBox.Name = "ApplicationPathTextBox";
            this.ApplicationPathTextBox.Size = new System.Drawing.Size(215, 20);
            this.ApplicationPathTextBox.TabIndex = 8;
            this.ApplicationPathTextBox.Text = global::Appium.Properties.Settings.Default.ApplicationPath;
            // 
            // AppPathLabel
            // 
            this.AppPathLabel.AutoSize = true;
            this.AppPathLabel.Location = new System.Drawing.Point(19, 55);
            this.AppPathLabel.Name = "AppPathLabel";
            this.AppPathLabel.Size = new System.Drawing.Size(0, 13);
            this.AppPathLabel.TabIndex = 9;
            // 
            // ApplicationPathBrowseButton
            // 
            this.ApplicationPathBrowseButton.Location = new System.Drawing.Point(307, 50);
            this.ApplicationPathBrowseButton.Name = "ApplicationPathBrowseButton";
            this.ApplicationPathBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.ApplicationPathBrowseButton.TabIndex = 10;
            this.ApplicationPathBrowseButton.Text = "Browse";
            this.ApplicationPathBrowseButton.UseVisualStyleBackColor = true;
            // 
            // UseRemoteServerCheckbox
            // 
            this.UseRemoteServerCheckbox.AutoSize = true;
            this.UseRemoteServerCheckbox.Checked = global::Appium.Properties.Settings.Default.UseRemoteServer;
            this.UseRemoteServerCheckbox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Appium.Properties.Settings.Default, "UseRemoteServer", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.UseRemoteServerCheckbox.Location = new System.Drawing.Point(263, 27);
            this.UseRemoteServerCheckbox.Name = "UseRemoteServerCheckbox";
            this.UseRemoteServerCheckbox.Size = new System.Drawing.Size(119, 17);
            this.UseRemoteServerCheckbox.TabIndex = 11;
            this.UseRemoteServerCheckbox.Text = "Use Remote Server";
            this.UseRemoteServerCheckbox.UseVisualStyleBackColor = true;
            // 
            // ApplicationPathCheckbox
            // 
            this.ApplicationPathCheckbox.AutoSize = true;
            this.ApplicationPathCheckbox.Checked = global::Appium.Properties.Settings.Default.UseApplicationPath;
            this.ApplicationPathCheckbox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Appium.Properties.Settings.Default, "UseApplicationPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ApplicationPathCheckbox.Location = new System.Drawing.Point(12, 54);
            this.ApplicationPathCheckbox.Name = "ApplicationPathCheckbox";
            this.ApplicationPathCheckbox.Size = new System.Drawing.Size(73, 17);
            this.ApplicationPathCheckbox.TabIndex = 12;
            this.ApplicationPathCheckbox.Text = "App Path:";
            this.ApplicationPathCheckbox.UseVisualStyleBackColor = true;
            // 
            // AndroidGroupBox
            // 
            this.AndroidGroupBox.Controls.Add(this.LaunchAVDComboBox);
            this.AndroidGroupBox.Controls.Add(this.WaitForAndroidActivityTextBox);
            this.AndroidGroupBox.Controls.Add(this.WaitForAndroidActivityCheckbox);
            this.AndroidGroupBox.Controls.Add(this.LaunchAVDCheckbox);
            this.AndroidGroupBox.Controls.Add(this.AndroidActivityTextBox);
            this.AndroidGroupBox.Controls.Add(this.AndroidActivityCheckbox);
            this.AndroidGroupBox.Controls.Add(this.AndroidPackageTextBox);
            this.AndroidGroupBox.Controls.Add(this.AndroidPackageCheckbox);
            this.AndroidGroupBox.Location = new System.Drawing.Point(12, 78);
            this.AndroidGroupBox.Name = "AndroidGroupBox";
            this.AndroidGroupBox.Size = new System.Drawing.Size(457, 80);
            this.AndroidGroupBox.TabIndex = 13;
            this.AndroidGroupBox.TabStop = false;
            this.AndroidGroupBox.Text = "Android";
            // 
            // LaunchAVDComboBox
            // 
            this.LaunchAVDComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Appium.Properties.Settings.Default, "AVD", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LaunchAVDComboBox.FormattingEnabled = true;
            this.LaunchAVDComboBox.Location = new System.Drawing.Point(105, 47);
            this.LaunchAVDComboBox.Name = "LaunchAVDComboBox";
            this.LaunchAVDComboBox.Size = new System.Drawing.Size(84, 21);
            this.LaunchAVDComboBox.TabIndex = 8;
            this.LaunchAVDComboBox.Text = global::Appium.Properties.Settings.Default.AVD;
            // 
            // WaitForAndroidActivityTextBox
            // 
            this.WaitForAndroidActivityTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Appium.Properties.Settings.Default, "AndroidWaitActivity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.WaitForAndroidActivityTextBox.Location = new System.Drawing.Point(307, 46);
            this.WaitForAndroidActivityTextBox.Name = "WaitForAndroidActivityTextBox";
            this.WaitForAndroidActivityTextBox.Size = new System.Drawing.Size(139, 20);
            this.WaitForAndroidActivityTextBox.TabIndex = 7;
            this.WaitForAndroidActivityTextBox.Text = global::Appium.Properties.Settings.Default.AndroidWaitActivity;
            // 
            // WaitForAndroidActivityCheckbox
            // 
            this.WaitForAndroidActivityCheckbox.AutoSize = true;
            this.WaitForAndroidActivityCheckbox.Checked = global::Appium.Properties.Settings.Default.UseAndroidWaitActivity;
            this.WaitForAndroidActivityCheckbox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Appium.Properties.Settings.Default, "UseAndroidWaitActivity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.WaitForAndroidActivityCheckbox.Location = new System.Drawing.Point(195, 48);
            this.WaitForAndroidActivityCheckbox.Name = "WaitForAndroidActivityCheckbox";
            this.WaitForAndroidActivityCheckbox.Size = new System.Drawing.Size(106, 17);
            this.WaitForAndroidActivityCheckbox.TabIndex = 6;
            this.WaitForAndroidActivityCheckbox.Text = "Wait For Activity:";
            this.WaitForAndroidActivityCheckbox.UseVisualStyleBackColor = true;
            // 
            // LaunchAVDCheckbox
            // 
            this.LaunchAVDCheckbox.AutoSize = true;
            this.LaunchAVDCheckbox.Checked = global::Appium.Properties.Settings.Default.LaunchAVD;
            this.LaunchAVDCheckbox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Appium.Properties.Settings.Default, "LaunchAVD", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LaunchAVDCheckbox.Location = new System.Drawing.Point(8, 48);
            this.LaunchAVDCheckbox.Name = "LaunchAVDCheckbox";
            this.LaunchAVDCheckbox.Size = new System.Drawing.Size(90, 17);
            this.LaunchAVDCheckbox.TabIndex = 4;
            this.LaunchAVDCheckbox.Text = "Launch AVD:";
            this.LaunchAVDCheckbox.UseVisualStyleBackColor = true;
            // 
            // AndroidActivityTextBox
            // 
            this.AndroidActivityTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Appium.Properties.Settings.Default, "AndroidActivity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.AndroidActivityTextBox.Location = new System.Drawing.Point(307, 20);
            this.AndroidActivityTextBox.Name = "AndroidActivityTextBox";
            this.AndroidActivityTextBox.Size = new System.Drawing.Size(139, 20);
            this.AndroidActivityTextBox.TabIndex = 3;
            this.AndroidActivityTextBox.Text = global::Appium.Properties.Settings.Default.AndroidActivity;
            // 
            // AndroidActivityCheckbox
            // 
            this.AndroidActivityCheckbox.AutoSize = true;
            this.AndroidActivityCheckbox.Checked = global::Appium.Properties.Settings.Default.UseAndroidActivity;
            this.AndroidActivityCheckbox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Appium.Properties.Settings.Default, "UseAndroidActivity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.AndroidActivityCheckbox.Location = new System.Drawing.Point(238, 22);
            this.AndroidActivityCheckbox.Name = "AndroidActivityCheckbox";
            this.AndroidActivityCheckbox.Size = new System.Drawing.Size(63, 17);
            this.AndroidActivityCheckbox.TabIndex = 2;
            this.AndroidActivityCheckbox.Text = "Activity:";
            this.AndroidActivityCheckbox.UseVisualStyleBackColor = true;
            // 
            // AndroidPackageTextBox
            // 
            this.AndroidPackageTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Appium.Properties.Settings.Default, "AndroidPackage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.AndroidPackageTextBox.Location = new System.Drawing.Point(86, 20);
            this.AndroidPackageTextBox.Name = "AndroidPackageTextBox";
            this.AndroidPackageTextBox.Size = new System.Drawing.Size(139, 20);
            this.AndroidPackageTextBox.TabIndex = 1;
            this.AndroidPackageTextBox.Text = global::Appium.Properties.Settings.Default.AndroidPackage;
            // 
            // AndroidPackageCheckbox
            // 
            this.AndroidPackageCheckbox.AutoSize = true;
            this.AndroidPackageCheckbox.Checked = global::Appium.Properties.Settings.Default.UseAndroidPackage;
            this.AndroidPackageCheckbox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Appium.Properties.Settings.Default, "UseAndroidPackage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.AndroidPackageCheckbox.Location = new System.Drawing.Point(8, 22);
            this.AndroidPackageCheckbox.Name = "AndroidPackageCheckbox";
            this.AndroidPackageCheckbox.Size = new System.Drawing.Size(72, 17);
            this.AndroidPackageCheckbox.TabIndex = 0;
            this.AndroidPackageCheckbox.Text = "Package:";
            this.AndroidPackageCheckbox.UseVisualStyleBackColor = true;
            // 
            // PortTextBox
            // 
            this.PortTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Appium.Properties.Settings.Default, "ServerPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PortTextBox.Location = new System.Drawing.Point(201, 26);
            this.PortTextBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(56, 20);
            this.PortTextBox.TabIndex = 4;
            this.PortTextBox.Value = global::Appium.Properties.Settings.Default.ServerPort;
            // 
            // IPAddressTextBox
            // 
            this.IPAddressTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Appium.Properties.Settings.Default, "ServerAddress", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.IPAddressTextBox.Location = new System.Drawing.Point(86, 26);
            this.IPAddressTextBox.Multiline = true;
            this.IPAddressTextBox.Name = "IPAddressTextBox";
            this.IPAddressTextBox.Size = new System.Drawing.Size(74, 20);
            this.IPAddressTextBox.TabIndex = 2;
            this.IPAddressTextBox.Text = global::Appium.Properties.Settings.Default.ServerAddress;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 187);
            this.Controls.Add(this.AndroidGroupBox);
            this.Controls.Add(this.ApplicationPathCheckbox);
            this.Controls.Add(this.UseRemoteServerCheckbox);
            this.Controls.Add(this.ApplicationPathBrowseButton);
            this.Controls.Add(this.AppPathLabel);
            this.Controls.Add(this.ApplicationPathTextBox);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.LaunchButton);
            this.Controls.Add(this.PortTextBox);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.IPAddressTextBox);
            this.Controls.Add(this.IPAddressLabel);
            this.Controls.Add(this.MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.Text = "Appium";
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.AndroidGroupBox.ResumeLayout(false);
            this.AndroidGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PortTextBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label IPAddressLabel;
        public System.Windows.Forms.TextBox IPAddressTextBox;
        public System.Windows.Forms.Label PortLabel;
        public System.Windows.Forms.NumericUpDown PortTextBox;
        public System.Windows.Forms.MenuStrip MainMenu;
        public System.Windows.Forms.ToolStripMenuItem FileMenu;
        public System.Windows.Forms.ToolStripMenuItem FileMenuExitItem;
        public System.Windows.Forms.Button LaunchButton;
        public System.Windows.Forms.StatusStrip StatusBar;
        public System.Windows.Forms.ToolStripStatusLabel StatusBarText;
        public System.Windows.Forms.TextBox ApplicationPathTextBox;
        public System.Windows.Forms.Label AppPathLabel;
        public System.Windows.Forms.Button ApplicationPathBrowseButton;
        public System.Windows.Forms.CheckBox UseRemoteServerCheckbox;
        public System.Windows.Forms.CheckBox ApplicationPathCheckbox;
        public System.Windows.Forms.GroupBox AndroidGroupBox;
        public System.Windows.Forms.CheckBox AndroidPackageCheckbox;
        public System.Windows.Forms.TextBox AndroidActivityTextBox;
        public System.Windows.Forms.CheckBox AndroidActivityCheckbox;
        public System.Windows.Forms.TextBox AndroidPackageTextBox;
        public System.Windows.Forms.ComboBox LaunchAVDComboBox;
        public System.Windows.Forms.TextBox WaitForAndroidActivityTextBox;
        public System.Windows.Forms.CheckBox WaitForAndroidActivityCheckbox;
        public System.Windows.Forms.CheckBox LaunchAVDCheckbox;
        private System.Windows.Forms.ToolStripMenuItem FileMenuPreferencesItem;
    }
}

