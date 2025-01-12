using System.Text.RegularExpressions;
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
        private bool optionsVisible = false;
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        // When ui botton is click this enables all options to edit he window to be visible in a 4th row
        private void ToggleOptionMenu(object sender, RoutedEventArgs e)
        {
            ToggleVisibility();
        }

        private static readonly Regex _regex = new("[^0-9]+"); // REGEX
        // Apply changes button is pressed, get all the input fields and set the settings to match them
        private void ApplyChanges(object sender, RoutedEventArgs e)
        {
            // Validates all values are numbers
            if (_regex.IsMatch(rulerGapTextBox.Text) && _regex.IsMatch(rulerBodyTextBox.Text) && _regex.IsMatch(widthRulerBody.Text) /* TODO Add regex for RBGA texbox */)
            {
                return;
            }

            var rowGapHeight = new GridLength(Double.Parse(rulerGapTextBox.Text), GridUnitType.Pixel);
            var rowBodyHeight = new GridLength(Double.Parse(rulerBodyTextBox.Text), GridUnitType.Pixel);
            grid.RowDefinitions[0].Height = (rowBodyHeight.Value > 10) ? rowBodyHeight : new GridLength(10, GridUnitType.Pixel);
            grid.RowDefinitions[1].Height = (rowGapHeight.Value > 10) ? rowGapHeight : new GridLength(10, GridUnitType.Pixel);
            grid.RowDefinitions[2].Height = (rowBodyHeight.Value > 10) ? rowBodyHeight : new GridLength(10, GridUnitType.Pixel);
            grid.RowDefinitions[3].Height = new GridLength(50, GridUnitType.Pixel);
            grid.Width = (Double.Parse(widthRulerBody.Text) > 700) ? Double.Parse(widthRulerBody.Text) : 700;
            window.Height = grid.RowDefinitions[0].Height.Value + grid.RowDefinitions[1].Height.Value + grid.RowDefinitions[2].Height.Value + grid.RowDefinitions[3].Height.Value;


            var brush = (Brush) new BrushConverter().ConvertFromString(rgbaBody.Text);
            row0.Fill = brush;
            row2.Fill = brush;
            menuOptions.Fill = brush;


        }

        private void ToggleVisibility()
        {
            optionsVisible = !optionsVisible;
            var visibility = optionsVisible ? Visibility.Visible : Visibility.Collapsed;

            menuOptions.Visibility = visibility;
            rulerGapLabel.Visibility = visibility;
            rulerGapTextBox.Visibility = visibility;
            rulerBodyLabel.Visibility = visibility;
            rulerBodyTextBox.Visibility = visibility;
            rgbaLabel.Visibility = visibility;
            rgbaBody.Visibility = visibility;
            widthRulerLabel.Visibility = visibility;
            widthRulerBody.Visibility = visibility;
            applyBtn.Visibility = visibility;
        }
    }
}