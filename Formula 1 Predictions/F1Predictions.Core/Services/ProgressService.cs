using F1Predictions.Core.Config;
using F1Predictions.Core.Constants;
using F1Predictions.Core.Enums;
using F1Predictions.Core.Events;
using F1Predictions.Core.Interfaces;
using Prism.Events;
using Prism.Regions;

namespace F1Predictions.Core.Services;

public class ProgressService : IProgressService
{
    private readonly IEventAggregator eventAggregator;
    private readonly IRegionManager regionManager;
    private readonly int[] numQuestionsPerSection;
    
    public ProgressService(PredictionConfig config, IEventAggregator eventAggregator, IRegionManager regionManager)
    {
        this.eventAggregator = eventAggregator;
        this.regionManager = regionManager;

        CurrentQuestionIndex = -1;
        CurrentSectionIndex = -1;
        
        numQuestionsPerSection = config.PredictionSections.Select(s => s.QuestionCount)
            .ToArray();
        
        eventAggregator.GetEvent<ProgressChangedEvent>().Subscribe(OnProgressChanged);
    }

    public int CurrentSectionIndex { get; private set; }
    public int CurrentQuestionIndex { get; private set; }

    private void OnProgressChanged(bool isForward)
    {
        if (isForward)
            Increment();

        var navParams = new NavigationParameters
        {
            {Navigation.SectionId, CurrentSectionIndex},
            {Navigation.QuestionId, CurrentQuestionIndex}
        };
        
        regionManager.RequestNavigate($"{Regions.Content}", ViewNames.TopQuestionView, navParams);
    }
    
    private void Increment()
    {
        if (CurrentQuestionIndex == -1 || CurrentSectionIndex == -1)
        {
            CurrentQuestionIndex = 0;
            CurrentSectionIndex = 0;
            
            eventAggregator.GetEvent<SectionChangedEvent>().Publish();
            eventAggregator.GetEvent<QuestionChangedEvent>().Publish();
        }
        else
        {
            CurrentQuestionIndex++;
            CurrentQuestionIndex %= numQuestionsPerSection[CurrentSectionIndex];

            if (CurrentQuestionIndex == 0)
            {
                CurrentSectionIndex++;
                CurrentSectionIndex %= numQuestionsPerSection.Length;
                eventAggregator.GetEvent<SectionChangedEvent>().Publish();

                if (CurrentSectionIndex == 0)
                    eventAggregator.GetEvent<ProgressCompleteEvent>().Publish();
            }
            
            eventAggregator.GetEvent<QuestionChangedEvent>().Publish();
        }
    }
}