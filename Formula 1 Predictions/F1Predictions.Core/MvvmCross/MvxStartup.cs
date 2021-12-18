using F1Predictions.Core.ViewModels;
using MvvmCross.ViewModels;
using MvvmCross;
using MvvmCross.IoC;

namespace F1Predictions.Core.MvvmCross;

public class MvxStartup : MvxApplication
{
    private readonly IMvxIoCProvider ioc;

    public MvxStartup()
    {
        ioc = Mvx.IoCProvider;
    }
    
    
    public override void Initialize()
    {
        // TODO: Register required services here
        
        RegisterAppStart<ToolbarViewModel>();
    }
}