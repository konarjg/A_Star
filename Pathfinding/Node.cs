using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Pathfinding
{
    [Category("g cost"), Description("Specifies g cost of the node.")]
    public class Node : Control
    {
        public bool selected;
        public bool blocked;
        public bool start;
        public bool end;
        public bool chosen;

        public float g;
        public float f;
        public float h;

        private int x;
        private int y;

        public Node(bool start, bool end, bool selected, bool blocked, float g, float h, float f, int x, int y)
        {
            this.start = start;
            this.end = end;
            this.selected = selected;
            this.blocked = blocked;
            this.g = g;
            this.h = h;
            this.f = f;
            this.x = x;
            this.y = y;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (this.selected)
                return;

            Form1 window = (Form1)this.FindForm();

            if (window.path.Count != 0)
                return;

            if (window.start != null)
            {
                if (window.start.Equals(this))
                    return;
            }
            if (window.end != null)
            {
                if (window.end.Equals(this))
                    return;
            }
            if (blocked)
                return;

            if (window.selected != null)
            {
                if (window.selected.Equals(this))
                {
                    chosen = false;
                    window.selected = null;
                    Invalidate();
                }
                else
                {
                    chosen = true;
                    window.selected.chosen = false;
                    window.selected.Invalidate();
                    window.selected = this;
                    Invalidate();
                }
            }
            else
            {
                chosen = true;
                window.selected = this;
                Invalidate();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            var graphics = e.Graphics;

            Brush brush = Brushes.Blue;

            if (this.selected)
                brush = Brushes.Lime;
            if (this.blocked)
                brush = Brushes.Black;
            if (this.start || this.end)
                brush = Brushes.Red;
            if (this.chosen)
                brush = Brushes.BlueViolet;

            graphics.FillRectangle(brush, this.ClientRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var graphics = e.Graphics;

            var brush = Brushes.White;
            var font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);

            string pos = string.Format("({0}; {1})", x, y);
            string cost = f.ToString();

            var costPos = new Point(ClientRectangle.Left, ClientRectangle.Bottom - 20);

            if (start)
            {
                var textFont = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold);

                var textPos = new PointF(ClientRectangle.Size.Width / 2 - 52.5f, ClientRectangle.Size.Height / 2 - 15);
                graphics.DrawString("START", textFont, Brushes.Lime, textPos);
            }

            if (end)
            {
                var textFont = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold);

                var textPos = new PointF(ClientRectangle.Size.Width / 2 - 52.5f, ClientRectangle.Size.Height / 2 - 15);
                graphics.DrawString("FINISH", textFont, Brushes.Lime, textPos);
            }

            graphics.DrawString(pos, font, brush, ClientRectangle.Location);
            graphics.DrawString(cost, font, brush, costPos);
        }
    }
}
