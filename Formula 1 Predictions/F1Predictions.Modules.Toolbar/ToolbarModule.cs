using F1Predictions.Core.Enums;
using F1Predictions.Core.ViewModels;
using F1Predictions.Modules.Toolbar.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace F1Predictions.Modules.Toolbar;

public class ToolbarModule : IModule
{
    private readonly IRegionManager _regionManager;
    
    public ToolbarModule(IRegionManager regionManager)
    {
        _regionManager = regionManager;
    }
    
    
    public void OnInitialized(IContainerProvider containerProvider)
    {
        _regionManager.RegisterViewWithRegion<ToolbarView>($"{Regions.Toolbar}");
    }
    
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        ViewModelLocationProvider.Register<ToolbarView, ToolbarViewModel>();
    }
}