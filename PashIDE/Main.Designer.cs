namespace PashIDE
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.Code = new FastColoredTextBoxNS.FastColoredTextBox();
            this.bar1 = new PashIDE.Components.Bar();
            this.Explorer = new PashIDE.Components.ProjectExplorer();
            ((System.ComponentModel.ISupportInitialize)(this.Code)).BeginInit();
            this.SuspendLayout();
            // 
            // Code
            // 
            this.Code.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Code.AutoCompleteBrackets = true;
            this.Code.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.Code.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>.+)\r\n";
            this.Code.AutoScrollMinSize = new System.Drawing.Size(31, 22);
            this.Code.BackBrush = null;
            this.Code.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(43)))));
            this.Code.BookmarkColor = System.Drawing.Color.Orange;
            this.Code.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.Code.CaretBlinking = false;
            this.Code.CaretColor = System.Drawing.Color.WhiteSmoke;
            this.Code.CharHeight = 22;
            this.Code.CharWidth = 10;
            this.Code.CommentPrefix = "#";
            this.Code.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Code.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.Code.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.Code.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Code.IndentBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(60)))), ((int)(((byte)(64)))));
            this.Code.IsReplaceMode = false;
            this.Code.Language = FastColoredTextBoxNS.Language.PASM;
            this.Code.LeftBracket = '(';
            this.Code.LineNumberColor = System.Drawing.Color.Silver;
            this.Code.LineNumberStartValue = ((uint)(0u));
            this.Code.Location = new System.Drawing.Point(0, 50);
            this.Code.Name = "Code";
            this.Code.Paddings = new System.Windows.Forms.Padding(0);
            this.Code.RightBracket = ')';
            this.Code.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.Code.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("Code.ServiceColors")));
            this.Code.ServiceLinesColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.Code.Size = new System.Drawing.Size(808, 574);
            this.Code.TabIndex = 0;
            this.Code.Zoom = 100;
            // 
            // bar1
            // 
            this.bar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(1047, 50);
            this.bar1.TabIndex = 3;
            // 
            // Explorer
            // 
            this.Explorer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Explorer.Location = new System.Drawing.Point(806, 50);
            this.Explorer.Name = "Explorer";
            this.Explorer.Size = new System.Drawing.Size(241, 574);
            this.Explorer.TabIndex = 2;
            // 
            // Main
            // 
            this.ClientSize = new System.Drawing.Size(1045, 621);
            this.Controls.Add(this.bar1);
            this.Controls.Add(this.Explorer);
            this.Controls.Add(this.Code);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1000, 660);
            this.Name = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Code)).EndInit();
            this.ResumeLayout(false);

        }

        

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox1;
        public Components.ProjectExplorer Explorer;
        private Components.Bar bar1;
        public FastColoredTextBoxNS.FastColoredTextBox Code;
    }
}