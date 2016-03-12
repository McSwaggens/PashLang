using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashIDE
{
    class Util
    {
        public static Color Mix(Color from, Color to, float percent)
        {
            float amountFrom = 1.0f - percent;

            return Color.FromArgb(
            (int)(from.A * amountFrom + to.A * percent),
            (int)(from.R * amountFrom + to.R * percent),
            (int)(from.G * amountFrom + to.G * percent),
            (int)(from.B * amountFrom + to.B * percent));
        }

        public static Color Mix(Color from, Color to, int percent)
        {
            float perc = percent / 100.0f;
            float amountFrom = 1.0f - perc;

            return Color.FromArgb(
            (int)(from.A * amountFrom + to.A * perc),
            (int)(from.R * amountFrom + to.R * perc),
            (int)(from.G * amountFrom + to.G * perc),
            (int)(from.B * amountFrom + to.B * perc));
        }

        public static void DrawRoundedRectangle(Graphics g, Color color, Rectangle rec, int radius, RoundedCorners corners)
        {
            using (var b = new SolidBrush(color))
            {
                int x = rec.X;
                int y = rec.Y;
                int diameter = radius * 2;
                var horiz = new Rectangle(x, y + radius, rec.Width, rec.Height - diameter);
                var vert = new Rectangle(x + radius, y, rec.Width - diameter, rec.Height);

                g.FillRectangle(b, horiz);
                g.FillRectangle(b, vert);

                if ((corners & RoundedCorners.TopLeft) == RoundedCorners.TopLeft)
                    g.FillEllipse(b, x, y, diameter, diameter);
                else
                    g.FillRectangle(b, x, y, diameter, diameter);

                if ((corners & RoundedCorners.TopRight) == RoundedCorners.TopRight)
                    g.FillEllipse(b, x + rec.Width - (diameter + 1), y, diameter, diameter);
                else
                    g.FillRectangle(b, x + rec.Width - (diameter + 1), y, diameter, diameter);

                if ((corners & RoundedCorners.BottomLeft) == RoundedCorners.BottomLeft)
                    g.FillEllipse(b, x, y + rec.Height - (diameter + 1), diameter, diameter);
                else
                    g.FillRectangle(b, x, y + rec.Height - (diameter + 1), diameter, diameter);

                if ((corners & RoundedCorners.BottomRight) == RoundedCorners.BottomRight)
                    g.FillEllipse(b, x + rec.Width - (diameter + 1), y + rec.Height - (diameter + 1), diameter, diameter);
                else
                    g.FillRectangle(b, x + rec.Width - (diameter + 1), y + rec.Height - (diameter + 1), diameter,
                                    diameter);
            }
        }
    }
    public enum RoundedCorners
    {
        None = 0x00,
        TopLeft = 0x02,
        TopRight = 0x04,
        BottomLeft = 0x08,
        BottomRight = 0x10,
        All = 0x1F
    }
}
