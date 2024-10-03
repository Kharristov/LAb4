using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LAb4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isDrawing;
        private Point lastPoint;
        private SolidColorBrush currentBrush;
        private double brushSize;

        public MainWindow()
        {
            InitializeComponent();
            currentBrush = Brushes.Black;
            brushSize = 5;
        }
        private void ColorPicker_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (colorPicker.SelectedItem is ComboBoxItem selectedColor)
            {
                currentBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(selectedColor.Content.ToString()));
            }
        }

        private void BrushSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            brushSize = e.NewValue;
        }

        private void Mode_Checked(object sender, RoutedEventArgs e)
        {
            isDrawing = drawMode.IsChecked == true;
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isDrawing)
            {
                lastPoint = e.GetPosition(drawingCanvas);
                isDrawing = true;
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing && e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentPoint = e.GetPosition(drawingCanvas);
                DrawLine(lastPoint, currentPoint);
                lastPoint = currentPoint;
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isDrawing)
            {
                isDrawing = false;
            }
        }

        private void DrawLine(Point start, Point end)
        {
            Line line = new Line
            {
                Stroke = currentBrush,
                StrokeThickness = brushSize,
                X1 = start.X,
                Y1 = start.Y,
                X2 = end.X,
                Y2 = end.Y
            };
            drawingCanvas.Children.Add(line);
        }
    }
}