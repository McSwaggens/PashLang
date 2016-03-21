namespace PashIDE
{
    partial class Home
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.L_NoProjects = new System.Windows.Forms.Label();
            this.L_Projects = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Open = new Client.Components.XButton();
            this.Create = new Client.Components.XButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.Loading_Title = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // L_NoProjects
            // 
            this.L_NoProjects.AutoSize = true;
            this.L_NoProjects.Font = new System.Drawing.Font("Corbel", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L_NoProjects.ForeColor = System.Drawing.Color.Gray;
            this.L_NoProjects.Location = new System.Drawing.Point(215, 195);
            this.L_NoProjects.Name = "L_NoProjects";
            this.L_NoProjects.Size = new System.Drawing.Size(299, 39);
            this.L_NoProjects.TabIndex = 2;
            this.L_NoProjects.Text = "You have no projects!";
            // 
            // L_Projects
            // 
            this.L_Projects.AutoSize = true;
            this.L_Projects.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L_Projects.ForeColor = System.Drawing.Color.Gray;
            this.L_Projects.Location = new System.Drawing.Point(303, 90);
            this.L_Projects.Name = "L_Projects";
            this.L_Projects.Size = new System.Drawing.Size(91, 29);
            this.L_Projects.TabIndex = 3;
            this.L_Projects.Text = "Projects";
            this.L_Projects.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(161)))), ((int)(((byte)(239)))));
            this.panel1.Controls.Add(this.Open);
            this.panel1.Controls.Add(this.Create);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(712, 87);
            this.panel1.TabIndex = 4;
            // 
            // Open
            // 
            this.Open.BackColor = System.Drawing.Color.Transparent;
            this.Open.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(152)))), ((int)(((byte)(210)))));
            this.Open.DisabledColor = System.Drawing.Color.LightGray;
            this.Open.Font = new System.Drawing.Font("Corbel", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Open.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Open.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(142)))), ((int)(((byte)(200)))));
            this.Open.Location = new System.Drawing.Point(358, 14);
            this.Open.Name = "Open";
            this.Open.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(74)))), ((int)(((byte)(124)))));
            this.Open.Size = new System.Drawing.Size(338, 51);
            this.Open.TabIndex = 1;
            this.Open.Text = "Open Existing Project";
            this.Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // Create
            // 
            this.Create.BackColor = System.Drawing.Color.Transparent;
            this.Create.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(152)))), ((int)(((byte)(210)))));
            this.Create.DisabledColor = System.Drawing.Color.LightGray;
            this.Create.Font = new System.Drawing.Font("Corbel", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Create.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Create.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(142)))), ((int)(((byte)(200)))));
            this.Create.Location = new System.Drawing.Point(14, 14);
            this.Create.Name = "Create";
            this.Create.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(74)))), ((int)(((byte)(124)))));
            this.Create.Size = new System.Drawing.Size(338, 51);
            this.Create.TabIndex = 0;
            this.Create.Text = "Create New Project";
            this.Create.Click += new System.EventHandler(this.Create_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.L_NoProjects);
            this.groupBox1.Location = new System.Drawing.Point(3, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(711, 527);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.Color.White;
            this.MainPanel.Controls.Add(this.L_Projects);
            this.MainPanel.Controls.Add(this.panel1);
            this.MainPanel.Controls.Add(this.groupBox1);
            this.MainPanel.Location = new System.Drawing.Point(-3, -1);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(713, 611);
            this.MainPanel.TabIndex = 6;
            // 
            // Loading_Title
            // 
            this.Loading_Title.AutoSize = true;
            this.Loading_Title.Font = new System.Drawing.Font("Corbel", 19.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Loading_Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Loading_Title.Location = new System.Drawing.Point(300, 46);
            this.Loading_Title.Name = "Loading_Title";
            this.Loading_Title.Size = new System.Drawing.Size(104, 32);
            this.Loading_Title.TabIndex = 8;
            this.Loading_Title.Text = "Loading";
            this.Loading_Title.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-32, 46);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 600);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(706, 609);
            this.Controls.Add(this.Loading_Title);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.Load += new System.EventHandler(this.Home_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Client.Components.XButton Create;
        private Client.Components.XButton Open;
        private System.Windows.Forms.Label L_NoProjects;
        private System.Windows.Forms.Label L_Projects;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label Loading_Title;
    }
}