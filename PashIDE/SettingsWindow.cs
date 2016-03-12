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
using static PashIDE.Logger;
namespace PashIDE
{
    public partial class SettingsWindow : Form
    {
        Settings tempSettings = Clone(Main.inst.settings);
        public SettingsWindow()
        {
            InitializeComponent();
            Text = "Settings - Main";
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            mainPanel.HorizontalScroll.Enabled = true;
            ShowWarnings_Button.Text = Settings.CurrentSettings.showConsoleWarnings ? "Enable" : "Disable";
            LineVisibility_Button.Text = Settings.CurrentSettings.showLineNumber ? "Enable" : "Disable";
        }

        protected override void OnShown(EventArgs e)
        {
            tempSettings = Clone(Main.inst.settings);
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
            LineVisibility_Button.Text = Settings.CurrentSettings.showLineNumber ? "Enable" : "Disable";
            Main.inst.Code.ShowLineNumbers = !Settings.CurrentSettings.showLineNumber;
        }

        private void xButton2_Click(object sender, EventArgs e)
        {
            Settings.CurrentSettings.showConsoleWarnings = !Settings.CurrentSettings.showConsoleWarnings;
            ShowWarnings_Button.Text = Settings.CurrentSettings.showConsoleWarnings ? "Enable" : "Disable";
        }

        private void xButton3_Click(object sender, EventArgs e)
        {

        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            Main.inst.settings = tempSettings;
        }

        private static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
