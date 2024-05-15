using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace Mather.Data.Tasks.Graphics
{
    public interface IPlot
    {
        public Shape Draw(double x1, double x2);
        public double GetY(double x);
    }
}
