using PashIDE;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Components {
    public enum RectAngles {
        None = 0,
        TopLeft = 1,
        TopRight = 2,
        BottomLeft = 4,
        BottomRight = 8,
        All = TopLeft|TopRight|BottomLeft|BottomRight
    }
    public class XButton : Control, OpaticControl {
        public const int WS_EX_TRANSPARENT = 0x00000020;
        public const int GWL_EXSTYLE = (-20);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong ( IntPtr hwnd , int index );

        [DllImport("user32.dll")]
        public static extern int SetWindowLong ( IntPtr hwnd , int index , int newStyle );

        public static void makeTransparent ( IntPtr hwnd ) {
            // Change the extended window style to include WS_EX_TRANSPARENT
            int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong ( hwnd , GWL_EXSTYLE , extendedStyle|WS_EX_TRANSPARENT );
        }

        public bool isPressed = false;
        public bool isHovered = false;

        private Color disabledColor = Color.LightGray;

        public Color DisabledColor {
            get { return disabledColor; }
            set { disabledColor=value; }
        }

        private Color pressedColor;
        public Color PressedColor {
            get { return pressedColor; }
            set { pressedColor=value; }
        }

        private Color hoverColor;
        public Color HoverColor {
            get { return hoverColor; }
            set { hoverColor=value; }
        }

        private Color buttonColor;
        public Color ButtonColor {
            get { return buttonColor; }
            set { buttonColor=value; }
        }

        public XButton ( ) {
            SetStyle ( ControlStyles.SupportsTransparentBackColor , true );
            BackColor=Color.Transparent;
            DoubleBuffered=true;
            OriginalLocation = this.Location;
        }

        public Point OriginalLocation;

        protected override void OnMouseEnter ( EventArgs e ) {
            isHovered=true;
            if (Opacity != 0)
            Refresh ( );
        }

        protected override void OnMouseLeave ( EventArgs e ) {
            isHovered=false;
            Refresh ( );
        }

        protected override void OnMouseDown ( MouseEventArgs e ) {
            isPressed=true;
            if (Opacity != 0)
                Refresh ( );
        }

        protected override void OnMouseUp ( MouseEventArgs e ) {
            isPressed=false;
            Refresh ( );
        }

        public Color bkclr;

        public void setOpacity ( float opac , Color bkclr ) {
            this.bkclr=bkclr;
            Opacity=opac;
            Refresh ( );
        }

        public void setOpacity ( float Opac ) {
            setOpacity ( Opac , BackColor );
        }

        public float getOpacity ( ) {
            return Opacity;
        }

        private float Opacity = 1.0f;

        protected override void OnPaint ( PaintEventArgs p ) {
            //Initialize Graphics
            Graphics g = p.Graphics;


            Pen pen = new Pen(Color.Transparent);

            g.DrawRectangle ( pen , 0 , 0 , Width , Height );

            pen.Color=ForeColor;
            

            //Paint main part of the button
            Color toDraw = Util.Mix(bkclr, ButtonColor, Opacity);
            if (!Enabled) toDraw=disabledColor;
            else if (isPressed) toDraw=pressedColor;
            else if (isHovered) toDraw=HoverColor;
            DrawRoundedRectangle ( g , toDraw , new Rectangle ( 0 , 0 , Width , Height ) , 1 , RoundedCorners.All );

            SizeF sz = p.Graphics.MeasureString(Text, Font);
            pen.Color=Util.Mix ( bkclr , ForeColor , Opacity );
            g.DrawString ( Text , Font , pen.Brush , new PointF ( ( Width/2 )-( sz.Width/2 ) , ( Height/2 )-( sz.Height/2 ) ) );
        }

        protected override void OnBackColorChanged ( EventArgs e ) {
            if (this.Parent!=null) {
                Parent.Invalidate ( this.Bounds , true );
            }
            base.OnBackColorChanged ( e );
        }

        protected override void OnParentBackColorChanged ( EventArgs e ) {
            this.Invalidate ( );
            base.OnParentBackColorChanged ( e );
        }


        public static void DrawRoundedRectangle ( Graphics g , Color color , Rectangle rec , int radius , RoundedCorners corners ) {
            using (var b = new SolidBrush ( color )) {
                int x = rec.X;
                int y = rec.Y;
                int diameter = radius * 2;
                var horiz = new Rectangle(x, y + radius, rec.Width, rec.Height - diameter);
                var vert = new Rectangle(x + radius, y, rec.Width - diameter, rec.Height);

                g.FillRectangle ( b , horiz );
                g.FillRectangle ( b , vert );

                if (( corners&RoundedCorners.TopLeft )==RoundedCorners.TopLeft)
                    g.FillEllipse ( b , x , y , diameter , diameter );
                else
                    g.FillRectangle ( b , x , y , diameter , diameter );

                if (( corners&RoundedCorners.TopRight )==RoundedCorners.TopRight)
                    g.FillEllipse ( b , x+rec.Width-( diameter+1 ) , y , diameter , diameter );
                else
                    g.FillRectangle ( b , x+rec.Width-( diameter+1 ) , y , diameter , diameter );

                if (( corners&RoundedCorners.BottomLeft )==RoundedCorners.BottomLeft)
                    g.FillEllipse ( b , x , y+rec.Height-( diameter+1 ) , diameter , diameter );
                else
                    g.FillRectangle ( b , x , y+rec.Height-( diameter+1 ) , diameter , diameter );

                if (( corners&RoundedCorners.BottomRight )==RoundedCorners.BottomRight)
                    g.FillEllipse ( b , x+rec.Width-( diameter+1 ) , y+rec.Height-( diameter+1 ) , diameter , diameter );
                else
                    g.FillRectangle ( b , x+rec.Width-( diameter+1 ) , y+rec.Height-( diameter+1 ) , diameter ,
                                    diameter );
            }
        }

        

        public enum RoundedCorners {
            None = 0x00,
            TopLeft = 0x02,
            TopRight = 0x04,
            BottomLeft = 0x08,
            BottomRight = 0x10,
            All = 0x1F
        }
    }
}
