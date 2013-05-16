namespace Appium
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
            this.IPAddressTextBox = new System.Windows.Forms.TextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.PortTextBox = new System.Windows.Forms.NumericUpDown();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
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
            this.AndroidPackageCheckbox = new System.Windows.Forms.CheckBox();
            this.AndroidPackageTextBox = new System.Windows.Forms.TextBox();
            this.AndroidActivityTextBox = new System.Windows.Forms.TextBox();
            this.AndroidActivityCheckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.PortTextBox)).BeginInit();
            this.MainMenu.SuspendLayout();
            this.StatusBar.SuspendLayout();
            this.AndroidGroupBox.SuspendLayout();
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
            // IPAddressTextBox
            // 
            this.IPAddressTextBox.Location = new System.Drawing.Point(86, 26);
            this.IPAddressTextBox.Multiline = true;
            this.IPAddressTextBox.Name = "IPAddressTextBox";
            this.IPAddressTextBox.Size = new System.Drawing.Size(74, 20);
            this.IPAddressTextBox.TabIndex = 2;
            this.IPAddressTextBox.Text = "127.0.0.1";
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
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(201, 26);
            this.PortTextBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(56, 20);
            this.PortTextBox.TabIndex = 4;
            this.PortTextBox.Value = new decimal(new int[] {
            4723,
            0,
            0,
            0});
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
            this.FileMenuExitItem});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(37, 20);
            this.FileMenu.Text = "File";
            // 
            // FileMenuExitItem
            // 
            this.FileMenuExitItem.Name = "FileMenuExitItem";
            this.FileMenuExitItem.Size = new System.Drawing.Size(92, 22);
            this.FileMenuExitItem.Text = "Exit";
            this.FileMenuExitItem.Click += new System.EventHandler(this.FileMenuExitItem_Click);
            // 
            // LaunchButton
            // 
            this.LaunchButton.Location = new System.Drawing.Point(388, 24);
            this.LaunchButton.Name = "LaunchButton";
            this.LaunchButton.Size = new System.Drawing.Size(81, 49);
            this.LaunchButton.TabIndex = 6;
            this.LaunchButton.Text = "Launch";
            this.LaunchButton.UseVisualStyleBackColor = true;
            this.LaunchButton.Click += new System.EventHandler(this.LaunchButton_Click);
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusBarText});
            this.StatusBar.Location = new System.Drawing.Point(0, 140);
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
            this.ApplicationPathTextBox.Location = new System.Drawing.Point(86, 52);
            this.ApplicationPathTextBox.Name = "ApplicationPathTextBox";
            this.ApplicationPathTextBox.Size = new System.Drawing.Size(215, 20);
            this.ApplicationPathTextBox.TabIndex = 8;
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
            this.ApplicationPathBrowseButton.Click += new System.EventHandler(this.AppPathBrowseButton_Click);
            // 
            // UseRemoteServerCheckbox
            // 
            this.UseRemoteServerCheckbox.AutoSize = true;
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
            this.ApplicationPathCheckbox.Location = new System.Drawing.Point(12, 54);
            this.ApplicationPathCheckbox.Name = "ApplicationPathCheckbox";
            this.ApplicationPathCheckbox.Size = new System.Drawing.Size(73, 17);
            this.ApplicationPathCheckbox.TabIndex = 12;
            this.ApplicationPathCheckbox.Text = "App Path:";
            this.ApplicationPathCheckbox.UseVisualStyleBackColor = true;
            // 
            // AndroidGroupBox
            // 
            this.AndroidGroupBox.Controls.Add(this.AndroidActivityTextBox);
            this.AndroidGroupBox.Controls.Add(this.AndroidActivityCheckbox);
            this.AndroidGroupBox.Controls.Add(this.AndroidPackageTextBox);
            this.AndroidGroupBox.Controls.Add(this.AndroidPackageCheckbox);
            this.AndroidGroupBox.Location = new System.Drawing.Point(12, 78);
            this.AndroidGroupBox.Name = "AndroidGroupBox";
            this.AndroidGroupBox.Size = new System.Drawing.Size(457, 55);
            this.AndroidGroupBox.TabIndex = 13;
            this.AndroidGroupBox.TabStop = false;
            this.AndroidGroupBox.Text = "Android";
            // 
            // AndroidPackageCheckbox
            // 
            this.AndroidPackageCheckbox.AutoSize = true;
            this.AndroidPackageCheckbox.Location = new System.Drawing.Point(8, 22);
            this.AndroidPackageCheckbox.Name = "AndroidPackageCheckbox";
            this.AndroidPackageCheckbox.Size = new System.Drawing.Size(72, 17);
            this.AndroidPackageCheckbox.TabIndex = 0;
            this.AndroidPackageCheckbox.Text = "Package:";
            this.AndroidPackageCheckbox.UseVisualStyleBackColor = true;
            // 
            // AndroidPackageTextBox
            // 
            this.AndroidPackageTextBox.Location = new System.Drawing.Point(86, 20);
            this.AndroidPackageTextBox.Name = "AndroidPackageTextBox";
            this.AndroidPackageTextBox.Size = new System.Drawing.Size(139, 20);
            this.AndroidPackageTextBox.TabIndex = 1;
            // 
            // AndroidActivityTextBox
            // 
            this.AndroidActivityTextBox.Location = new System.Drawing.Point(307, 20);
            this.AndroidActivityTextBox.Name = "AndroidActivityTextBox";
            this.AndroidActivityTextBox.Size = new System.Drawing.Size(139, 20);
            this.AndroidActivityTextBox.TabIndex = 3;
            // 
            // AndroidActivityCheckbox
            // 
            this.AndroidActivityCheckbox.AutoSize = true;
            this.AndroidActivityCheckbox.Location = new System.Drawing.Point(238, 22);
            this.AndroidActivityCheckbox.Name = "AndroidActivityCheckbox";
            this.AndroidActivityCheckbox.Size = new System.Drawing.Size(63, 17);
            this.AndroidActivityCheckbox.TabIndex = 2;
            this.AndroidActivityCheckbox.Text = "Activity:";
            this.AndroidActivityCheckbox.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 162);
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
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PortTextBox)).EndInit();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.AndroidGroupBox.ResumeLayout(false);
            this.AndroidGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label IPAddressLabel;
        private System.Windows.Forms.TextBox IPAddressTextBox;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.NumericUpDown PortTextBox;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem FileMenu;
        private System.Windows.Forms.ToolStripMenuItem FileMenuExitItem;
        private System.Windows.Forms.Button LaunchButton;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.ToolStripStatusLabel StatusBarText;
        private System.Windows.Forms.TextBox ApplicationPathTextBox;
        private System.Windows.Forms.Label AppPathLabel;
        private System.Windows.Forms.Button ApplicationPathBrowseButton;
        private System.Windows.Forms.CheckBox UseRemoteServerCheckbox;
        private System.Windows.Forms.CheckBox ApplicationPathCheckbox;
        private System.Windows.Forms.GroupBox AndroidGroupBox;
        private System.Windows.Forms.CheckBox AndroidPackageCheckbox;
        private System.Windows.Forms.TextBox AndroidActivityTextBox;
        private System.Windows.Forms.CheckBox AndroidActivityCheckbox;
        private System.Windows.Forms.TextBox AndroidPackageTextBox;
    }
}

