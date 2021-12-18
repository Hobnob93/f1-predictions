using F1Predictions.Core.ViewModels;
using MvvmCross.ViewModels;

namespace F1Predictions.Core.Mvx;

public class MvxStartup : MvxApplication
{
    public override void Initialize()
    {
        RegisterAppStart<ToolbarViewModel>();
    }
}