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

public class TopMiscAnswerViewModel : BindableBase, INavigationAware
{
    private readonly IQuestionFactory questions;

    private TopMiscQuestion question;
    private int sectionId;
    private int questionId;

    public TopMiscAnswerViewModel(IQuestionFactory questions)
    {
        this.questions = questions;
    }
    
    public TopMiscQuestion Question
    {
        get => question;
        set => SetProperty(ref question, value);
    }

    
    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        navigationContext.Parameters.TryGetValue(Navigation.SectionId, out sectionId);
        navigationContext.Parameters.TryGetValue(Navigation.QuestionId, out questionId);

        Question = questions.GetQuestion(sectionId, questionId) as TopMiscQuestion;
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