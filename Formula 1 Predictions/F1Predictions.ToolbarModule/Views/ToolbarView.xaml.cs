using System.Windows.Controls;
using F1Predictions.Core.Models;

namespace F1Predictions.ToolbarModule.Views;

public partial class ToolbarView : UserControl
{
    public ToolbarView()
    {
        InitializeComponent();
        
        DataContext = this;
    }

    public Toolbar Model => new Toolbar();
}