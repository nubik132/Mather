using Mather.Data.Logs;
using System.Windows;
using System.Windows.Shapes;

namespace Mather.Data.Tasks.Graphics
{
    public abstract class Plot : IPlot, ILogable, IComparable
    {
        public TaskLog Log;
        public abstract Shape Draw(double x1, double x2, Point center, double size);
        public abstract double GetY(double x);
        public Plot()
        {
            Log = new TaskLog();
        }
        public virtual Log GetLog()
        {
            return Log;
        }
        public double ConvertX(double x, Point center, double size)
        {
            Point rel1 = CoordinatePlane.ToRelative(new Point(x, 0), center, size);
            double y = GetY(rel1.X);
            Point abs = CoordinatePlane.ToAbsolute(new Point(0, y), center, size);
            return abs.Y;
        }

        public abstract int CompareTo(object? obj);
    }
}
