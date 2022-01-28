using System.Globalization;
using System.Windows.Data;
using F1Predictions.Core.Extensions;

namespace F1Predictions.Core.Converters;

[ValueConversion(typeof(int), typeof(string))]
public class PositionConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var position = value as int? ?? 0;

        return position.AsPosition();
    }

    public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}