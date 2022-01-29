using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace F1Predictions.Core.Components;

public partial class ParticipantBanner : UserControl
{
    public ParticipantBanner()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty TextProperty = 
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(ParticipantBanner), new PropertyMetadata(string.Empty));
    
    public static readonly DependencyProperty DataProperty = 
        DependencyProperty.Register(nameof(Data), typeof(string), typeof(ParticipantBanner), new PropertyMetadata(string.Empty));
    
    public static readonly DependencyProperty ColorProperty = 
        DependencyProperty.Register(nameof(Color), typeof(string), typeof(ParticipantBanner), new PropertyMetadata(string.Empty));
    
    public static readonly DependencyProperty HideProperty = 
        DependencyProperty.Register(nameof(Hide), typeof(bool), typeof(ParticipantBanner), new PropertyMetadata(false));
    
    public static readonly DependencyProperty VTextProperty = 
        DependencyProperty.Register(nameof(VText), typeof(string), typeof(ParticipantBanner), new PropertyMetadata(string.Empty));
    
    public static readonly DependencyProperty VDataProperty = 
        DependencyProperty.Register(nameof(VData), typeof(string), typeof(ParticipantBanner), new PropertyMetadata(string.Empty));
    
    public static readonly DependencyProperty VColorProperty = 
        DependencyProperty.Register(nameof(VColor), typeof(string), typeof(ParticipantBanner), new PropertyMetadata(string.Empty));

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
    
    public bool Hide
    {
        get => (bool) GetValue(HideProperty);
        set => SetValue(HideProperty, value);
    }
    
    public string VText
    {
        get => (string) GetValue(VTextProperty);
        set => SetValue(VTextProperty, value);
    }
    
    public string VData
    {
        get => (string) GetValue(VDataProperty);
        set => SetValue(VDataProperty, value);
    }
    
    public string VColor
    {
        get => (string) GetValue(VColorProperty);
        set => SetValue(VColorProperty, value);
    }

    private void ParticipantBanner_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton != MouseButton.Left || !Hide)
            return;
        
        VText = Text;
        VColor = Color;
        VData = Data;
    }

    private void ParticipantBanner_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (Hide)
            return;
        
        VText = Text;
        VColor = Color;
        VData = Data;
    }
}