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
            this.titleBackPanel = new System.Windows.Forms.Panel();
            this.Title = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.line3 = new PashIDE.Components.Line();
            this.xButton3 = new Client.Components.XButton();
            this.line2 = new PashIDE.Components.Line();
            this.xButton2 = new Client.Components.XButton();
            this.line1 = new PashIDE.Components.Line();
            this.xButton1 = new Client.Components.XButton();
            this.CancelButton = new Client.Components.XButton();
            this.DoneButton = new Client.Components.XButton();
            this.AcceptButton = new Client.Components.XButton();
            this.ThemeButton = new Client.Components.XButton();
            this.InfoButton = new Client.Components.XButton();
            this.ProjectButton = new Client.Components.XButton();
            this.GeneralButton = new Client.Components.XButton();
            this.mainPanel.SuspendLayout();
            this.titleBackPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.BackColor = System.Drawing.Color.White;
            this.mainPanel.Controls.Add(this.panel1);
            this.mainPanel.Controls.Add(this.CancelButton);
            this.mainPanel.Controls.Add(this.DoneButton);
            this.mainPanel.Controls.Add(this.AcceptButton);
            this.mainPanel.Controls.Add(this.titleBackPanel);
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(700, 774);
            this.mainPanel.TabIndex = 0;
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
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.line3);
            this.panel1.Controls.Add(this.xButton3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.line2);
            this.panel1.Controls.Add(this.xButton2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.line1);
            this.panel1.Controls.Add(this.xButton1);
            this.panel1.Location = new System.Drawing.Point(12, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(676, 660);
            this.panel1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Corbel", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(100)))));
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "Line Number Visibility";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Corbel", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(100)))));
            this.label2.Location = new System.Drawing.Point(9, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(259, 28);
            this.label2.TabIndex = 5;
            this.label2.Text = "Show Warnings in Console";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Corbel", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(100)))));
            this.label3.Location = new System.Drawing.Point(9, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 28);
            this.label3.TabIndex = 8;
            this.label3.Text = "TEST";
            // 
            // line3
            // 
            this.line3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.line3.Location = new System.Drawing.Point(6, 168);
            this.line3.Name = "line3";
            this.line3.Size = new System.Drawing.Size(664, 10);
            this.line3.TabIndex = 7;
            this.line3.Text = "line3";
            // 
            // xButton3
            // 
            this.xButton3.BackColor = System.Drawing.Color.Transparent;
            this.xButton3.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(151)))), ((int)(((byte)(229)))));
            this.xButton3.DisabledColor = System.Drawing.Color.LightGray;
            this.xButton3.Font = new System.Drawing.Font("Segoe UI", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xButton3.ForeColor = System.Drawing.Color.White;
            this.xButton3.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(131)))), ((int)(((byte)(199)))));
            this.xButton3.Location = new System.Drawing.Point(529, 117);
            this.xButton3.Name = "xButton3";
            this.xButton3.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(151)))), ((int)(((byte)(229)))));
            this.xButton3.Size = new System.Drawing.Size(138, 45);
            this.xButton3.TabIndex = 6;
            this.xButton3.Text = "Disable";
            this.xButton3.Click += new System.EventHandler(this.xButton3_Click);
            // 
            // line2
            // 
            this.line2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.line2.Location = new System.Drawing.Point(6, 113);
            this.line2.Name = "line2";
            this.line2.Size = new System.Drawing.Size(664, 10);
            this.line2.TabIndex = 4;
            this.line2.Text = "line2";
            // 
            // xButton2
            // 
            this.xButton2.BackColor = System.Drawing.Color.Transparent;
            this.xButton2.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(151)))), ((int)(((byte)(229)))));
            this.xButton2.DisabledColor = System.Drawing.Color.LightGray;
            this.xButton2.Font = new System.Drawing.Font("Segoe UI", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xButton2.ForeColor = System.Drawing.Color.White;
            this.xButton2.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(131)))), ((int)(((byte)(199)))));
            this.xButton2.Location = new System.Drawing.Point(529, 62);
            this.xButton2.Name = "xButton2";
            this.xButton2.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(151)))), ((int)(((byte)(229)))));
            this.xButton2.Size = new System.Drawing.Size(138, 45);
            this.xButton2.TabIndex = 3;
            this.xButton2.Text = "Enable";
            this.xButton2.Click += new System.EventHandler(this.xButton2_Click);
            // 
            // line1
            // 
            this.line1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.line1.Location = new System.Drawing.Point(6, 58);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(664, 10);
            this.line1.TabIndex = 1;
            this.line1.Text = "line1";
            // 
            // xButton1
            // 
            this.xButton1.BackColor = System.Drawing.Color.Transparent;
            this.xButton1.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(151)))), ((int)(((byte)(229)))));
            this.xButton1.DisabledColor = System.Drawing.Color.LightGray;
            this.xButton1.Font = new System.Drawing.Font("Segoe UI", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xButton1.ForeColor = System.Drawing.Color.White;
            this.xButton1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(131)))), ((int)(((byte)(199)))));
            this.xButton1.Location = new System.Drawing.Point(529, 7);
            this.xButton1.Name = "xButton1";
            this.xButton1.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(151)))), ((int)(((byte)(229)))));
            this.xButton1.Size = new System.Drawing.Size(138, 45);
            this.xButton1.TabIndex = 0;
            this.xButton1.Text = "Disable";
            this.xButton1.Click += new System.EventHandler(this.xButton1_Click);
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
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
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
            this.GeneralButton.Click += new System.EventHandler(this.GeneralButton_Click);
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
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.Panel panel1;
        private Client.Components.XButton xButton1;
        private Components.Line line1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private Components.Line line3;
        private Client.Components.XButton xButton3;
        private System.Windows.Forms.Label label2;
        private Components.Line line2;
        private Client.Components.XButton xButton2;
    }
}