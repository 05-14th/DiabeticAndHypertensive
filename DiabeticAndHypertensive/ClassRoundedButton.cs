using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiabeticAndHypertensive
{
    internal class ClassRoundedButton : Button
    {
        public int CornerRadius { get; set; } = 5;
        private static ClassRoundedButton currentButton;
        private Color originalColor;

        public ClassRoundedButton()
        {
            originalColor = this.BackColor; // Store the original color
            this.Click += OnClick;
        }
        private void OnClick(object sender, EventArgs e)
        {
            if (currentButton != null && currentButton != this)
            {
                // Reset the color of the previously clicked button
                currentButton.BackColor = currentButton.originalColor;
            }

            // Change the background color of the current button
            this.BackColor = Color.LightBlue; // You can set any color you prefer
            currentButton = this;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var path = GetRoundPath(new Rectangle(0, 0, Width, Height), CornerRadius))
            {
                this.Region = new Region(path);
                using (var pen = new Pen(Color.Transparent, 1.0f))
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private GraphicsPath GetRoundPath(RectangleF rect, int radius)
        {
            float r = radius;
            float diameter = r * 2;
            var size = new SizeF(diameter, diameter);
            var arc = new RectangleF(rect.Location, size);
            var path = new GraphicsPath();

            // Top left
            path.AddArc(arc, 180, 90);

            // Top right
            arc.X = rect.Right - diameter;
            path.AddArc(new RectangleF(new PointF(arc.X, arc.Y), size), 270, 90);

            // Bottom right
            arc.Y = rect.Bottom - diameter;
            path.AddArc(new RectangleF(new PointF(arc.X, arc.Y), size), 0, 90);

            // Bottom left
            arc.X = rect.Left;
            path.AddArc(new RectangleF(new PointF(arc.X, arc.Y), size), 90, 90);

            path.CloseFigure();

            return path;
        }
    }
}