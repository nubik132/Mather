using Mather.Data.Logs;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Point = System.Windows.Point;

namespace Mather.Data.Tasks.Graphics
{
    internal class Exponential : Plot
    {
        double A { get; set; }
        public Exponential(CoordinatePlane parent, Point a, Point b) : base()
        {
            a = parent.ToRelative(a);
            b = parent.ToRelative(b);

            if (a.X == b.X)
            {
                throw new ArgumentException("x1 и x2 не должны быть равны");
            }
            if (a.Y <= 0 || b.Y <= 0)
            {
                throw new ArgumentException("y1 и y2 должны быть больше нуля");
            }

            A = Math.Pow(b.Y / a.Y, 1 / (b.X - a.X));
        }
        public override Shape Draw(double x1, double x2, Point center, double size)
        {
            Path path = new Path() 
            {
                StrokeThickness = 3
            };
            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure();

            // Начальная точка
            pathFigure.StartPoint = new Point(x1, ConvertX(x1, center, size));

            // Создаем кривую с помощью Bezier-сегментов
            int numPoints = 100; // Количество точек для приближения кривой
            for (int i = 1; i <= numPoints; i++)
            {
                double t = (double)i / numPoints;
                double x = x1 + t * (x2 - x1);
                double y = ConvertX(x, center, size);

                LineSegment segment = new LineSegment(new Point(x, y), true);
                pathFigure.Segments.Add(segment);
            }

            pathGeometry.Figures.Add(pathFigure);
            path.Data = pathGeometry;

            return path;
        }

        public override double GetY(double x)
        {
            return Math.Pow(A, x);
        }

        public override bool Equals(object? obj)
        {
            Log.PreText = "Показательная функция";
            if (obj is Exponential plot)
            {
                Log.Logs.Clear();

                Log.Logs.Add(new LogElement("A", plot.A.ToString(), this.A.ToString(), plot.A == this.A));

                bool result = this.A == plot.A;

                Log.PostText = result ? "Графики совпадают (Правильно)" : "Совпадение графиков отсутстует (Не правильно)";

                return result;
            }
            return false;
        }
    }
}
