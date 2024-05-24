using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Mather.Data.Tasks.Graphics
{
    /// <summary>
    /// Логика взаимодействия для PlotControl.xaml
    /// </summary>
    public partial class PlotControl : UserControl
    {
        public static readonly DependencyProperty CoordinatePlaneProperty;
        static PlotControl()
        {
            CoordinatePlaneProperty = DependencyProperty.Register("CoordinatePlane", typeof(CoordinatePlane), typeof(PlotControl));
        }
        public CoordinatePlane CoordinatePlane
        {
            get { return (CoordinatePlane)GetValue(CoordinatePlaneProperty); }
            set { SetValue(CoordinatePlaneProperty, value); }
        }
        public ObservableCollection<Shape> Shapes
        { get; private set; }
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
                CoordinatePlane.Center = value;
            }
        }
        private Point _center;
        public PlotControl()
        {
            InitializeComponent();
            Shapes = new ObservableCollection<Shape>();
            //CoordinatePlane = new CoordinatePlane();
            //CoordinatePlane.Size = 25;
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
            Render();
        }
        private void Render()
        {
            RenderPlate();
            RenderPlots();
        }
        private void RenderPlate()
        {
            #region Prepare
            CoordinateCanvas.Children.Clear();
            CoordinateCanvas.Children.Add(_previewEllipse);
            Shapes.Clear();

            Center = new Point(Math.Round(CoordinateCanvas.ActualWidth / 2), Math.Round(CoordinateCanvas.ActualHeight / 2));
            #endregion
            #region Additional Axes
            for (double i = CoordinatePlane.Size; i < Center.X; i += CoordinatePlane.Size)
            {
                addVerticalHelpLine(Center.X - i, 0, CoordinateCanvas.ActualHeight);
                addVerticalHelpLine(Center.X + i, 0, CoordinateCanvas.ActualHeight);
            }

            for (double i = CoordinatePlane.Size; i < Center.Y; i += CoordinatePlane.Size)
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
            foreach (var plot in CoordinatePlane.Plots)
            {
                Shape shape = plot.Draw(0, CoordinateCanvas.ActualWidth, CoordinatePlane.Center, CoordinatePlane.Size);

                shape.Stroke = Brushes.LightPink;

                shape.ContextMenu = Resources["ShapeContextMenu"] as ContextMenu;
                CoordinateCanvas.Children.Add(shape);
                Shapes.Add(shape);
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Render();
        }
        private void AddLinePlot_Click(object sender, RoutedEventArgs e)
        {
            CoordinateCanvas.MouseLeftButtonDown += AddLine_CoordinateCanvas_MouseLeftButtonDown;
            AddLineButton.IsEnabled = false;
            CoordinateCanvas.MouseMove += Canvas_MouseMove;
        }

        private void AddLine_CoordinateCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (startPoint == default)
            {
                // Первое нажатие
                startPoint = e.GetPosition(CoordinateCanvas);
                startPoint.Y = Math.Round(startPoint.Y);
                startPoint = CoordinatePlane.SnapToGrid(startPoint);

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
                endPoint = CoordinatePlane.SnapToGrid(endPoint);
                if (startPoint.X == endPoint.X)
                {
                    MessageBox.Show("График не может множество значний.\n\nСоздание линии отменено.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (startPoint != endPoint)
                {
                    LinePlot line = new LinePlot(CoordinatePlane, startPoint, endPoint);
                    CoordinatePlane.Plots.Add(line);
                }
                else
                {
                    MessageBox.Show("Начальная и конечная точки совпадают.\n\nСоздание линии отменено.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                // Сбросить состояние
                startPoint = default;
                CoordinateCanvas.MouseLeftButtonDown -= AddLine_CoordinateCanvas_MouseLeftButtonDown;
                CoordinateCanvas_Loaded(sender, e);
                AddLineButton.IsEnabled = true;
                CoordinateCanvas.MouseMove -= Canvas_MouseMove;
                _previewEllipse.Visibility = Visibility.Hidden;
                _startPointEllipse.Visibility = Visibility.Hidden;
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point cursorPosition = e.GetPosition(CoordinateCanvas);
            cursorPosition.Y = Math.Round(cursorPosition.Y);
            Point snappedPosition = CoordinatePlane.SnapToGrid(cursorPosition);

            Canvas.SetLeft(_previewEllipse, snappedPosition.X - _previewEllipse.Width / 2);
            Canvas.SetTop(_previewEllipse, snappedPosition.Y - _previewEllipse.Height / 2);
            _previewEllipse.Visibility = Visibility.Visible;
        }

        private void DeleteShapeContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem &&
                menuItem.Parent is ContextMenu contextMenu &&
                contextMenu.PlacementTarget is Shape shape)
            {
                var index = Shapes.IndexOf(shape);
                Shapes.RemoveAt(index);
                CoordinatePlane.Plots.RemoveAt(index);
            }
            Render();
        }

    }
}

