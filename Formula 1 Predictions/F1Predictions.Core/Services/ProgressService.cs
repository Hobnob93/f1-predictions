using AutoMapper;
using F1Predictions.Core.Config;
using F1Predictions.Core.Constants;
using F1Predictions.Core.Enums;
using F1Predictions.Core.Events;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Prism.Events;
using Prism.Regions;

namespace F1Predictions.Core.Services;

public class ProgressService : IProgressService
{
    private readonly IEventAggregator eventAggregator;
    private readonly IRegionManager regionManager;
    private readonly Section[] sections;
    
    public ProgressService(PredictionConfig config, IEventAggregator eventAggregator, IRegionManager regionManager, IMapper mapper)
    {
        this.eventAggregator = eventAggregator;
        this.regionManager = regionManager;

        CurrentQuestionIndex = -1;
        CurrentSectionIndex = -1;
        
        sections = config.PredictionSections.Select(mapper.Map<Section>)
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
        
        regionManager.RequestNavigate($"{Regions.Content}", ViewFromQuestionType(), navParams);
    }

    private string ViewFromQuestionType()
    {
        var section = sections[CurrentSectionIndex];
        var overrideData = section.ScoringOverrides.FirstOrDefault(so => so.QuestionIndex == CurrentQuestionIndex);
        var scoringType = overrideData?.ScoringType ?? section.ScoringType;

        return scoringType switch
        {
            ScoringTypes.Top => ViewNames.TopQuestionView,
            ScoringTypes.Numerical => ViewNames.NumericalQuestionView,
            ScoringTypes.HeadToHead => ViewNames.HeadToHeadQuestionView,
            _ => ViewNames.HomeView
        };
    }
    
    private void Increment()
    {
        if (CurrentQuestionIndex == -1 || CurrentSectionIndex == -1)
        {
            CurrentQuestionIndex = 0;
            CurrentSectionIndex = 3;
            
            eventAggregator.GetEvent<SectionChangedEvent>().Publish();
            eventAggregator.GetEvent<QuestionChangedEvent>().Publish();
        }
        else
        {
            CurrentQuestionIndex++;
            CurrentQuestionIndex %= sections[CurrentSectionIndex].QuestionCount;

            if (CurrentQuestionIndex == 0)
            {
                CurrentSectionIndex++;
                CurrentSectionIndex %= sections.Length;
                eventAggregator.GetEvent<SectionChangedEvent>().Publish();

                if (CurrentSectionIndex == 0)
                    eventAggregator.GetEvent<ProgressCompleteEvent>().Publish();
            }
            
            eventAggregator.GetEvent<QuestionChangedEvent>().Publish();
        }
    }
}