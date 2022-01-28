using System.Globalization;
using System.Windows.Data;
using F1Predictions.Core.Extensions;

namespace F1Predictions.Core.Converters;

[ValueConversion(typeof(int), typeof(string))]
public class IndexConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var val = (int)value;
        return (val + 1).AsPosition();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var val = (int)value;
        return val - 1;
    }
}