using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PashIDE.Logger;

namespace PashIDE.Components
{
    public class ProjectExplorer : Panel
    {
        public Main mninst;
        

        public string WorkingDirectory;

        public ProjectExplorer()
        {
            DoubleBuffered = true;
            RemoveButton = new TinyButton(Width, Height,"Remove");
            AddButton = new TinyButton(Width, Height, "Add");
            RenameButton = new TinyButton(Width, Height, "Rename");
            Controls.Add(RemoveButton);
            RemoveButton.Click += RemoveButton_Click;
            Controls.Add(AddButton);
            AddButton.Click += AddButton_Click;
            Controls.Add(RenameButton);
            RenameButton.Click += RenameButton_Click;

            this.KeyPress += ProjectExplorer_KeyPress;
        }

        private void ProjectExplorer_KeyPress(object sender, KeyPressEventArgs e)
        {
            LogInfo(e.KeyChar.ToString());
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            string message = $"Are you sure that you want to delete the CodeFile \"{mninst.CurrentOpen.HardName}\"?";
            string caption = "Remove?";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                mninst.CurrentOpen.Delete();
            }
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            CodeFileAddWindow window = new CodeFileAddWindow();
            window.ShowDialog();
        }

        private void RenameButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            AddButton.Width = (Width) - 8;
            AddButton.Height = 25;
            AddButton.Location = new Point(3, Height - ((AddButton.Height + 5) * 2));

            RemoveButton.Width = (Width / 2) - 8;
            RemoveButton.Height = 25;
            RemoveButton.Location = new Point(3, Height - (RemoveButton.Height + 5));

            RenameButton.Width = (Width / 2);
            RenameButton.Height = 25;
            RenameButton.Location = new Point(RenameButton.Width - 3, Height - (RemoveButton.Height + 5));
        }

        public List<CodeFile> CodeFiles = new List<CodeFile>();
        public List<Rep> Reps = new List<Rep>();
        public TinyButton RemoveButton;
        public TinyButton AddButton;
        public TinyButton RenameButton;

        public void ScanRoot()
        {
            foreach (CodeFile cf in from p in Directory.GetFiles(WorkingDirectory) where !DoesContainCodeFile(Path.GetFileName(p)) select new CodeFile(p))
            {
                CodeFiles.Add(cf);
                Rep r = new Rep(cf);
                Reps.Add(r);
                r.Location = new Point(0, 10 + (CodeFiles.Count * 22));
                r.Width = Width;
                r.Height = 20;
                r.peinst = this;
                Controls.Add(r);
            }

            bool requiresRefresh = false;

            List<CodeFile> toremove = new List<CodeFile>();

            foreach (CodeFile cf in CodeFiles)
            {
                if (!File.Exists(Main.inst.project.WorkingDirectory + "/" + cf.HardName))
                {
                    requiresRefresh = true;
                    foreach (Rep r in Reps)
                        if (r.codeFile == cf)
                        {
                            Reps.Remove(r);
                            toremove.Add(r.codeFile);
                            Controls.Remove(r);
                            break;
                        }
                }
            }

            foreach (CodeFile cf in toremove) CodeFiles.Remove(cf);


            int at = 1;
            foreach (Rep r in Reps)
            {
                r.Location = new Point(0, 10 + (at * 22));
                at++;
            }
        }

        public bool DoesContainCodeFile(string file)
        {
            foreach (CodeFile cf in CodeFiles) if (cf.HardName == file) return true;
            return false;
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Pen pen = new Pen(Color.FromArgb(50, 50, 50));

            //Draw Background
            g.FillRectangle(pen.Brush, 0, 0, Width, Height);
            pen.Color = Color.FromArgb(75, 75, 75);
            g.FillRectangle(pen.Brush, 0, 0, Width, 20);
            pen.Color = Color.FromArgb(210, 210, 210);
            g.DrawString("Project Files", new Font("Calibri Regular", 11.4f, FontStyle.Regular), pen.Brush, (Width / 2) - (g.MeasureString("Project Files", new Font("Calibri Regular", 11.4f, FontStyle.Regular)).Width / 2), 0);
        }
    }

    public class TinyButton : Control
    {
        public TinyButton(int width, int height, string text)
        {
            Width = width;
            Height = height;
            Text = text;
            DoubleBuffered = true;
        }
        public bool isHovered;

        protected override void OnMouseEnter(EventArgs e)
        {
            Cursor = Cursors.Hand;
            isHovered = true;
            Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            isHovered = false;
            Refresh();
        }
        Font font = new Font("Consolas", 12, FontStyle.Regular);
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Pen pen = new Pen(Color.FromArgb(89, 89, 89));
            if (isHovered) pen.Color = Color.FromArgb(80, 80, 80);
            g.FillRectangle(pen.Brush, 0, 0, Width, Height);
            pen.Color = Color.FromArgb(180, 180, 180);
            SizeF measure = g.MeasureString(Text, font);
            g.DrawString(Text, font, pen.Brush, (Width / 2) - (measure.Width / 2), (Height / 2) - (measure.Height / 2));
            
        }
    }

    public class Rep : Control
    {
        public ProjectExplorer peinst;
        public CodeFile codeFile;
        public Rep(CodeFile cf)
        {
            codeFile = cf;
        }

        public bool Hovered = false;

        protected override void OnMouseEnter(EventArgs e)
        {
            Cursor = Cursors.Hand;
            Hovered = true;
            Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            Hovered = false;
            Refresh();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            peinst.mninst.OpenCodeFile(codeFile);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Pen pen = new Pen(Color.FromArgb(89, 89, 89));
            if (Hovered) pen.Color = Color.FromArgb(80, 80, 80);
            g.FillRectangle(pen.Brush, 0, 0, Width, Height);
            pen.Color = Color.FromArgb(180, 180 , 180);
            g.DrawString(codeFile.HardName, new Font("Consolas", 9f, FontStyle.Regular), pen.Brush, 3, 3);
            pen.Color = codeFile.Saved ? Color.FromArgb(69, 191, 85) : Color.FromArgb(255, 109, 15);
            g.DrawLine(pen, 0, 0, 0, Height);
        }
    }
}
