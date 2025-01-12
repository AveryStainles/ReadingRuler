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


        // Apply changes button is pressed, get all the input fields and set the settings to match them
        private void ApplyChanges(object sender, RoutedEventArgs e)
        {
            // Ensures all input fields have appropriate error messages and correct input types
            if (!ValidatedInputFields())
            {
                return;
            }

            grid.RowDefinitions[3].Height = new GridLength(50, GridUnitType.Pixel);
            window.Height = grid.RowDefinitions[0].Height.Value + grid.RowDefinitions[1].Height.Value + grid.RowDefinitions[2].Height.Value + grid.RowDefinitions[3].Height.Value;
        }


        private static readonly Regex _regexHexCode = new("^#[0-9a-fA-F]{6}$");
        private static readonly Regex _regex = new("[^0-9]+"); // REGEX
        private bool ValidatedInputFields()
        {
            // Validation Colours
            var error_colour = new SolidColorBrush(Colors.Red);
            var valid_colour = new SolidColorBrush(Colors.Transparent);

            // Input Fields Validation

            string errorMessage = "";

            // Validates the 'Ruler Gap' input field
            if (rulerGapTextBox.Text.Length == 0 || _regex.IsMatch(rulerGapTextBox.Text))
            {
                errorMessage += "Only Numbers are allowed. Fields may NOT be empty\n";
                rulerGapTextBox.BorderBrush = error_colour;
            } else
            {
                rulerGapTextBox.BorderBrush = valid_colour;
                var rowGapHeight = new GridLength(Double.Parse(rulerGapTextBox.Text), GridUnitType.Pixel);
                grid.RowDefinitions[1].Height = (rowGapHeight.Value > 10) ? rowGapHeight : new GridLength(10, GridUnitType.Pixel);
            }

            // Validates the 'Ruler Body' input field
            if (rulerBodyTextBox.Text.Length == 0 || _regex.IsMatch(rulerBodyTextBox.Text))
            {
                errorMessage += "Only Numbers are allowed. Fields may NOT be empty\n";
                rulerBodyTextBox.BorderBrush = error_colour;
            } else
            {
                rulerBodyTextBox.BorderBrush = valid_colour;
                var rowBodyHeight = new GridLength(Double.Parse(rulerBodyTextBox.Text), GridUnitType.Pixel);
                grid.RowDefinitions[0].Height = (rowBodyHeight.Value > 10) ? rowBodyHeight : new GridLength(10, GridUnitType.Pixel);
                grid.RowDefinitions[2].Height = (rowBodyHeight.Value > 10) ? rowBodyHeight : new GridLength(10, GridUnitType.Pixel);
            }

            // Validates the 'Width of Ruler' input field
            if (widthRulerBody.Text.Length == 0 || _regex.IsMatch(widthRulerBody.Text))
            {
                errorMessage += "Only Numbers are allowed. Fields may NOT be empty\n";
                widthRulerBody.BorderBrush = error_colour;
            } else
            {
                widthRulerBody.BorderBrush = valid_colour;
                grid.Width = (Double.Parse(widthRulerBody.Text) > 700) ? Double.Parse(widthRulerBody.Text) : 700;
            }

            // Validates the 'RGBA' input field
            if (rgbaBody.Text.Length == 0 || !_regexHexCode.IsMatch(rgbaBody.Text))
            {
                errorMessage += "RGBA must start with '#' and followed with 6 numbers (example: '#00FF00'). Fields may NOT be empty\n";
                rgbaBody.BorderBrush = error_colour;
            } else
            {
                rgbaBody.BorderBrush = valid_colour;

                // Colours rectangles with the hex value entered in rgbaBody
                var brush = (Brush)new BrushConverter().ConvertFromString(rgbaBody.Text);
                row0.Fill = brush;
                row2.Fill = brush;
                menuOptions.Fill = brush;
            }


            if (errorMessage.Length > 0)
            {
                errorLabel.Visibility = Visibility.Visible;
                errorLabel.Content = errorMessage;
                return false;
            }

            // Remove error messages and styles after validation
            errorLabel.Visibility = Visibility.Collapsed;

            return true;
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
            errorLabel.Visibility = visibility;
        }
    }
}