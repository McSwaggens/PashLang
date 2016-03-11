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
            KeyPress += ProjectExplorer_KeyPress;
        }

        private void ProjectExplorer_KeyPress(object sender, KeyPressEventArgs e)
        {
            Warning(e.KeyChar.ToString());
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStripFlat menu = new ContextMenuStripFlat();
                menu.Items.Add(new ToolStripMenuItem("Add", Properties.Resources.Add, new EventHandler(Add)));
                menu.Show(this, new Point(e.X, e.Y));
            }
        }

        private void Add(object sender, EventArgs args)
        {
            CodeFileAddWindow window = new CodeFileAddWindow();
            window.ShowDialog();
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

        private void RenameButton_Click(object sender, EventArgs e)
        {
        }

        protected override void OnSizeChanged(EventArgs e)
        {
        }

        public List<CodeFile> CodeFiles = new List<CodeFile>();
        public List<Rep> Reps = new List<Rep>();

        public void ScanRoot()
        {
            foreach (CodeFile cf in from p in Directory.GetFiles(WorkingDirectory) where !DoesContainCodeFile(Path.GetFileName(p)) select new CodeFile(p))
            {
                CodeFiles.Add(cf);
                Rep r = new Rep(cf);
                Reps.Add(r);
                r.Location = new Point(0, (CodeFiles.Count * 22));
                r.Width = Width;
                r.Height = 21;
                r.peinst = this;
                Controls.Add(r);
            }


            List<CodeFile> toremove = new List<CodeFile>();

            foreach (CodeFile cf in CodeFiles)
            {
                if (!File.Exists(Main.inst.project.WorkingDirectory + "/" + cf.HardName))
                {
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
                r.Location = new Point(0, 2+(at * 23));
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

            Pen pen = new Pen(Color.FromArgb(46, 47, 50));

            //Draw Background
            g.FillRectangle(pen.Brush, 0, 0, Width, Height);
            pen.Color = Color.FromArgb(57, 60, 64);
            g.FillRectangle(pen.Brush, 0, 0, Width, 20);
            pen.Color = Color.FromArgb(77, 80, 84);
            g.DrawLine(pen, 0, 10, 65, 10);
            g.DrawLine(pen, Width-75, 10, Width, 10);
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

    public class Rep : Panel
    {
        public ProjectExplorer peinst;
        public CodeFile codeFile;

        public Rep(CodeFile cf)
        {
            codeFile = cf;
            codeFile.rep = this;
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
            if (e.Button == MouseButtons.Left)
            peinst.mninst.OpenCodeFile(codeFile);
            else if (e.Button == MouseButtons.Right)
            {
                ContextMenuStripFlat menu = new ContextMenuStripFlat();
                menu.Items.Add(new ToolStripMenuItem("Rename", Properties.Resources.Rename, new EventHandler(Rename)));
                menu.Items.Add(new ToolStripMenuItem("Delete", Properties.Resources.Delete, new EventHandler(Delete)));
                menu.Show(this, new Point(e.X, e.Y));
            }
        }

        private void Delete(object sender, EventArgs e)
        {
            string message = $"Are you sure that you want to delete the CodeFile \"{codeFile.HardName}\"?";
            string caption = "Remove?";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                codeFile.Delete();
            }
        }

        private void Rename(object sender, EventArgs e)
        {
            CodeFileRename renameWindow = new CodeFileRename(codeFile);
            renameWindow.ShowDialog();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Pen pen = new Pen(Color.FromArgb(89, 89, 89));
            if (Hovered) pen.Color = Color.FromArgb(80, 80, 80);
            g.FillRectangle(pen.Brush, 0, 0, Width, Height);
            pen.Color = Color.FromArgb(180, 180 , 180);
            g.DrawString(codeFile.HardName, new Font("Consolas", 9f, FontStyle.Regular), pen.Brush, 4, 3);
            if (Main.inst.inCompileTime)
            {
                pen.Color = Color.FromArgb(23, 199, 0);
                if (codeFile.compileStatus != CodeFile.CompileTimeStatus.Complete) pen.Color = Color.FromArgb(255, 194, 12);
                g.FillRectangle(pen.Brush, 0, Height - 2, Width, 2);
            }
            pen.Color = codeFile.Saved ? Color.FromArgb(69, 191, 85) : Color.FromArgb(255, 109, 15);
            g.DrawLine(pen, 0, 0, 0, Height);
            g.DrawLine(pen, 1, 0, 1, Height);
        }
    }
}
