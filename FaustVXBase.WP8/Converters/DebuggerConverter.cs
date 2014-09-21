using System;
using Windows.UI.Xaml.Data;

namespace FaustVXBase.WP8.Converters
{
    public class DebuggerConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language) => value;

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language) => value;
    }
}
