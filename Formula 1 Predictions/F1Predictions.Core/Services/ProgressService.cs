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
    
    public void GoToQuestion(int sectionIndex, int questionIndex)
    {
        CurrentQuestionIndex = questionIndex;
        CurrentSectionIndex = sectionIndex;
        
        eventAggregator.GetEvent<QuestionChangedEvent>().Publish(new QuestionChangedData
        {
            IsProgression = false,
            SectionIndex = CurrentSectionIndex,
            QuestionIndex = CurrentQuestionIndex
        });
    }

    private void OnProgressChanged(bool isForward)
    {
        if (isForward)
            Increment();
        else
            Decrement();
    }

    private void Started()
    {
        regionManager.RequestNavigate($"{Regions.Content}", ViewNames.QuestionView);
    }

    private void BackToMain()
    {
        regionManager.RequestNavigate($"{Regions.Content}", ViewNames.HomeView);
        regionManager.RequestNavigate($"{Regions.Progress}", ViewNames.MessageView);
    }

    private void Completed()
    {
        regionManager.RequestNavigate($"{Regions.Content}", ViewNames.TotalScoresView);
        foreach (var view in regionManager.Regions[$"{Regions.Progress}"].Views)
        {
            regionManager.Regions[$"{Regions.Progress}"].Remove(view);
        }
    }
    
    private void Increment()
    {
        if (CurrentQuestionIndex == -1 || CurrentSectionIndex == -1)
        {
            Started();
            
            CurrentQuestionIndex = 0;
            CurrentSectionIndex = 0;
            
            PublishQuestionChangedEvent(true);
        }
        else
        {
            CurrentQuestionIndex++;
            CurrentQuestionIndex %= sections[CurrentSectionIndex].QuestionCount;

            if (CurrentQuestionIndex == 0)
            {
                CurrentSectionIndex++;
                CurrentSectionIndex %= sections.Length;

                if (CurrentSectionIndex == 0)
                {
                    eventAggregator.GetEvent<ProgressCompleteEvent>().Publish();
                    Completed();
                    return;
                }
            }
            
            PublishQuestionChangedEvent(true);
        }
    }
    
    private void Decrement()
    {
        if (CurrentQuestionIndex == -1 || CurrentSectionIndex == -1)
        {
            Started();
            
            CurrentQuestionIndex = 0;
            CurrentSectionIndex = 0;

            PublishQuestionChangedEvent(false);
        }
        else
        {
            CurrentQuestionIndex--;

            if (CurrentQuestionIndex == -1)
            {
                CurrentSectionIndex--;

                if (CurrentSectionIndex == -1)
                {
                    eventAggregator.GetEvent<ProgressResetEvent>().Publish();
                    BackToMain();
                    return;
                }
                    
                CurrentQuestionIndex += sections[CurrentSectionIndex].QuestionCount;
            }
            
            PublishQuestionChangedEvent(false);
        }
    }

    private void PublishQuestionChangedEvent(bool isForward)
    {
        eventAggregator.GetEvent<QuestionChangedEvent>().Publish(new QuestionChangedData
        {
            IsProgression = isForward,
            SectionIndex = CurrentSectionIndex,
            QuestionIndex = CurrentQuestionIndex
        });
    }
}