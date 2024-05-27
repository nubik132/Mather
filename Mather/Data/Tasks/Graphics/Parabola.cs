using Mather.Data.Logs;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Mather.Data.Tasks.Graphics
{
    public class Parabola : Plot
    {
        double A { get; set; }
        double B { get; set; }
        double C { get; set; }
        public Parabola(CoordinatePlane parent, Point a, Point b) : base()
        {
            a = parent.ToRelative(a);
            b = parent.ToRelative(b);

            A = (b.Y - a.Y) / Math.Pow(b.X - a.X, 2);
            B = -2 * A * a.X;
            C = A * a.X * a.X + a.Y;
        }

        public override Shape Draw(double x1, double x2, Point center, double size)
        {
            PathFigure parabolaFigure = new PathFigure();
            parabolaFigure.StartPoint = new Point(x1, ConvertX(x1, center, size));

            for (double x = x1; x <= x2; x += 1)
            {
                double y = ConvertX(x, center, size);
                parabolaFigure.Segments.Add(new LineSegment(new Point(x, y), true));
            }

            PathGeometry parabolaGeometry = new PathGeometry();
            parabolaGeometry.Figures.Add(parabolaFigure);

            Path parabolaPath = new Path();
            parabolaPath.Stroke = Brushes.Blue;
            parabolaPath.StrokeThickness = 3;
            parabolaPath.Data = parabolaGeometry; 

            return parabolaPath;
        }

        public override double GetY(double x)
        {
            return A * x * x + B * x + C;
        }

        public override bool Equals(object? obj)
        {
            Log.PreText = "Парабола";
            if (obj is Parabola plot)
            {
                Log.Logs.Clear();

                Log.Logs.Add(new LogElement("A", plot.A.ToString(), this.A.ToString(), plot.A == this.A));
                Log.Logs.Add(new LogElement("B", plot.B.ToString(), this.B.ToString(), plot.B == this.B));
                Log.Logs.Add(new LogElement("C", plot.C.ToString(), this.C.ToString(), plot.C == this.C));

                bool result = this.A == plot.A && this.B == plot.B && this.C == plot.C;

                Log.PostText = result ? "Графики совпадают (Правильно)": "Совпадение графиков отсутстует (Не правильно)";

                return result;
            }
            return false;
        }
    }
}
