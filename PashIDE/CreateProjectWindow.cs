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
    public partial class CreateProjectWindow : Form
    {
        public CreateProjectWindow()
        {
            InitializeComponent();
        }

        private void TB_ProjectName_KeyPress(object sender, KeyPressEventArgs e)
        {
            CreateButton.Enabled = TB_ProjectName.Text.Length > 0;
        }

        private void TB_ProjectName_TextChanged(object sender, EventArgs e)
        {
            CreateButton.Enabled = TB_ProjectName.Text.Length > 0;
        }

        public Home prnt;

        private void CreateButton_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(prnt.WorkingDirectory + @"\" + TB_ProjectName.Text);
            prnt.creatednew = true;
            Close();
        }
    }
}
