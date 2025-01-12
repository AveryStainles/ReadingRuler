using System.Drawing;
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

        #region Settings Update
        // Apply changes button is pressed, get all the input fields and set the settings to match them
        private void ApplySettingsUpdate(object sender, RoutedEventArgs e)
        {
            // Ensures all input fields have appropriate error messages and correct input types
            if (!ValidateInputFields())
            {
                return;
            }

            var valid_colour = new SolidColorBrush(Colors.Transparent);

            // Update Ruler Gap 
            rulerGapTextBox.BorderBrush = valid_colour;
            grid.RowDefinitions[1].Height = GetNewGridLength(rulerGapTextBox.Text);

            // Update Ruler Body Size
            rulerBodyTextBox.BorderBrush = valid_colour;
            grid.RowDefinitions[0].Height = GetNewGridLength(rulerBodyTextBox.Text);
            grid.RowDefinitions[2].Height = GetNewGridLength(rulerBodyTextBox.Text);

            // Update Ruler Width Size
            widthRulerBody.BorderBrush = valid_colour;
            grid.Width = (Double.Parse(widthRulerBody.Text) > 700) ? Double.Parse(widthRulerBody.Text) : 700;

            // Update Ruler RGBA Size
            // https://stackoverflow.com/questions/10062376/creating-solidcolorbrush-from-hex-color-value
            byte R = Convert.ToByte(rgbaBody.Text.Substring(1, 2), 16);
            byte G = Convert.ToByte(rgbaBody.Text.Substring(3, 2), 16);
            byte B = Convert.ToByte(rgbaBody.Text.Substring(5, 2), 16);
            Brush brush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(R, G, B));
            rgbaBody.BorderBrush = valid_colour;
            row0.Fill = brush;
            row2.Fill = brush;
            menuOptions.Fill = brush;



            grid.RowDefinitions[3].Height = new GridLength(50, GridUnitType.Pixel);
            window.Height = grid.RowDefinitions[0].Height.Value + grid.RowDefinitions[1].Height.Value + grid.RowDefinitions[2].Height.Value + grid.RowDefinitions[3].Height.Value;
        }

        private readonly GridLength MinimumLength = new GridLength(10, GridUnitType.Pixel);
        private GridLength EnforceMinimumLength(GridLength length) => length.Value > MinimumLength.Value ? length : MinimumLength;
        private GridLength GetNewGridLength(string length) => EnforceMinimumLength(new GridLength(Double.Parse(length), GridUnitType.Pixel));
        private bool ValidateInputFields()
        {
            // Startup Variables
            Regex regexOnlyHexCode = new("^#[0-9a-fA-F]{6}$");
            Regex regexOnlyNumbers = new("[^0-9]+");
            var error_colour = new SolidColorBrush(Colors.Red);
            string errorMessage = "";

            // Validates the 'Ruler Gap' input field
            if (rulerGapTextBox.Text.Length == 0 || regexOnlyNumbers.IsMatch(rulerGapTextBox.Text))
            {
                errorMessage += "Only Numbers are allowed. Fields may NOT be empty\n";
                rulerGapTextBox.BorderBrush = error_colour;
            }

            // Validates the 'Ruler Body' input field
            if (rulerBodyTextBox.Text.Length == 0 || regexOnlyNumbers.IsMatch(rulerBodyTextBox.Text))
            {
                errorMessage += "Only Numbers are allowed. Fields may NOT be empty\n";
                rulerBodyTextBox.BorderBrush = error_colour;
            }

            // Validates the 'Width of Ruler' input field
            if (widthRulerBody.Text.Length == 0 || regexOnlyNumbers.IsMatch(widthRulerBody.Text))
            {
                errorMessage += "Only Numbers are allowed. Fields may NOT be empty\n";
                widthRulerBody.BorderBrush = error_colour;
            }

            // Validates the 'RGBA' input field
            if (rgbaBody.Text.Length == 0 || !regexOnlyHexCode.IsMatch(rgbaBody.Text))
            {
                errorMessage += "RGBA must start with '#' and followed with 6 numbers (example: '#00FF00'). Fields may NOT be empty\n";
                rgbaBody.BorderBrush = error_colour;
            }

            // If any errors are found run this
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
        #endregion
        private void ToggleVisibility()
        {
            var visibility = (menuOptions.Visibility != Visibility.Visible) ? Visibility.Visible : Visibility.Collapsed;

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