using F1Predictions.Core.ViewModels;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace F1Predictions.Views;

[MvxContentPresentation]
[MvxViewFor(typeof(ToolbarViewModel))]
public partial class ToolbarView : MvxWpfView
{
    public ToolbarView()
    {
        InitializeComponent();
    }
}