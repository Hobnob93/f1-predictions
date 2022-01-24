using System.Net;
using System.Windows.Input;
using F1Predictions.Core.Enums;
using F1Predictions.Core.Events;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace F1Predictions.Core.ViewModels;

public class QuestionViewModel : BindableBase
{
    private readonly IQuestionFactory questions;
    private readonly IEventAggregator eventAggregator;
    private readonly IProgressService progress;

    private BaseQuestion question;
    
    public QuestionViewModel(IQuestionFactory questions, IEventAggregator eventAggregator, IProgressService progress)
    {
        this.questions = questions;
        this.eventAggregator = eventAggregator;
        this.progress = progress;

        PreviousCommand = new DelegateCommand(PreviousQuestionAction);
        NextCommand = new DelegateCommand(NextQuestionAction);
        
        eventAggregator.GetEvent<QuestionChangedEvent>().Subscribe(UpdateQuestion);
    }
    
    public ICommand PreviousCommand { get; }
    public ICommand NextCommand { get; }
    
    public BaseQuestion Question
    {
        get => question;
        set => SetProperty(ref question, value);
    }
    
    public string PredictionsRegionName => Regions.Predictions.ToString();
    public string AnswersRegionName => Regions.Answers.ToString();
    public string PointsRegionName => Regions.Points.ToString();
    
    private void PreviousQuestionAction()
    {
        eventAggregator.GetEvent<ProgressChangedEvent>().Publish(false);
    }

    private void NextQuestionAction()
    {
        eventAggregator.GetEvent<ProgressChangedEvent>().Publish(true);
    }

    private void UpdateQuestion(QuestionChangedData data)
    {
        Question = questions.GetQuestion(data.SectionIndex, data.QuestionIndex);
    }
}