using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PashIDE.Components
{
    public class ContextMenuStripFlat : ContextMenuStrip
    {
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new Pen(Color.White).Brush, 0, 0, Width, Height);
        }
    }
}
