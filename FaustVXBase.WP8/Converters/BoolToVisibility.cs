using Windows.UI.Xaml;

namespace FaustVXBase.WP8.Converters
{
    public class BoolToVisibility : ValueConverter<bool, Visibility>
    {
        public static BoolToVisibility Converter { get; } = new BoolToVisibility();

        public override Visibility Convert(bool value, object parameter, string language) => (value) ? Visibility.Visible : Visibility.Collapsed;
        
        public override bool ConvertBack(Visibility value, object parameter, string language) => value == Visibility.Visible;
    }
}
