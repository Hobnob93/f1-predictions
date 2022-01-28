// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Collections.ObjectModel;
using F1Predictions.Core.Config;
using F1Predictions.Core.Events;
using F1Predictions.Core.Extensions;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Prism.Events;
using Prism.Mvvm;

namespace F1Predictions.Core.ViewModels;

public class ProgressBarViewModel : BindableBase
{
    private readonly IProgressService progressService;
    private ObservableCollection<SectionBar> sections;
    private SectionBar activeSection;
    private BaseBar activeQuestion;

    private const string NormalColor = "#FFF8DC";
    private const string ActiveColor = "#8B8000";
    private const string CompletedColor = "#93C572";

    public ProgressBarViewModel(PredictionConfig config, IEventAggregator eventAggregator, IProgressService progressService)
    {
        this.progressService = progressService;
        var sectionsConfig = config.PredictionSections;

        var sectionBars = sectionsConfig.Select(s => new SectionBar
        {
            Color = NormalColor,
            ChildBars = Enumerable.Range(0, s.QuestionCount)
                .Select(_ => new BaseBar { Color = NormalColor })
                .ToArray()
        });
        
        Sections = new ObservableCollection<SectionBar>(sectionBars);

        eventAggregator.GetEvent<QuestionChangedEvent>().Subscribe(OnQuestionChanged);
    }
    
    
    public ObservableCollection<SectionBar> Sections
    {
        get => sections;
        set => SetProperty(ref sections, value);
    }
    
    private void OnQuestionChanged(QuestionChangedData data)
    {
        if (data.IsProgression)
            activeQuestion?.Complete(CompletedColor);
        else
            activeQuestion?.Deactivate(NormalColor, CompletedColor);

        var section = sections[data.SectionIndex];
        if (section != activeSection && data.IsProgression)
            activeSection?.Complete(CompletedColor);
        else if (section != activeSection)
            activeSection?.Deactivate(NormalColor, CompletedColor);

        activeSection = section;
        activeSection.Activate(ActiveColor);
        activeQuestion = activeSection.ChildBars[data.QuestionIndex];
        activeQuestion.Activate(ActiveColor);
        
        sections.Refresh();
    }
}