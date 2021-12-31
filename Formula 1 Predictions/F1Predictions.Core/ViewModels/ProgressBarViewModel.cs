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
        if (activeSection != null && activeQuestion != null)
        {
            activeQuestionId++;
            activeQuestionId %= activeSection.ChildBars.Length;
            activeQuestion.Complete(CompletedColor);

            if (activeQuestionId == 0)
            {
                activeSectionId++;
                activeSectionId %= Sections.Count;
                activeSection.Complete(CompletedColor);

                if (activeSectionId == 0)
                    return;
            }
        }
        
        UpdateActiveSection();
        UpdateActiveQuestion();
        sections.Refresh();
    }

    private void UpdateActiveSection()
    {
        activeSection = sections[activeSectionId];
        activeSection.Color = ActiveColor;
    }

    private void UpdateActiveQuestion()
    {
        activeQuestion = activeSection.ChildBars[activeQuestionId];
        activeQuestion.Color = ActiveColor;
    }
}