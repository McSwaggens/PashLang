using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PashIDE
{
    public partial class SettingsWindow : Form
    {
        public SettingsWindow()
        {
            InitializeComponent();
            Text = "Settings - Main";
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            mainPanel.HorizontalScroll.Enabled = true;
        }

        protected override void OnResize(EventArgs e)
        {
            mainPanel.Width = Width;
            mainPanel.Height = Height;
            titleBackPanel.Width = Width;
            Title.Location = new Point((Width / 2)-(Title.Width / 2), (titleBackPanel.Height / 2) - (Title.Height / 2));
        }
    }
}
