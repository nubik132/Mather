using Mather.Data.Logs;
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
        public LinePlot(CoordinatePlane parent, double k = 1, double b = 0) : base()
        {
            K = k; B = b;
        }
        public LinePlot(CoordinatePlane parent, Point a, Point b) : base()
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

        public override Shape Draw(double x1, double x2, Point center, double size)
        {
            double y1 = ConvertX(x1, center, size);
            double y2 = ConvertX(x2, center, size);

            Line line = new Line();
            line.X1 = x1; line.Y1 = y1;
            line.X2 = x2; line.Y2 = y2;
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
            if (obj is LinePlot plot)
            {
                Log.Logs.Clear();

                    Log.Logs.Add(new LogElement("K", plot.K.ToString(), this.K.ToString(), plot.K == this.K));
                    Log.Logs.Add(new LogElement("B", plot.B.ToString(), this.B.ToString(), plot.B == this.B));

                    Log.Logs.Add(new LogElement("x константа?", 
                        plot.IsHorizontal ? "Да" : "Нет", 
                        this.IsHorizontal ? "Да" : "Нет",
                        this.IsHorizontal == plot.IsHorizontal));

                return this.K == plot.K && this.B == plot.B && this.IsHorizontal == plot.IsHorizontal;
            }
            return false;
        }

        public override int CompareTo(object? obj)
        {
            if(obj is LinePlot plot)
            {
                int result = (int)(GetY(1) - plot.GetY(1));
                if (result == 0) return (int)(GetY(2) - plot.GetY(2));
                return result;
            }
            throw new Exception("При проверке графиков возникла ошибка");
        }
    }
}
