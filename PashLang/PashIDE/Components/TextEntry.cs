using PashIDE;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Components {
    public class TextEntry : Control, OpaticControl {

        public TextBox TB = new TextBox();
        private int _maxchars = 32767;
        private bool _ReadOnly;
        private bool _Multiline;
        private Image _Image;
        private Size _ImageSize;
        private HorizontalAlignment ALNType;
        private bool isPasswordMasked = false;
        private Pen P1;
        private SolidBrush B1;
        private GraphicsPath Shape;

        private Color backGroundColor;

        public Color BackGroundColor {
            get { return backGroundColor; }
            set { backGroundColor=value; TB.BackColor=value; }
        }

        #region Decloration

        public HorizontalAlignment TextAlignment {
            get {
                return ALNType;
            }
            set {
                ALNType=value;
                Invalidate ( );
            }
        }
        public int MaxLength {
            get {
                return _maxchars;
            }
            set {
                _maxchars=value;
                TB.MaxLength=MaxLength;
                Invalidate ( );
            }
        }

        public bool UseSystemPasswordChar {
            get {
                return isPasswordMasked;
            }
            set {
                TB.UseSystemPasswordChar=UseSystemPasswordChar;
                isPasswordMasked=value;
                Invalidate ( );
            }
        }
        public bool ReadOnly {
            get {
                return _ReadOnly;
            }
            set {
                _ReadOnly=value;
                if (TB!=null) {
                    TB.ReadOnly=value;
                }
            }
        }
        public bool Multiline {
            get {
                return _Multiline;
            }
            set {
                _Multiline=value;
                if (TB!=null) {
                    TB.Multiline=value;

                    if (value) {
                        TB.Height=Height-23;
                    } else {
                        Height=TB.Height+23;
                    }
                }
            }
        }

        public Image Image {
            get {
                return _Image;
            }
            set {
                if (value==null) {
                    _ImageSize=Size.Empty;
                } else {
                    _ImageSize=value.Size;
                }

                _Image=value;

                if (Image==null) {
                    TB.Location=new Point ( 8 , 10 );
                } else {
                    TB.Location=new Point ( 35 , 11 );
                }
                Invalidate ( );
            }
        }

        protected Size ImageSize {
            get {
                return _ImageSize;
            }
        }

        private void _Enter ( object Obj , EventArgs e ) {
            P1=new Pen ( backGroundColor );
            Refresh ( );
        }

        private void _Leave ( object Obj , EventArgs e ) {
            P1=new Pen ( backGroundColor );
            Refresh ( );
        }

        private void OnBaseTextChanged(object s, EventArgs e) {


            if (OnlyNum && TB.Text != "")
                try
                {
                    int pt = int.Parse(TB.Text);
                    Text = TB.Text;
                }
                catch (Exception E)
                {
                    E.GetType();
                    //String is not a number
                    //Replace it with the last string...
                    TB.Text = Text;
                }
            else Text = TB.Text;
        }

        protected override void OnTextChanged ( System.EventArgs e ) {
            base.OnTextChanged ( e );
            TB.Text=Text;
            Invalidate ( );
        }

        protected override void OnForeColorChanged ( System.EventArgs e ) {
            base.OnForeColorChanged ( e );
            TB.ForeColor=ForeColor;
            Invalidate ( );
        }

        protected override void OnFontChanged ( System.EventArgs e ) {
            base.OnFontChanged ( e );
            TB.Font=Font;
        }

        protected override void OnPaintBackground ( PaintEventArgs e ) {
            base.OnPaintBackground ( e );
        }

        private void _OnKeyDown ( object Obj , KeyEventArgs e ) {
            if (e.Control&&e.KeyCode==Keys.A) {
                TB.SelectAll ( );
                e.SuppressKeyPress=true;
            }
            if (e.Control&&e.KeyCode==Keys.C) {
                TB.Copy ( );
                e.SuppressKeyPress=true;
            }
        }

        private bool OnlyNum;

        public bool OnlyAllowNumbers
        {
            get { return OnlyNum; }
            set { OnlyNum = value; }
        }


        protected override void OnResize ( System.EventArgs e ) {
            base.OnResize ( e );
            if (_Multiline) {
                TB.Height=Height-23;
            } else {
                Height=TB.Height+23;
            }

            Shape=new GraphicsPath ( );
            Shape.AddArc ( 0 , 0 , 10 , 10 , 180 , 90 );
            Shape.AddArc ( Width-11 , 0 , 10 , 10 , -90 , 90 );
            Shape.AddArc ( Width-11 , Height-11 , 10 , 10 , 0 , 90 );
            Shape.AddArc ( 0 , Height-11 , 10 , 10 , 90 , 90 );
            Shape.CloseAllFigures ( );
        }

        protected override void OnGotFocus ( System.EventArgs e ) {
            base.OnGotFocus ( e );
            Refresh ( );
            TB.Focus ( );
        }

        public void _TextChanged ( System.Object sender , System.EventArgs e ) {
            Text=TB.Text;
        }

        public void _BaseTextChanged ( System.Object sender , System.EventArgs e ) {
            TB.Text=Text;
        }

        protected override void OnEnter ( EventArgs e ) {
            TB.Visible=true;
            TB.Focus ( );
        }



        #endregion

        public void AddTextBox ( ) {
            TB.Location=new Point ( 8 , 10 );
            TB.Text=String.Empty;
            TB.BorderStyle=BorderStyle.None;
            TB.TextAlign=HorizontalAlignment.Left;
            TB.Font=new Font ( "Tahoma" , 11 );
            TB.UseSystemPasswordChar=UseSystemPasswordChar;
            TB.Multiline=false;
            TB.BackColor=BackGroundColor;
            TB.ScrollBars=ScrollBars.None;
            TB.KeyDown+=_OnKeyDown;
            TB.Enter+=_Enter;
            TB.Leave+=_Leave;
            TB.TextChanged+=OnBaseTextChanged;
            TB.GotFocus+=TB_GotFocus;
            TB.LostFocus+=TB_LostFocus;
            TB.KeyUp+=TB_KeyUp;
        }

        void TB_KeyUp ( object sender , KeyEventArgs e ) {

            OnKeyUp ( e );
        }

        private void TB_LostFocus ( object sender , EventArgs e ) {
            TB.Visible=true;
            Refresh ( );
        }

        private void TB_GotFocus ( object sender , EventArgs e ) {
            TB.Visible=true;
            Refresh ( );
        }

        protected override void OnMouseEnter ( EventArgs e ) {
            Cursor=Cursors.IBeam;
        }

        protected override void OnMouseLeave ( EventArgs e ) {
            Cursor=Cursors.Default;
        }

        public Color ForeColor;

        public TextEntry ( ) {
            SetStyle ( ControlStyles.SupportsTransparentBackColor , true );

            AddTextBox ( );
            Controls.Add ( TB );

            P1=new Pen ( backGroundColor );
            B1=new SolidBrush ( backGroundColor );
            BackColor=Color.Transparent;

            Text=null;
            Font=new Font ( "Tahoma" , 11 );
            Size=new Size ( 135 , 43 );
            DoubleBuffered=true;
        }

        protected override void OnClick ( EventArgs e ) {
            TB.Visible=true;
            TB.Focus ( );
        }

        private String defaultText = "Default Text";

        public String DefaultText {
            get { return defaultText; }
            set { defaultText=value; }
        }

        private Color defaultTextColor = Color.Gray;

        public Color DefaultTextColor {
            get { return defaultTextColor; }
            set { defaultTextColor=value; }
        }

        public void setOpacity ( float Opac ) {
            TB.BackColor=backGroundColor;
            TB.Refresh();
            opacity=Opac;
            Refresh ( );
        }

        public float getOpacity ( ) {
            return opacity;
        }

        private float opacity = 1.0f;

        public float Opacity { get { return opacity; } set { opacity=value; Refresh ( ); } }

        protected override void OnPaint ( System.Windows.Forms.PaintEventArgs e ) {
            base.OnPaint ( e );
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);

            G.SmoothingMode=SmoothingMode.AntiAlias;


            if (Image==null) {
                TB.Width=Width-18;
            } else {
                TB.Width=Width-45;
            }

            TB.TextAlign=TextAlignment;
            TB.UseSystemPasswordChar=UseSystemPasswordChar;

            G.Clear ( Color.Transparent );
            Color useColor = BackGroundColor;
            if (BackGroundColor == BackColor)
            {
                useColor = backGroundColor;
                Console.WriteLine(this.ToString() + " :  SET");
            }
            Pen pen = new Pen(Util.Mix(BackColor, useColor, opacity));
            G.FillPath ( pen.Brush , Shape );
            G.DrawPath ( pen , Shape );

            if (Image!=null) {
                G.DrawImage ( _Image , 5 , 8 , 24 , 24 );
            }

            e.Graphics.DrawImage ( (Image)( B.Clone ( ) ) , 0 , 0 );


            G.Dispose ( );
            B.Dispose ( );

            if (TB.Text.Length==0&&!TB.Focused) {
                Graphics g = e.Graphics;

                pen=new Pen ( Util.Mix ( BackColor , DefaultTextColor , opacity ) );
                SizeF sz = e.Graphics.MeasureString(defaultText, Font);
                int loc = 35;
                if (Image==null) loc=3;
                g.DrawString ( defaultText , Font , pen.Brush , new PointF ( loc , ( Height/2 )-( sz.Height/2 ) ) );
                TB.Visible=false;
                TB.Focus ( );
            }
        }
    }
}