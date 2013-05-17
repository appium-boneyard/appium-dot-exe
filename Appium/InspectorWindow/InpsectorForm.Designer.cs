namespace Appium.InspectorWindow
{
    partial class InpsectorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InpsectorForm));
            this.DOMTreeView = new System.Windows.Forms.TreeView();
            this.DetailsTextBox = new System.Windows.Forms.TextBox();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.ScreenshotPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ScreenshotPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // DOMTreeView
            // 
            this.DOMTreeView.Location = new System.Drawing.Point(13, 13);
            this.DOMTreeView.Name = "DOMTreeView";
            this.DOMTreeView.Size = new System.Drawing.Size(298, 490);
            this.DOMTreeView.TabIndex = 0;
            // 
            // DetailsTextBox
            // 
            this.DetailsTextBox.Location = new System.Drawing.Point(317, 367);
            this.DetailsTextBox.Multiline = true;
            this.DetailsTextBox.Name = "DetailsTextBox";
            this.DetailsTextBox.Size = new System.Drawing.Size(240, 136);
            this.DetailsTextBox.TabIndex = 1;
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(317, 13);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(75, 23);
            this.RefreshButton.TabIndex = 2;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // ScreenshotPictureBox
            // 
            this.ScreenshotPictureBox.Location = new System.Drawing.Point(317, 41);
            this.ScreenshotPictureBox.Name = "ScreenshotPictureBox";
            this.ScreenshotPictureBox.Size = new System.Drawing.Size(240, 320);
            this.ScreenshotPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ScreenshotPictureBox.TabIndex = 3;
            this.ScreenshotPictureBox.TabStop = false;
            // 
            // InpsectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 611);
            this.Controls.Add(this.ScreenshotPictureBox);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.DetailsTextBox);
            this.Controls.Add(this.DOMTreeView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InpsectorForm";
            this.Text = "Inspector";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Inpsector_FormClosing);
            this.Load += new System.EventHandler(this.Inpsector_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ScreenshotPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView DOMTreeView;
        private System.Windows.Forms.TextBox DetailsTextBox;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.PictureBox ScreenshotPictureBox;
    }
}