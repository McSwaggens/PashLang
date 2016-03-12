using PashIDE.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PashIDE.Components
{
    public class Bar : Panel
    {

        public StartButton startButton = new StartButton();
        public P_Button settingsButton = new P_Button(Resources.Settings_icon);
        public Bar()
        {
            startButton.Size = new Size(80, 50);
            Controls.Add(startButton);

            settingsButton.Size = new Size(50, 50);
            Controls.Add(settingsButton);
            settingsButton.BackColor = Color.FromArgb(57, 60, 64);
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            startButton.Location = new Point(((Width - 80) / (4)) * 2, 0);
            settingsButton.Location = new Point(Width - (settingsButton.Width + 10), 0);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Refresh();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new Pen(Color.FromArgb(57, 60, 64)).Brush, 0, 0, Width, Height);
        }

        public class P_Button : Control
        {
            public bool drawBackground = false;
            private Image image;
            public P_Button(Image image)
            {
                this.image = image;
                DoubleBuffered = true;
                Cursor = Cursors.Hand;
            }

            public bool Hovered = false;

            protected override void OnMouseEnter(EventArgs e)
            {
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
                if (Main.inst.settingsWindow == null)
                    Main.inst.settingsWindow = new SettingsWindow();
                Main.inst.settingsWindow.ShowDialog();
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                Pen pen = new Pen(Color.FromArgb(73, 140, 160));
                if (drawBackground)
                {
                    if (Hovered) pen.Color = Color.FromArgb(83, 150, 170);
                    g.FillRectangle(pen.Brush, 0, 0, Width, Height);
                }
                Rectangle r = new Rectangle(10, 10, Width-10, Height-10);
                g.DrawImage(image, r);
            }
        }

        public class StartButton : Control
        {
            public StartButton()
            {
                DoubleBuffered = true;
                Cursor = Cursors.Hand;
            }

            public List<Color> RainbowColors = new List<Color>()
            {
                Color.FromArgb(63, 130, 150),
                Color.FromArgb(53, 120, 140),
                Color.FromArgb(43, 110, 130),
            };

            public bool isLooping = false;

            Thread thread;

            private void ColorLoop()
            {
                if (thread != null) thread = null;
                isLooping = true;
                thread = new Thread(() =>
                {
                    Color CurrentColor = Color.FromArgb(73, 140, 160);
                    Random r = new Random();
                    while (true)
                    {
                        Color to = RainbowColors[r.Next(0, RainbowColors.Count)];
                        for (int i = 0; i < 100; i += 5)
                        {
                            rbsetColor = Util.Mix(CurrentColor, to, i);
                            Invoke(new MethodInvoker(delegate { Refresh(); }));
                            Thread.Sleep(15);
                        }
                        CurrentColor = to;
                    }
                });
                thread.Start();
            }

            private Color rbsetColor;

            public bool Hovered = false;

            protected override void OnMouseEnter(EventArgs e)
            {
                Hovered = true;
                ColorLoop();
                Refresh();
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                thread.Abort();
                isLooping = false;
                Hovered = false;
                Refresh();
            }

            protected override void OnMouseClick(MouseEventArgs e)
            {
                if (!Main.inst.isRunningCode)
                    Main.inst.StartProject();
                else Main.inst.StopInstance();
                Refresh();
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                if (isLooping)
                {
                    Pen pen = new Pen(rbsetColor);
                    g.FillRectangle(pen.Brush, 0, 0, Width, Height);
                }
                else
                {
                    Pen pen = new Pen(Color.FromArgb(73, 140, 160));
                    if (Hovered) pen.Color = Color.FromArgb(83, 150, 170);
                    g.FillRectangle(pen.Brush, 0, 0, Width, Height);
                }
                Rectangle r = new Rectangle(15, 3, Width - 30, Height - 10);
                if (Main.inst != null)
                    if (!Main.inst.isRunningCode)
                        g.DrawImage(Resources.Start, r);
                    else
                        g.DrawImage(Resources.Stop, r);
            }
        }

        public class HeaderButton : Control
        {
            public HeaderButton()
            {
            }

            public bool Hovered = false;

            protected override void OnMouseEnter(EventArgs e)
            {
                Hovered = true;
                Refresh();
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                Hovered = false;
                Refresh();
            }

            protected override void OnPaint(PaintEventArgs e)
            {

                Graphics g = e.Graphics;

                Pen pen = new Pen(Color.FromArgb(51, 52, 57));
                if (Hovered) pen.Color = Color.FromArgb(61, 62, 67);
                g.FillRectangle(pen.Brush, 0, 0, Width, Height);

                //Draw outer line
                pen.Color = Color.FromArgb(31, 33, 38);
                g.DrawRectangle(pen, 0, 0, Width, Height - 1);

            }
        }
    }
}