// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Collections.ObjectModel;
using System.Windows.Input;
using F1Predictions.Core.Config;
using F1Predictions.Core.Events;
using F1Predictions.Core.Extensions;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace F1Predictions.Core.ViewModels;

public class ProgressBarViewModel : BindableBase
{
    private readonly IEventAggregator eventAggregator;
    private readonly IProgressService progress;
    
    private ObservableCollection<SectionBar> sections;
    private SectionBar activeSection;
    private BaseBar activeQuestion;

    private const string NormalColor = "#FFF8DC";
    private const string ActiveColor = "#8B8000";
    private const string CompletedColor = "#93C572";

    public ProgressBarViewModel(PredictionConfig config, IEventAggregator eventAggregator, IProgressService progress)
    {
        this.eventAggregator = eventAggregator;
        this.progress = progress;
        var sectionsConfig = config.PredictionSections;

        ToQuestionCommand = new DelegateCommand<BaseBar>(ToQuestionAction);
        
        var sectionBars = sectionsConfig.Select((s, i) => new SectionBar
        {
            Color = NormalColor,
            ChildBars = Enumerable.Range(0, s.QuestionCount)
                .Select((_, j) => new BaseBar { Color = NormalColor, SectionIndex = i, QuestionIndex = j })
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
    
    public ICommand ToQuestionCommand { get; }
    
    
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

    private void ToQuestionAction(BaseBar bar)
    {
        progress.GoToQuestion(bar.SectionIndex, bar.QuestionIndex);
    }
}