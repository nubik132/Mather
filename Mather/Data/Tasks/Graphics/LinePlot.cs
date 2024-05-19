using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace Mather.Data.Tasks.Graphics
{
    public class LinePlot : Plot
    {
        bool IsHorizontal = false;
        public double K
        {
            get { return k; }
            set
            {
                if (value == 0) k = 1;
                else k = value;
            }
        }
        public double B { get; set; }
        private double k;
        public LinePlot(CoordinatePlane parent, double k = 1, double b = 0) : base(parent) 
        {
            K = k; B = b;
        }
        public LinePlot(CoordinatePlane parent, Point a, Point b) : base(parent)
        {
            a = parent.ToRelative(a);
            b = parent.ToRelative(b);
            if (a.Y == b.Y)
            {
                IsHorizontal = true;
                B = b.Y;
            }
            else
            {
                K = (b.Y - a.Y) / (b.X - a.X);
                B = a.Y - k * a.X;
            }

        }

        public override Shape Draw(double x1, double x2)
        {
            Point rel1 = Parent.ToRelative(new Point(x1, 0));
            Point rel2 = Parent.ToRelative(new Point(x2, 0));

            double y1 = GetY(rel1.X);
            double y2 = GetY(rel2.X);

            Point abs1 = Parent.ToAbsolute(new Point(0, y1));
            Point abs2 = Parent.ToAbsolute(new Point(0, y2));

            Line line = new Line();
            line.X1 = x1; line.Y1 = abs1.Y;
            line.X2 = x2; line.Y2 = abs2.Y;
            line.StrokeThickness = 3;

            return line;
        }
        public override double GetY(double x)
        {
            if (IsHorizontal) return B;
            return K * x + B;
        }

        public override bool Equals(object? obj)
        {
            if (obj is LinePlot plot) { return this.K == plot.K && this.B == plot.B && this.IsHorizontal == plot.IsHorizontal; }
            return false;
        }
    }
}
