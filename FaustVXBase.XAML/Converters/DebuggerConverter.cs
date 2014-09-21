using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace FaustVXBase.XAML.Converters
{
    public class DebuggerConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            Debugger.Break();
            return value;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            Debugger.Break();
            return value;
        }
    }
}
