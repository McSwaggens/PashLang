namespace PashIDE
{
    partial class CodeFileAddWindow
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
            this.Title = new System.Windows.Forms.Label();
            this.R_PASM = new System.Windows.Forms.RadioButton();
            this.R_CROC = new System.Windows.Forms.RadioButton();
            this.Cancel = new Client.Components.XButton();
            this.Add = new Client.Components.XButton();
            this.NameTB = new Client.Components.TextEntry();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.ForeColor = System.Drawing.Color.Silver;
            this.Title.Location = new System.Drawing.Point(98, 9);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(223, 25);
            this.Title.TabIndex = 3;
            this.Title.Text = "Create a new CodeFile";
            // 
            // R_PASM
            // 
            this.R_PASM.AutoSize = true;
            this.R_PASM.Checked = true;
            this.R_PASM.Location = new System.Drawing.Point(12, 88);
            this.R_PASM.Name = "R_PASM";
            this.R_PASM.Size = new System.Drawing.Size(73, 17);
            this.R_PASM.TabIndex = 4;
            this.R_PASM.TabStop = true;
            this.R_PASM.Text = ".p (PASM)";
            this.R_PASM.UseVisualStyleBackColor = true;
            this.R_PASM.CheckedChanged += new System.EventHandler(this.R_PASM_CheckedChanged);
            // 
            // R_CROC
            // 
            this.R_CROC.AutoSize = true;
            this.R_CROC.Location = new System.Drawing.Point(135, 88);
            this.R_CROC.Name = "R_CROC";
            this.R_CROC.Size = new System.Drawing.Size(73, 17);
            this.R_CROC.TabIndex = 5;
            this.R_CROC.TabStop = true;
            this.R_CROC.Text = ".c (CROC)";
            this.R_CROC.UseVisualStyleBackColor = true;
            this.R_CROC.CheckedChanged += new System.EventHandler(this.R_CROC_CheckedChanged);
            // 
            // Cancel
            // 
            this.Cancel.BackColor = System.Drawing.Color.Transparent;
            this.Cancel.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Cancel.DisabledColor = System.Drawing.Color.Silver;
            this.Cancel.HoverColor = System.Drawing.Color.Gray;
            this.Cancel.Location = new System.Drawing.Point(296, 125);
            this.Cancel.Name = "Cancel";
            this.Cancel.PressedColor = System.Drawing.Color.Silver;
            this.Cancel.Size = new System.Drawing.Size(125, 28);
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = "Cancel";
            // 
            // Add
            // 
            this.Add.BackColor = System.Drawing.Color.Transparent;
            this.Add.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Add.DisabledColor = System.Drawing.Color.Silver;
            this.Add.Enabled = false;
            this.Add.HoverColor = System.Drawing.Color.Gray;
            this.Add.Location = new System.Drawing.Point(12, 125);
            this.Add.Name = "Add";
            this.Add.PressedColor = System.Drawing.Color.Silver;
            this.Add.Size = new System.Drawing.Size(125, 28);
            this.Add.TabIndex = 1;
            this.Add.Text = "Create";
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // NameTB
            // 
            this.NameTB.BackColor = System.Drawing.Color.White;
            this.NameTB.BackGroundColor = System.Drawing.Color.Gainsboro;
            this.NameTB.DefaultText = "Eneter a name here";
            this.NameTB.DefaultTextColor = System.Drawing.Color.DimGray;
            this.NameTB.Font = new System.Drawing.Font("Tahoma", 11F);
            this.NameTB.Image = null;
            this.NameTB.Location = new System.Drawing.Point(12, 41);
            this.NameTB.MaxLength = 32767;
            this.NameTB.Multiline = false;
            this.NameTB.Name = "NameTB";
            this.NameTB.OnlyAllowNumbers = false;
            this.NameTB.Opacity = 1F;
            this.NameTB.ReadOnly = false;
            this.NameTB.Size = new System.Drawing.Size(409, 41);
            this.NameTB.TabIndex = 0;
            this.NameTB.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.NameTB.UseSystemPasswordChar = false;
            // 
            // CodeFileAddWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(433, 168);
            this.Controls.Add(this.R_CROC);
            this.Controls.Add(this.R_PASM);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.NameTB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CodeFileAddWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CodeFileAddWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Client.Components.TextEntry NameTB;
        private Client.Components.XButton Add;
        private Client.Components.XButton Cancel;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.RadioButton R_PASM;
        private System.Windows.Forms.RadioButton R_CROC;
    }
}