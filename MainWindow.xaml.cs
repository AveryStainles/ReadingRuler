using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace ReadingRuler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ChangeRulerBodySize(object sender, MouseWheelEventArgs e)
        {
            var maxHeight = (window.Height - grid.RowDefinitions[1].Height.Value) / 2;
            var minHeight = 50;
            var height = GetGridLengthFromDouble(grid.RowDefinitions[0].Height.Value + (e.Delta > 0 ? 15 : -15));

            if (height.Value < grid.RowDefinitions[0].MinHeight)
            {
                height = GetGridLengthFromDouble(minHeight);
            }
            else if (height.Value > (window.Height / 2))
            {
                height = GetGridLengthFromDouble(maxHeight);
            }

            grid.RowDefinitions[0].Height = height;
            grid.RowDefinitions[2].Height = height;
        }

        private GridLength GetGridLengthFromDouble(double value) => new GridLength(value, GridUnitType.Pixel);

        private void ClrPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            if (!ClrPicker.SelectedColor.HasValue)
                return;

            Brush brush = new SolidColorBrush(ClrPicker.SelectedColor.Value);
            row0.Fill = brush;
            row2.Fill = brush;
            ClrPicker.Background = brush;
        }
    }
}