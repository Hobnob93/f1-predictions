using System.Windows;
using System.Windows.Controls;

namespace F1Predictions.Core.Components;

public partial class ToolbarButton : UserControl
{
    public ToolbarButton()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty TextProperty = 
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(ToolbarButton), new PropertyMetadata(string.Empty));

    public string Text
    {
        get => (string) GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}