using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Mather.Data.Tasks.Graphics
{
    public static class LineFabric
    {
        private static Line DefaultLine(Point a, Point b)
        {
            return new Line()
            {
                X1 = a.X,
                Y1 = a.Y,
                X2 = b.X,
                Y2 = b.Y,
                StrokeThickness = 1
            };
        }

        public static Line Line(Point a, Point b, SolidColorBrush brush)
        {
            Line line = DefaultLine(a, b); 
            line.Stroke = brush;
            return line;
        }
    }
}
