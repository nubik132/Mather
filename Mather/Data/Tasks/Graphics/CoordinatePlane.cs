using Mather.Data.Logs;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Shapes;

namespace Mather.Data.Tasks.Graphics
{
    public class CoordinatePlane : ILogable
    {
        private PlotLog _log;
        public ObservableCollection<Plot> Plots { get; set; }
        public double Size { get; set; }
        public Point Center { get; set; }
        public CoordinatePlane()
        {
            Plots = new ObservableCollection<Plot>();
            Size = 50;
            Center = new Point();
            _log = new PlotLog();
        }
        public Point ToAbsolute(Point point)
        {
            return new Point(Center.X + point.X * Size, Center.Y - point.Y * Size);
        }

        public Point ToRelative(Point point)
        {
            return new Point((point.X - Center.X) / Size, (Center.Y - point.Y) / Size);
        }
        public static Point ToAbsolute(Point point, Point center, double size)
        {
            return new Point(center.X + point.X * size, center.Y - point.Y * size);
        }

        public static Point ToRelative(Point point, Point center, double size)
        {
            return new Point((point.X - center.X) / size, (center.Y - point.Y) / size);
        }
        public Point SnapToGrid(Point a)
        {
            a = ToRelative(a);
            a.X = Math.Round(a.X);
            a.Y = Math.Round(a.Y);
            a = ToAbsolute(a);
            return a;
        }
        public bool ComparePlots(CoordinatePlane plane)
        {
            if (this.Plots.Count != plane.Plots.Count) return false;

            if (plane.Plots.Count == 0) return false;

            _log.Logs.Clear();

            var answerArray = this.Plots.ToArray();
            var userArray = plane.Plots.ToArray();

            Array.Sort(answerArray);
            Array.Sort(userArray);

            var IsEqual = true;

            for (int i = 0; i < answerArray.Length; i++)
            {
                var ArePlotsEqual = answerArray[i].Equals(userArray[i]);
                _log.Logs.Add(answerArray[i].GetLog() as TaskLog);
                if (!ArePlotsEqual) IsEqual = false;
            }
            return IsEqual;
        }

        public Log GetLog()
        {
            _log.PreText = "Координатная плоскость. Графиков: " + Plots.Count.ToString();
            return _log;
        }
    }
}
