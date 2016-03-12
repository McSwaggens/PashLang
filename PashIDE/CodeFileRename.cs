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
            ExecuteRename();
        }

        void ExecuteRename()
        {
            char type = codeFile.HardName.ToCharArray()[codeFile.HardName.Length - 1];
            File.Move(codeFile.path, codeFile.path.Replace(codeFile.HardName, "") + this.textBox1.Text + "." + type);
            codeFile.Name = this.textBox1.Text + "." + type;
            codeFile.path = codeFile.path.Replace(codeFile.HardName, "") + this.textBox1.Text + "." + type;
            Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ExecuteRename();
        }
    }
}
