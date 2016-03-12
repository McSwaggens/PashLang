namespace PashIDE
{
    partial class CreateProjectWindow
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
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_ProjectName = new Client.Components.TextEntry();
            this.CreateButton = new Client.Components.XButton();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.SystemColors.Control;
            this.checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Out",
            "IN"});
            this.checkedListBox1.Location = new System.Drawing.Point(12, 124);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(406, 180);
            this.checkedListBox1.TabIndex = 2;
            this.checkedListBox1.UseTabStops = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Corbel", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(70)))), ((int)(((byte)(65)))));
            this.label1.Location = new System.Drawing.Point(7, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Standard Libraries";
            // 
            // TB_ProjectName
            // 
            this.TB_ProjectName.BackColor = System.Drawing.Color.Transparent;
            this.TB_ProjectName.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TB_ProjectName.DefaultText = "Project Name";
            this.TB_ProjectName.DefaultTextColor = System.Drawing.Color.Gray;
            this.TB_ProjectName.Font = new System.Drawing.Font("Tahoma", 11F);
            this.TB_ProjectName.Image = null;
            this.TB_ProjectName.Location = new System.Drawing.Point(12, 12);
            this.TB_ProjectName.MaxLength = 32767;
            this.TB_ProjectName.Multiline = false;
            this.TB_ProjectName.Name = "TB_ProjectName";
            this.TB_ProjectName.OnlyAllowNumbers = false;
            this.TB_ProjectName.Opacity = 1F;
            this.TB_ProjectName.ReadOnly = false;
            this.TB_ProjectName.Size = new System.Drawing.Size(218, 41);
            this.TB_ProjectName.TabIndex = 1;
            this.TB_ProjectName.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.TB_ProjectName.UseSystemPasswordChar = false;
            this.TB_ProjectName.TextChanged += new System.EventHandler(this.TB_ProjectName_TextChanged);
            this.TB_ProjectName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_ProjectName_KeyPress);
            // 
            // CreateButton
            // 
            this.CreateButton.BackColor = System.Drawing.Color.Transparent;
            this.CreateButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CreateButton.DisabledColor = System.Drawing.Color.WhiteSmoke;
            this.CreateButton.Enabled = false;
            this.CreateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateButton.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.CreateButton.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.CreateButton.Location = new System.Drawing.Point(277, 326);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.CreateButton.Size = new System.Drawing.Size(141, 46);
            this.CreateButton.TabIndex = 0;
            this.CreateButton.Text = "Create";
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // CreateProjectWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 384);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.TB_ProjectName);
            this.Controls.Add(this.CreateButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateProjectWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Project";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Client.Components.XButton CreateButton;
        private Client.Components.TextEntry TB_ProjectName;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label label1;
    }
}