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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeFileAddWindow));
            this.Title = new System.Windows.Forms.Label();
            this.R_PASM = new System.Windows.Forms.RadioButton();
            this.R_CROC = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Cancel = new Client.Components.XButton();
            this.Add = new Client.Components.XButton();
            this.NameTB = new Client.Components.TextEntry();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.ForeColor = System.Drawing.Color.White;
            this.Title.Location = new System.Drawing.Point(122, 12);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(223, 25);
            this.Title.TabIndex = 3;
            this.Title.Text = "Create a new CodeFile";
            // 
            // R_PASM
            // 
            this.R_PASM.AutoSize = true;
            this.R_PASM.Checked = true;
            this.R_PASM.Font = new System.Drawing.Font("Open Sans", 18.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.R_PASM.Location = new System.Drawing.Point(78, 135);
            this.R_PASM.Name = "R_PASM";
            this.R_PASM.Size = new System.Drawing.Size(206, 38);
            this.R_PASM.TabIndex = 4;
            this.R_PASM.TabStop = true;
            this.R_PASM.Text = "Pash Assembly";
            this.R_PASM.UseVisualStyleBackColor = true;
            this.R_PASM.CheckedChanged += new System.EventHandler(this.R_PASM_CheckedChanged);
            // 
            // R_CROC
            // 
            this.R_CROC.AutoSize = true;
            this.R_CROC.Font = new System.Drawing.Font("Open Sans", 18.25F);
            this.R_CROC.Location = new System.Drawing.Point(78, 196);
            this.R_CROC.Name = "R_CROC";
            this.R_CROC.Size = new System.Drawing.Size(101, 38);
            this.R_CROC.TabIndex = 5;
            this.R_CROC.TabStop = true;
            this.R_CROC.Text = "Puffin";
            this.R_CROC.UseVisualStyleBackColor = true;
            this.R_CROC.CheckedChanged += new System.EventHandler(this.R_Puffin_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 120);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(60, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // Cancel
            // 
            this.Cancel.BackColor = System.Drawing.Color.Transparent;
            this.Cancel.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Cancel.DisabledColor = System.Drawing.Color.Silver;
            this.Cancel.HoverColor = System.Drawing.Color.Gray;
            this.Cancel.Location = new System.Drawing.Point(342, 257);
            this.Cancel.Name = "Cancel";
            this.Cancel.PressedColor = System.Drawing.Color.Silver;
            this.Cancel.Size = new System.Drawing.Size(125, 28);
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = "Cancel";
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Add
            // 
            this.Add.BackColor = System.Drawing.Color.Transparent;
            this.Add.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Add.DisabledColor = System.Drawing.Color.Silver;
            this.Add.Enabled = false;
            this.Add.HoverColor = System.Drawing.Color.Gray;
            this.Add.Location = new System.Drawing.Point(12, 257);
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
            this.NameTB.DefaultText = "File Name";
            this.NameTB.DefaultTextColor = System.Drawing.Color.DimGray;
            this.NameTB.Font = new System.Drawing.Font("Tahoma", 11F);
            this.NameTB.Image = null;
            this.NameTB.Location = new System.Drawing.Point(12, 73);
            this.NameTB.MaxLength = 32767;
            this.NameTB.Multiline = false;
            this.NameTB.Name = "NameTB";
            this.NameTB.OnlyAllowNumbers = false;
            this.NameTB.Opacity = 1F;
            this.NameTB.ReadOnly = false;
            this.NameTB.Size = new System.Drawing.Size(455, 41);
            this.NameTB.TabIndex = 0;
            this.NameTB.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.NameTB.UseSystemPasswordChar = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::PashIDE.Properties.Resources.Puffin;
            this.pictureBox3.Location = new System.Drawing.Point(12, 186);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(60, 60);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 8;
            this.pictureBox3.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(161)))), ((int)(((byte)(239)))));
            this.panel1.Controls.Add(this.Title);
            this.panel1.Location = new System.Drawing.Point(-4, -3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(490, 53);
            this.panel1.TabIndex = 9;
            // 
            // CodeFileAddWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(479, 297);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.R_CROC);
            this.Controls.Add(this.R_PASM);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.NameTB);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CodeFileAddWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CodeFileAddWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panel1;
    }
}