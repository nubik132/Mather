using Mather.Data.Logs;
using System.Windows;
using System.Windows.Shapes;

namespace Mather.Data.Tasks.Graphics
{
    public abstract class Plot : IPlot, ILogable
    {
        protected TaskLog _log;
        public CoordinatePlane Parent { get; set; }
        public abstract Shape Draw(double x1, double x2);
        public abstract double GetY(double x);
        public Plot(CoordinatePlane parent)
        {
            Parent = parent;
        }
        public virtual Log GetLog()
        {
            return _log;
        }
        protected double ConvertX(double x)
        {
            Point rel1 = Parent.ToRelative(new Point(x, 0));
            double y = GetY(rel1.X);
            Point abs = Parent.ToAbsolute(new Point(0, y));
            return abs.Y;
        }
    }
}
