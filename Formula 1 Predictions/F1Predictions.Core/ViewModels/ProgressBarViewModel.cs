// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Collections.ObjectModel;
using F1Predictions.Core.Config;
using F1Predictions.Core.Events;
using F1Predictions.Core.Extensions;
using F1Predictions.Core.Models;
using Prism.Events;
using Prism.Mvvm;

namespace F1Predictions.Core.ViewModels;

public class ProgressBarViewModel : BindableBase
{
    private ObservableCollection<SectionBar> sections;
    private SectionBar activeSection;
    private BaseBar activeQuestion;
    private int activeSectionId;
    private int activeQuestionId;

    private const string NormalColor = "#FFF8DC";
    private const string ActiveColor = "#8B8000";
    private const string CompletedColor = "#93C572";

    public ProgressBarViewModel(PredictionConfig config, IEventAggregator eventAggregator)
    {
        var sectionsConfig = config.PredictionSections;

        var sectionBars = sectionsConfig.Select(s => new SectionBar
        {
            Color = NormalColor,
            ChildBars = Enumerable.Range(0, s.QuestionCount)
                .Select(_ => new BaseBar { Color = NormalColor })
                .ToArray()
        });
        
        Sections = new ObservableCollection<SectionBar>(sectionBars);

        eventAggregator.GetEvent<SectionChangedEvent>().Subscribe(OnSectionChanged);
    }
    
    
    public ObservableCollection<SectionBar> Sections
    {
        get => sections;
        set => SetProperty(ref sections, value);
    }

    private void OnSectionChanged(bool isForward)
    {
        if (activeSection == null || activeQuestion == null)
        {
            activeSection = sections[activeSectionId];
            activeQuestion = activeSection.ChildBars[activeQuestionId];
        }
        else
        {
            activeQuestionId++;

            if (activeQuestionId >= activeSection.ChildBars.Length)
            {
                activeQuestionId = 0;
                activeSectionId++;
                activeSection.Color = CompletedColor;
                activeSection.IsComplete = true;

                if (activeSectionId >= Sections.Count)
                {
                    // We are done
                    return;
                }

                activeSection = sections[activeSectionId];
            }

            activeQuestion.Color = CompletedColor;
            activeQuestion.IsComplete = true;
            activeQuestion = activeSection.ChildBars[activeQuestionId];
        }
        
        activeSection.Color = ActiveColor;
        activeQuestion.Color = ActiveColor;
        
        sections.Refresh();
    }
}