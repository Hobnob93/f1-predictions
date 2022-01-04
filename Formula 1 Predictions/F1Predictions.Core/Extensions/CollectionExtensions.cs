using System.Collections.ObjectModel;
using System.Windows.Data;

namespace F1Predictions.Core.Extensions;

public static class CollectionExtensions
{
    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
    {
        return new ObservableCollection<T>(enumerable);
    }
    
    public static void Refresh<T>(this ObservableCollection<T> value)
    {
        CollectionViewSource.GetDefaultView(value).Refresh();
    }
}