using System.Windows;

namespace F1Predictions.Core.Extensions;

public static class EnumExtensions
{
    public static Visibility FlipCollapsed(this Visibility visibility)
    {
        return visibility switch
        {
            Visibility.Visible => Visibility.Collapsed,
            Visibility.Collapsed => Visibility.Visible,
            _ => visibility
        };
    }
}