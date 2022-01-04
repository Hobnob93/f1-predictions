using F1Predictions.Core.Constants;
using F1Predictions.Core.Enums;
using F1Predictions.Core.ViewModels;
using F1Predictions.Modules.Progress.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace F1Predictions.Modules.Progress;

public class ProgressModule : IModule
{
    private readonly IRegionManager _regionManager;
    
    public ProgressModule(IRegionManager regionManager)
    {
        _regionManager = regionManager;
    }
    
    
    public void OnInitialized(IContainerProvider containerProvider)
    {
        _regionManager.RegisterViewWithRegion<MessageView>($"{Regions.Progress}");
    }
    
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<MessageView, MessageViewModel>(ViewNames.MessageView);
        containerRegistry.RegisterForNavigation<ProgressBarView, ProgressBarViewModel>(ViewNames.ProgressBarView);
    }
}