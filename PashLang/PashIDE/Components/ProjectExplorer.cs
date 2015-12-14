using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PashIDE.Components
{
    public class ProjectExplorer : Panel
    {
        public Main mninst;
        

        public string WorkingDirectory;

        public ProjectExplorer()
        {
            DoubleBuffered = true;
        }

        public List<CodeFile> CodeFiles = new List<CodeFile>();
        public List<Rep> Reps = new List<Rep>();

        public void ScanRoot()
        {
            foreach (string p in Directory.GetFiles(WorkingDirectory))
            {
                if (DoesContainCodeFile(Path.GetFileName(p))) continue;
                CodeFile cf = new CodeFile(p);
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
