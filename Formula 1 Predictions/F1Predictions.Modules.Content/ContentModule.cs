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
    private readonly IRegionManager regionManager;
    
    public ContentModule(IRegionManager regionManager)
    {
        this.regionManager = regionManager;
    }
    
    
    public void OnInitialized(IContainerProvider containerProvider)
    {
        regionManager.RegisterViewWithRegion<HomeView>($"{Regions.Content}");
    }
    
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>(ViewNames.HomeView);
        containerRegistry.RegisterForNavigation<TopQuestionView, TopQuestionViewModel>(ViewNames.TopQuestionView);
        containerRegistry.RegisterForNavigation<TopMiscQuestionView, TopMiscQuestionViewModel>(ViewNames.TopMiscQuestionView);
        containerRegistry.RegisterForNavigation<NumericalQuestionView, NumericalQuestionViewModel>(ViewNames.NumericalQuestionView);
        containerRegistry.RegisterForNavigation<HeadToHeadQuestionView, HeadToHeadQuestionViewModel>(ViewNames.HeadToHeadQuestionView);
    }
}