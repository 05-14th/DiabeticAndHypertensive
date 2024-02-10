using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class RoundedPanel : Panel
{
    public int CornerRadius { get; set; } = 5;

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
