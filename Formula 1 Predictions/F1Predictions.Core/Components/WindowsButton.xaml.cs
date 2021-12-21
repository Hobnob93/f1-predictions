using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Prism.Commands;

namespace F1Predictions.Core.Components;

public partial class WindowsButton : UserControl
{
    public WindowsButton()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty TextProperty = 
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(WindowsButton));
    
    public static readonly DependencyProperty OnClickProperty = 
        DependencyProperty.Register(nameof(OnClick), typeof(ICommand), typeof(WindowsButton));

    public string Text
    {
        get => (string) GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public ICommand OnClick
    {
        get => (ICommand) GetValue(OnClickProperty);
        set => SetValue(OnClickProperty, value);
    }
}