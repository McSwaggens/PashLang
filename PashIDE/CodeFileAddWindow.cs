using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PashIDE
{
    public partial class CodeFileAddWindow : Form
    {
        public string CodeFileType = "p";
        public char[] AllowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_".ToCharArray();
        public CodeFileAddWindow()
        {
            InitializeComponent();
            NameTB.TextChanged += NameTB_TextChanged;
        }

        private void NameTB_TextChanged(object sender, EventArgs e)
        {
            if (NameTB.Text.Length > 0 && isAllowed(NameTB.Text))
            {
                Add.Enabled = true;
                return;
            }
            Add.Enabled = false;
        }

        private bool isAllowed(string str)
        {
            foreach (char c in str.ToString())
            {
                if (!AllowedCharacters.Contains(c)) return false;
            }
            return true;
        }

        private void Add_Click(object sender, EventArgs e)
        {
            File.Create(Main.inst.project.WorkingDirectory + "/" + NameTB.Text + "." + CodeFileType).Close();
            Close();
        }

        private void R_PASM_CheckedChanged(object sender, EventArgs e)
        {
            CodeFileType = "p";
        }

        private void R_Puffin_CheckedChanged(object sender, EventArgs e)
        {
            CodeFileType = "puf";
        }

        private void CodeFileAddWindow_Load(object sender, EventArgs e)
        {
            Add.Enabled = false;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
