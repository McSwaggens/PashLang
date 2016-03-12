using PashIDE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Components
{
    class ToggleSwitch : Control, OpaticControl
    {
        public ToggleSwitch()
        {
            DoubleBuffered = true;
            Refresh();
        }

        #region Method Decloration

        public Color BackColor = Color.Transparent;

        private Color bodyColor = DefaultBackColor;

        public Color BodyColor
        {
            get { return bodyColor; }
            set { bodyColor = value; }
        }

        private bool smoothColorTransition = true;

        public bool UseSmoothColorTransition
        {
            get { return smoothColorTransition; }
            set { smoothColorTransition = value; }
        }

        private Color switchColor = DefaultBackColor;

        public Color SwitchColor
        {
            get { return switchColor; }
            set { switchColor = value; }
        }

        private bool showText;

        public bool ShowText
        {
            get { return showText; }
            set { showText = value; }
        }

        private bool enableColorSwitch = false;

        public bool EnableColorSwitch
        {
            get { return enableColorSwitch; }
            set { enableColorSwitch = value; }
        }

        private Color disabledColor;

        public Color DisabledColor
        {
            get { return disabledColor; }
            set { disabledColor = value; }
        }

        private Color enabledColor;

        public Color EnabledColor
        {
            get { return enabledColor; }
            set { enabledColor = value; }
        }

        private float Opacity = 1;

        public float getOpacity()
        {
            return Opacity;
        }

        public void setOpacity(float Opac)
        {
            Opacity = Opac;
            Refresh();
        }

        private bool toggled = false;

        public bool Toggled
        {
            get { return toggled; }
            set { toggled = value; }
        }

        #endregion

        protected override void OnMouseClick(MouseEventArgs e)
        {
            MouseClick();
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            MouseClick();
        }

        public void MouseClick()
        {
            toggled = !toggled;
            if (!isProcessingAnimation)
            {
                AnimationThread = new Thread(ThreadEntry);
                AnimationThread.Start();
            }
        }

        private int TogglePosition = 0;
        private bool isProcessingAnimation = false;
        private Thread AnimationThread;

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(BodyColor);

            //Draw Body
            Util.DrawRoundedRectangle(g, pen.Color, new Rectangle(0, 0, Width, Height), 4, RoundedCorners.All);

            //Draw Toggle Pin

            Color useColor = DefaultBackColor;

            if (toggled)
                useColor = disabledColor;
            else useColor = enabledColor;

            if (!smoothColorTransition) pen.Color = useColor;
            if (enableColorSwitch)
                pen.Color = Util.Mix(disabledColor, enabledColor, TogglePosition  );
            
            int P_WID = (Width - 10);

            int pos = 5 + ((P_WID - 25) * TogglePosition) / (100);

            Util.DrawRoundedRectangle(g, pen.Color, new Rectangle(pos, 3, 24, Height - 6), 3, RoundedCorners.All);
        }

        private void ThreadEntry()
        {
            isProcessingAnimation = true;
            while (true)
            {

                bool dir = toggled;

                if ((dir && TogglePosition >= 100) || (!dir && TogglePosition <= 0)) break; //Break out of thread if the operation is finished.

                if (dir) TogglePosition += 10; else TogglePosition -= 10;

                if (dir && TogglePosition > 100) TogglePosition = 100; else if (!dir && TogglePosition < 0) TogglePosition = 0;

                try
                {
                    Invoke(new MethodInvoker(delegate
                    {
                        Refresh();
                    }));
                }
                catch (Exception e) { }


                Thread.Sleep(20);


            }
            isProcessingAnimation = false;
        }
    }
}
