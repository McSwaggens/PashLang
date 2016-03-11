namespace PashIDE
{
    partial class SettingsWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
            this.mainPanel = new System.Windows.Forms.Panel();
            this.CancelButton = new Client.Components.XButton();
            this.DoneButton = new Client.Components.XButton();
            this.AcceptButton = new Client.Components.XButton();
            this.titleBackPanel = new System.Windows.Forms.Panel();
            this.ThemeButton = new Client.Components.XButton();
            this.InfoButton = new Client.Components.XButton();
            this.ProjectButton = new Client.Components.XButton();
            this.GeneralButton = new Client.Components.XButton();
            this.Title = new System.Windows.Forms.Label();
            this.mainPanel.SuspendLayout();
            this.titleBackPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.BackColor = System.Drawing.Color.White;
            this.mainPanel.Controls.Add(this.CancelButton);
            this.mainPanel.Controls.Add(this.DoneButton);
            this.mainPanel.Controls.Add(this.AcceptButton);
            this.mainPanel.Controls.Add(this.titleBackPanel);
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(700, 774);
            this.mainPanel.TabIndex = 0;
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.BackColor = System.Drawing.Color.Transparent;
            this.CancelButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(151)))), ((int)(((byte)(229)))));
            this.CancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CancelButton.DisabledColor = System.Drawing.Color.LightGray;
            this.CancelButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(141)))), ((int)(((byte)(219)))));
            this.CancelButton.Location = new System.Drawing.Point(385, 730);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(141)))), ((int)(((byte)(219)))));
            this.CancelButton.Size = new System.Drawing.Size(97, 32);
            this.CancelButton.TabIndex = 7;
            this.CancelButton.Text = "Cancel";
            // 
            // DoneButton
            // 
            this.DoneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DoneButton.BackColor = System.Drawing.Color.Transparent;
            this.DoneButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(151)))), ((int)(((byte)(229)))));
            this.DoneButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DoneButton.DisabledColor = System.Drawing.Color.LightGray;
            this.DoneButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.DoneButton.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(141)))), ((int)(((byte)(219)))));
            this.DoneButton.Location = new System.Drawing.Point(488, 730);
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(141)))), ((int)(((byte)(219)))));
            this.DoneButton.Size = new System.Drawing.Size(97, 32);
            this.DoneButton.TabIndex = 6;
            this.DoneButton.Text = "Done";
            // 
            // AcceptButton
            // 
            this.AcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AcceptButton.BackColor = System.Drawing.Color.Transparent;
            this.AcceptButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(151)))), ((int)(((byte)(229)))));
            this.AcceptButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AcceptButton.DisabledColor = System.Drawing.Color.LightGray;
            this.AcceptButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.AcceptButton.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(141)))), ((int)(((byte)(219)))));
            this.AcceptButton.Location = new System.Drawing.Point(591, 730);
            this.AcceptButton.Name = "AcceptButton";
            this.AcceptButton.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(141)))), ((int)(((byte)(219)))));
            this.AcceptButton.Size = new System.Drawing.Size(97, 32);
            this.AcceptButton.TabIndex = 5;
            this.AcceptButton.Text = "Accept";
            // 
            // titleBackPanel
            // 
            this.titleBackPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(161)))), ((int)(((byte)(239)))));
            this.titleBackPanel.Controls.Add(this.ThemeButton);
            this.titleBackPanel.Controls.Add(this.InfoButton);
            this.titleBackPanel.Controls.Add(this.ProjectButton);
            this.titleBackPanel.Controls.Add(this.GeneralButton);
            this.titleBackPanel.Controls.Add(this.Title);
            this.titleBackPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleBackPanel.Location = new System.Drawing.Point(0, 0);
            this.titleBackPanel.Name = "titleBackPanel";
            this.titleBackPanel.Size = new System.Drawing.Size(700, 58);
            this.titleBackPanel.TabIndex = 1;
            // 
            // ThemeButton
            // 
            this.ThemeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ThemeButton.BackColor = System.Drawing.Color.Transparent;
            this.ThemeButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(151)))), ((int)(((byte)(229)))));
            this.ThemeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ThemeButton.DisabledColor = System.Drawing.Color.LightGray;
            this.ThemeButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.ThemeButton.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(141)))), ((int)(((byte)(219)))));
            this.ThemeButton.Location = new System.Drawing.Point(585, 23);
            this.ThemeButton.Name = "ThemeButton";
            this.ThemeButton.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(141)))), ((int)(((byte)(219)))));
            this.ThemeButton.Size = new System.Drawing.Size(97, 32);
            this.ThemeButton.TabIndex = 4;
            this.ThemeButton.Text = "Themes";
            // 
            // InfoButton
            // 
            this.InfoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoButton.BackColor = System.Drawing.Color.Transparent;
            this.InfoButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(151)))), ((int)(((byte)(229)))));
            this.InfoButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InfoButton.DisabledColor = System.Drawing.Color.LightGray;
            this.InfoButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.InfoButton.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(141)))), ((int)(((byte)(219)))));
            this.InfoButton.Location = new System.Drawing.Point(482, 23);
            this.InfoButton.Name = "InfoButton";
            this.InfoButton.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(141)))), ((int)(((byte)(219)))));
            this.InfoButton.Size = new System.Drawing.Size(97, 32);
            this.InfoButton.TabIndex = 3;
            this.InfoButton.Text = "Info";
            // 
            // ProjectButton
            // 
            this.ProjectButton.BackColor = System.Drawing.Color.Transparent;
            this.ProjectButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(151)))), ((int)(((byte)(229)))));
            this.ProjectButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ProjectButton.DisabledColor = System.Drawing.Color.LightGray;
            this.ProjectButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.ProjectButton.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(141)))), ((int)(((byte)(219)))));
            this.ProjectButton.Location = new System.Drawing.Point(115, 23);
            this.ProjectButton.Name = "ProjectButton";
            this.ProjectButton.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(141)))), ((int)(((byte)(219)))));
            this.ProjectButton.Size = new System.Drawing.Size(97, 32);
            this.ProjectButton.TabIndex = 2;
            this.ProjectButton.Text = "Project";
            // 
            // GeneralButton
            // 
            this.GeneralButton.BackColor = System.Drawing.Color.Transparent;
            this.GeneralButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(151)))), ((int)(((byte)(229)))));
            this.GeneralButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GeneralButton.DisabledColor = System.Drawing.Color.LightGray;
            this.GeneralButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.GeneralButton.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(141)))), ((int)(((byte)(219)))));
            this.GeneralButton.Location = new System.Drawing.Point(12, 23);
            this.GeneralButton.Name = "GeneralButton";
            this.GeneralButton.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(141)))), ((int)(((byte)(219)))));
            this.GeneralButton.Size = new System.Drawing.Size(97, 32);
            this.GeneralButton.TabIndex = 1;
            this.GeneralButton.Text = "General";
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.ForeColor = System.Drawing.Color.White;
            this.Title.Location = new System.Drawing.Point(329, 0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(105, 29);
            this.Title.TabIndex = 0;
            this.Title.Text = "Settings";
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 774);
            this.Controls.Add(this.mainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(716, 813);
            this.Name = "SettingsWindow";
            this.ShowInTaskbar = false;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.mainPanel.ResumeLayout(false);
            this.titleBackPanel.ResumeLayout(false);
            this.titleBackPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel titleBackPanel;
        private System.Windows.Forms.Label Title;
        private Client.Components.XButton ProjectButton;
        private Client.Components.XButton GeneralButton;
        private Client.Components.XButton ThemeButton;
        private Client.Components.XButton InfoButton;
        private Client.Components.XButton CancelButton;
        private Client.Components.XButton DoneButton;
        private Client.Components.XButton AcceptButton;
    }
}