using Mather.Data.Tasks.Graphics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Mather
{
    /// <summary>
    /// Логика взаимодействия для LineGraphWindow.xaml
    /// </summary>
    public partial class LineGraphWindow : Window
    {
        private CoordinatePlane _coordinatePlane;
        private Point startPoint;
        private Point endPoint;
        private Ellipse _previewEllipse;
        private Ellipse _startPointEllipse;
        private Point Center
        {
            get { return _center; }
            set
            {
                _center = value;
                _coordinatePlane.Center = value;
            }
        }
        private Point _center;
        public LineGraphWindow()
        {
            InitializeComponent();
            _coordinatePlane = new CoordinatePlane();
            _coordinatePlane.Size = 25;
            _previewEllipse = new Ellipse
            {
                Width = 5,
                Height = 5,
                Fill = Brushes.Gray,
                Visibility = Visibility.Hidden
            };
            _startPointEllipse = new Ellipse()
            {
                Width = 5,
                Height = 5,
                Fill = Brushes.Black,
                Visibility = Visibility.Hidden
            };
            CoordinateCanvas.Children.Add(_previewEllipse);
            CoordinateCanvas.Children.Add(_startPointEllipse);
        }
        private void CoordinateCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            RenderPlate();
            RenderPlots();

        }
        private void RenderPlate()
        {
            #region Prepare
            CoordinateCanvas.Children.Clear();
            CoordinateCanvas.Children.Add(_previewEllipse);

            Center = new Point(Math.Round(CoordinateCanvas.ActualWidth / 2), Math.Round(CoordinateCanvas.ActualHeight / 2));
            #endregion
            #region Additional Axes
            for (double i = _coordinatePlane.Size; i < Center.X; i += _coordinatePlane.Size)
            {
                addVerticalHelpLine(Center.X - i, 0, CoordinateCanvas.ActualHeight);
                addVerticalHelpLine(Center.X + i, 0, CoordinateCanvas.ActualHeight);
            }

            for (double i = _coordinatePlane.Size; i < Center.Y; i += _coordinatePlane.Size)
            {

                addHorizontalHelpLine(Center.Y - i, 0, CoordinateCanvas.ActualWidth);
                addHorizontalHelpLine(Center.Y + i, 0, CoordinateCanvas.ActualWidth);
            }
            #endregion
            #region Main Axes
            var verticalMainLine = LineFabric.Line(new Point(Center.X, 0), new Point(Center.X, CoordinateCanvas.ActualHeight), Brushes.Black);
            var horizontalMainLine = LineFabric.Line(new Point(0, Center.Y), new Point(CoordinateCanvas.ActualWidth, Center.Y), Brushes.Black);

            CoordinateCanvas.Children.Add(verticalMainLine);
            CoordinateCanvas.Children.Add(horizontalMainLine);
            #endregion
            #region Helpful Funcs
            void addVerticalHelpLine(double offset, double start, double finish)
            {
                CoordinateCanvas.Children.Add(
                    LineFabric.Line(
                        new Point(offset, start),
                        new Point(offset, finish),
                        Brushes.LightGray)
                    );
            }

            void addHorizontalHelpLine(double offset, double start, double finish)
            {
                CoordinateCanvas.Children.Add(
                    LineFabric.Line(
                        new Point(start, offset),
                        new Point(finish, offset),
                        Brushes.LightGray)
                    );
            }
            #endregion
        }

        private void RenderPlots()
        {
            foreach (var plot in _coordinatePlane.Plots)
            {
                Shape shape = plot.Draw(0, CoordinateCanvas.ActualWidth);

                shape.Stroke = Brushes.LightPink;
                CoordinateCanvas.Children.Add(shape);
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CoordinateCanvas_Loaded(sender, e);
        }
        private void AddLinePlot_Click(object sender, RoutedEventArgs e)
        {
            CoordinateCanvas.MouseLeftButtonDown += AddLine_CoordinateCanvas_MouseLeftButtonDown;
            AddLineMenu.IsEnabled = false;
            CoordinateCanvas.MouseMove += Canvas_MouseMove;
        }

        private void AddLine_CoordinateCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (startPoint == default)
            {
                // Первое нажатие
                startPoint = e.GetPosition(CoordinateCanvas);
                startPoint.Y = Math.Round(startPoint.Y);
                startPoint = _coordinatePlane.SnapToGrid(startPoint);

                Canvas.SetLeft(_startPointEllipse, startPoint.X - _startPointEllipse.Width / 2);
                Canvas.SetTop(_startPointEllipse, startPoint.Y - _startPointEllipse.Height / 2);
                CoordinateCanvas.Children.Add(_startPointEllipse);
                _startPointEllipse.Visibility = Visibility.Visible;
            }
            else
            {
                // Второе нажатие
                endPoint = e.GetPosition(CoordinateCanvas);
                endPoint.Y = Math.Round(endPoint.Y);
                endPoint = _coordinatePlane.SnapToGrid(endPoint);

                if (startPoint != endPoint)
                {
                    LinePlot line = new LinePlot(_coordinatePlane, startPoint, endPoint);
                    _coordinatePlane.Plots.Add(line);
                }
                else
                {
                    MessageBox.Show("Начальная и конечная точки совпадают.\n\nСоздание линии отменено.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                // Сбросить состояние
                startPoint = default;
                CoordinateCanvas.MouseLeftButtonDown -= AddLine_CoordinateCanvas_MouseLeftButtonDown;
                CoordinateCanvas_Loaded(sender, e);
                AddLineMenu.IsEnabled = true;
                CoordinateCanvas.MouseMove -= Canvas_MouseMove;
                _previewEllipse.Visibility = Visibility.Hidden;
                _startPointEllipse.Visibility = Visibility.Hidden;
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point cursorPosition = e.GetPosition(CoordinateCanvas);
            cursorPosition.Y = Math.Round(cursorPosition.Y);
            Point snappedPosition = _coordinatePlane.SnapToGrid(cursorPosition);

            Canvas.SetLeft(_previewEllipse, snappedPosition.X - _previewEllipse.Width / 2);
            Canvas.SetTop(_previewEllipse, snappedPosition.Y - _previewEllipse.Height / 2);
            _previewEllipse.Visibility = Visibility.Visible;
        }
    }
}
