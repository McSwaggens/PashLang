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
    public class TextBallet : Control
    {
        public string toText;
        public string CurrentText;
        private bool fadingIn = true;
        public int transparency = 0;
        private bool visible = false;
        private Thread thread = new Thread(() => { });

        public TextBallet()
        {
            DoubleBuffered = true;
        }

        public TextBallet(string text, int fadeTime) : this()
        {
            toText = text;
        }

        public void Push(string text, int fadeOutTime = 1000, int fadeInTime = 1000)
        {
            thread.Abort();
            thread = new Thread(() =>
            {
                toText = text;
                if (visible)
                    FadeOut(fadeOutTime);
                CurrentText = toText;
                FadeIn(fadeInTime);
            });
            thread.Start();
        }

        private void FadeIn(int fadeTime)
        {
            for (int i = 0; i < fadeTime; i += 5)
            {
                int percentage = (int)((((double)i) / fadeTime) * 100);
                transparency = percentage;
                InvokeRefresh();
                Thread.Sleep(5);
            }
            visible = true;
        }
        private void FadeOut(int fadeTime)
        {
            for (int i = fadeTime; i > 0; i -= 5)
            {
                int percentage = (int)((((double)i) / fadeTime) * 100);
                transparency = percentage;
                InvokeRefresh();
                Thread.Sleep(5);
            }
            visible = false;
        }

        public void InvokeRefresh()
        {
            try
            {
                Invoke(new MethodInvoker(delegate { Refresh(); }));
            }
            catch (InvalidOperationException e)
            {
                thread.Abort();
            }
        }

        public Font font = new Font("Calibri Regular", 20);
        protected override void OnPaint(PaintEventArgs e)
        {
            Pen pen = new Pen(Mix(Color.FromArgb(8, 0, 0), Color.White, transparency));
            Graphics g = e.Graphics;
            SizeF size = g.MeasureString(CurrentText, font);
            g.DrawString(CurrentText, font, pen.Brush, 0, (Height / 2) - (size.Height / 2));
        }

        private static Color Mix(Color from, Color to, int percent)
        {
            float perc = percent / 100.0f;
            float amountFrom = 1.0f - perc;

            return Color.FromArgb(
            (int)(from.A * amountFrom + to.A * perc),
            (int)(from.R * amountFrom + to.R * perc),
            (int)(from.G * amountFrom + to.G * perc),
            (int)(from.B * amountFrom + to.B * perc));
        }
    }
}
