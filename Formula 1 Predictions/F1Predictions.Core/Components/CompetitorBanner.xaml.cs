using System.Windows;
using System.Windows.Controls;

namespace F1Predictions.Core.Components;

public partial class CompetitorBanner : UserControl
{
    public CompetitorBanner()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty TextProperty = 
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(CompetitorBanner), new PropertyMetadata(string.Empty));
    
    public static readonly DependencyProperty DataProperty = 
        DependencyProperty.Register(nameof(Data), typeof(string), typeof(CompetitorBanner), new PropertyMetadata(string.Empty));
    
    public static readonly DependencyProperty ColorProperty = 
        DependencyProperty.Register(nameof(Color), typeof(string), typeof(CompetitorBanner), new PropertyMetadata(string.Empty));

    public string Text
    {
        get => (string) GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    public string Data
    {
        get => (string) GetValue(DataProperty);
        set => SetValue(DataProperty, value);
    }
    
    public string Color
    {
        get => (string) GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }
}