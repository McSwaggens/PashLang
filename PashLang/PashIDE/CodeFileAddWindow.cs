using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PashIDE
{
    public partial class CodeFileAddWindow : Form
    {
        public char CodeFileType = 'p';
        public char[] AllowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        public CodeFileAddWindow()
        {
            InitializeComponent();
            NameTB.TextChanged += NameTB_TextChanged;
        }

        private void NameTB_TextChanged(object sender, EventArgs e)
        {
            if (NameTB.Text.Length == 0 || NameTB.Text.ToCharArray().Any(c => !char.IsLetter(c)))
            {
                Add.Enabled = false;
                return;
            }
            Add.Enabled = true;
        }
        private void Add_Click(object sender, EventArgs e)
        {
            File.Create(Main.inst.project.WorkingDirectory + "/" + NameTB.Text + "." + CodeFileType).Close();
            Close();
        }

        private void R_PASM_CheckedChanged(object sender, EventArgs e)
        {
            CodeFileType = 'p';
        }

        private void R_CROC_CheckedChanged(object sender, EventArgs e)
        {
            CodeFileType = 'c';
        }

        private void CodeFileAddWindow_Load(object sender, EventArgs e)
        {
            Add.Enabled = false;
        }
    }
}
