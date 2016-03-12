using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PashIDE.Logger;
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
            Warning("Hello World");
            xButton2.Text = Settings.CurrentSettings.showConsoleWarnings ? "Enable" : "Disable";
            xButton1.Text = Settings.CurrentSettings.showLineNumber ? "Disable" : "Enable";
        }

        protected override void OnResize(EventArgs e)
        {
            mainPanel.Width = Width;
            mainPanel.Height = Height;
            titleBackPanel.Width = Width;
            Title.Location = new Point((Width / 2)-(Title.Width / 2), (titleBackPanel.Height / 2) - (Title.Height / 2));
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GeneralButton_Click(object sender, EventArgs e)
        {
            
        }

        private void xButton1_Click(object sender, EventArgs e)
        {
            Settings.CurrentSettings.showLineNumber = !Settings.CurrentSettings.showLineNumber;
            xButton1.Text = Settings.CurrentSettings.showLineNumber ? "Disable" : "Enable";
        }

        private void xButton2_Click(object sender, EventArgs e)
        {
            Settings.CurrentSettings.showConsoleWarnings = !Settings.CurrentSettings.showConsoleWarnings;
            xButton2.Text = Settings.CurrentSettings.showConsoleWarnings ? "Enable" : "Disable";
        }

        private void xButton3_Click(object sender, EventArgs e)
        {

        }
    }
}
