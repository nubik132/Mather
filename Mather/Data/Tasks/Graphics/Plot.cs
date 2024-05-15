using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Mather.Data.Tasks.Graphics
{
    public abstract class Plot : IPlot
    {
        public CoordinatePlane Parent { get; set; }
        public abstract Shape Draw(double x1, double x2);
        public abstract double GetY(double x);
        public Plot(CoordinatePlane parent)
        {
            Parent = parent;
        }
    }
}
