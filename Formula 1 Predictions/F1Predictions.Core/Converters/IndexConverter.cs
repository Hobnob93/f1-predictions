using System.Globalization;
using System.Windows.Data;

namespace F1Predictions.Core.Converters;

[ValueConversion(typeof(int), typeof(int))]
public class IndexConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var val = (int)value;
        return val + 1;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var val = (int)value;
        return val - 1;
    }
}