using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PashIDE.Components
{
    public class Region : Panel
    {
        private string title = "TITLE NOT SET";
        public string Title { get { return title; } set { title = value; titleLabel.Text = value; } }
        public Panel titlePanel = new Panel();
        public Label titleLabel = new Label();
        public Color titleBackgroundColor = Util.Mix(Color.FromArgb(14, 161, 239), Color.White, 50);
        public Region()
        {
            AutoScroll = true;
            Controls.Add(titlePanel);
            titleLabel.ForeColor = Color.White;
            titleLabel.Text = title;
            titleLabel.Font = new Font("Corbel Regular", 15);
            titlePanel.Controls.Add(titleLabel);
            titlePanel.Height = 60;
            titlePanel.BackColor = titleBackgroundColor;
        }

        protected override void OnResize(EventArgs eventargs)
        {
            titlePanel.Width = Width;
            titleLabel.Location = new Point((Width / 2) - (titleLabel.Width / 2), 5);
        }
    }
}
