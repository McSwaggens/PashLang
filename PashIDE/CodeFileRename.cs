using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PashIDE
{
    public partial class CodeFileRename : Form
    {
        public CodeFile codeFile;
        public CodeFileRename(CodeFile codeFile)
        {
            this.codeFile = codeFile;
            
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isAllowed(textBox1.Text))
                ExecuteRename();
            else MessageBox.Show("Please use alphabetical characters and _ in your names");
        }

        public char[] AllowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_".ToCharArray();

        private bool isAllowed(string str)
        {
            foreach (char c in str.ToString())
            {
                if (!AllowedCharacters.Contains(c)) return false;
            }
            return true;
        }

        void ExecuteRename()
        {
            string type = codeFile.HardName.Split('.')[1];
            File.Move(codeFile.path, codeFile.path.Replace(codeFile.HardName, "") + textBox1.Text + "." + type);
            codeFile.Name = textBox1.Text + "." + type;
            codeFile.path = codeFile.path.Replace(codeFile.HardName, "") + textBox1.Text + "." + type;
            Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && isAllowed(textBox1.Text)) ExecuteRename();
        }

        private void CodeFileRename_Load(object sender, EventArgs e)
        {
            textBox1.Text = codeFile.Name.Split('.')[0];
            textBox1.Focus();
            textBox1.SelectAll();
        }
    }
}
