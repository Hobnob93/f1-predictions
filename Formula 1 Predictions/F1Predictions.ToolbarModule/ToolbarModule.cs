using F1Predictions.Core.Enums;
using F1Predictions.Core.ViewModels;
using F1Predictions.ToolbarModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace F1Predictions.ToolbarModule;

public class ToolbarModule : IModule
{
    private readonly IRegionManager regionManager;
    
    public ToolbarModule(IRegionManager regionManager)
    {
        this.regionManager = regionManager;
    }
    
    
    public void OnInitialized(IContainerProvider containerProvider)
    {
        regionManager.RegisterViewWithRegion<ToolbarView>($"{Regions.ToolbarRegion}");
    }
    
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        ViewModelLocationProvider.Register<ToolbarView, ToolbarViewModel>();
    }
}