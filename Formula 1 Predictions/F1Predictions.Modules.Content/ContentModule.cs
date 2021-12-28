using F1Predictions.Core.Constants;
using F1Predictions.Core.Enums;
using F1Predictions.Core.ViewModels;
using F1Predictions.Modules.Content.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace F1Predictions.Modules.Content;

public class ContentModule : IModule
{
    private readonly IRegionManager _regionManager;
    
    public ContentModule(IRegionManager regionManager)
    {
        _regionManager = regionManager;
    }
    
    
    public void OnInitialized(IContainerProvider containerProvider)
    {
        _regionManager.RegisterViewWithRegion<HomeView>($"{Regions.Content}");
    }
    
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>(ViewNames.HomeView);
    }
}