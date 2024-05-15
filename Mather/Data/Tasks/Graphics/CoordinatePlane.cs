using System.Collections.ObjectModel;
using System.Windows;

namespace Mather.Data.Tasks.Graphics
{
    public class CoordinatePlane
    {
        public ObservableCollection<IPlot> Plots { get; set; }
        public double Size { get; set; }
        public Point Center { get; set; }
        public CoordinatePlane()
        {
            Plots = new ObservableCollection<IPlot>();
            Size = 50;
            Center = new Point();
        }
        public Point ToAbsolute(Point point)
        {
            return new Point(Center.X + point.X * Size, Center.Y - point.Y * Size);
        }

        public Point ToRelative(Point point)
        {
            return new Point((point.X - Center.X) / Size, (Center.Y - point.Y) / Size);
        }

        public Point SnapToGrid(Point a)
        {
            a = ToRelative(a);
            a.X = Math.Round(a.X);
            a.Y = Math.Round(a.Y);
            a = ToAbsolute(a);
            return a;
        }
    }
}
