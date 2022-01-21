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

public class OrderedQuestionViewModel : BindableBase, INavigationAware
{
    private readonly IQuestionFactory questions;
    private readonly IEventAggregator eventAggregator;

    private OrderedQuestion question;
    private int sectionId;
    private int questionId;

    public OrderedQuestionViewModel(IQuestionFactory questions, IEventAggregator eventAggregator)
    {
        this.questions = questions;
        this.eventAggregator = eventAggregator;

        PreviousCommand = new DelegateCommand(PreviousQuestionAction);
        NextCommand = new DelegateCommand(NextQuestionAction);
    }
    
    public ICommand PreviousCommand { get; }
    public ICommand NextCommand { get; }
    
    public OrderedQuestion Question
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

        Question = questions.GetQuestion(sectionId, questionId) as OrderedQuestion;
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