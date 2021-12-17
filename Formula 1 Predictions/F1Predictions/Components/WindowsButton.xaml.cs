using System.Windows;
using System.Windows.Controls;

namespace F1Predictions.Components;

public partial class WindowsButton : UserControl
{
    public WindowsButton()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty TextProperty = 
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(WindowsButton), new PropertyMetadata(string.Empty));

    public string Text
    {
        get => (string) GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}