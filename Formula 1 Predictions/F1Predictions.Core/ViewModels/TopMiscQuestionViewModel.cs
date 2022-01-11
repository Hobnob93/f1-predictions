using System.Windows.Input;
using F1Predictions.Core.Constants;
using F1Predictions.Core.Events;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace F1Predictions.Core.ViewModels;

public class TopMiscQuestionViewModel : BindableBase, INavigationAware
{
    private readonly ISectionManager sectionManager;
    private readonly IEventAggregator eventAggregator;

    private TopMiscQuestion question;
    private int sectionId;
    private int questionId;

    public TopMiscQuestionViewModel(ISectionManager sectionManager, IEventAggregator eventAggregator)
    {
        this.sectionManager = sectionManager;
        this.eventAggregator = eventAggregator;

        PreviousCommand = new DelegateCommand(PreviousQuestionAction);
        NextCommand = new DelegateCommand(NextQuestionAction);
    }
    
    public ICommand PreviousCommand { get; }
    public ICommand NextCommand { get; }
    
    public TopMiscQuestion Question
    {
        get => question;
        set => SetProperty(ref question, value);
    }


    private void PreviousQuestionAction()
    {
        
    }

    private void NextQuestionAction()
    {
        eventAggregator.GetEvent<ProgressChangedEvent>().Publish(true);
    }
    
    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        navigationContext.Parameters.TryGetValue(Navigation.SectionId, out sectionId);
        navigationContext.Parameters.TryGetValue(Navigation.QuestionId, out questionId);

        Question = sectionManager.GetQuestion(sectionId, questionId) as TopMiscQuestion;
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        navigationContext.Parameters.TryGetValue(Navigation.SectionId, out int otherSectionId);
        navigationContext.Parameters.TryGetValue(Navigation.QuestionId, out int otherQuestionId);

        return otherSectionId == sectionId && otherQuestionId == questionId;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
        
    }
}