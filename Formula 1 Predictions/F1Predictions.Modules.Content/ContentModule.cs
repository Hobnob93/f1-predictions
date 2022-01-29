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
        containerRegistry.RegisterForNavigation<QuestionView, QuestionViewModel>(ViewNames.QuestionView);
        
        containerRegistry.RegisterForNavigation<TopAnswerView, TopAnswerViewModel>(ViewNames.TopAnswersView);
        containerRegistry.RegisterForNavigation<TopMiscAnswerView, TopMiscAnswerViewModel>(ViewNames.TopMiscAnswersView);
        containerRegistry.RegisterForNavigation<NumericalAnswerView, NumericalAnswerViewModel>(ViewNames.NumericalAnswersView);
        containerRegistry.RegisterForNavigation<HeadToHeadAnswerView, HeadToHeadAnswerViewModel>(ViewNames.HeadToHeadAnswersView);
        containerRegistry.RegisterForNavigation<OrderedAnswerView, OrderedQuestionViewModel>(ViewNames.OrderedAnswersView);

        containerRegistry.RegisterForNavigation<CompetitorPredictionsView, CompetitorPredictionsViewModel>(ViewNames.CompetitorPredictionView);
        containerRegistry.RegisterForNavigation<ValuePredictionsView, ValuePredictionsViewModel>(ViewNames.ValuePredictionView);
        containerRegistry.RegisterForNavigation<ScoresView, ScoresViewModel>(ViewNames.ScoresView);
    }
}