using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            if (!_regex.IsMatch(rulerGapTextBox.Text))
            {
                var newRowHeight = new GridLength(Double.Parse(rulerGapTextBox.Text), GridUnitType.Pixel);
                grid.RowDefinitions[0].Height = new GridLength(50, GridUnitType.Pixel);
                grid.RowDefinitions[1].Height = newRowHeight;
                grid.RowDefinitions[2].Height = new GridLength(50, GridUnitType.Pixel);
                grid.RowDefinitions[3].Height = new GridLength(50, GridUnitType.Pixel);

                window.Height = grid.RowDefinitions[0].Height.Value + grid.RowDefinitions[1].Height.Value + grid.RowDefinitions[2].Height.Value + grid.RowDefinitions[3].Height.Value;
            }
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
            heightRulerLabel.Visibility = visibility;
            heightRulerBody.Visibility = visibility;
            alwaysOnTopLabel.Visibility = visibility;
            alwaysOnTopCheckBox.Visibility = visibility;
            applyBtn.Visibility = visibility;
        }
    }
}