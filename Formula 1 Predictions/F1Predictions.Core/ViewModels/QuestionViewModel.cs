using System.Net;
using System.Windows.Input;
using F1Predictions.Core.Constants;
using F1Predictions.Core.Enums;
using F1Predictions.Core.Events;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace F1Predictions.Core.ViewModels;

public class QuestionViewModel : BindableBase
{
    private readonly IQuestionFactory questions;
    private readonly IEventAggregator eventAggregator;
    private readonly IRegionManager regionManager;
    private readonly ISectionManager sections;

    private BaseQuestion question;
    
    public QuestionViewModel(IQuestionFactory questions, IEventAggregator eventAggregator, IRegionManager regionManager, ISectionManager sections)
    {
        this.questions = questions;
        this.eventAggregator = eventAggregator;
        this.regionManager = regionManager;
        this.sections = sections;

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
        
        var navParams = new NavigationParameters
        {
            {Navigation.SectionId, data.SectionIndex},
            {Navigation.QuestionId, data.QuestionIndex}
        };
        
        regionManager.RequestNavigate($"{Regions.Predictions}", PredictionsViewFromQuestionType(data.QuestionIndex), navParams);
        regionManager.RequestNavigate($"{Regions.Answers}", AnswersViewFromQuestionType(data.QuestionIndex), navParams);
        regionManager.RequestNavigate($"{Regions.Points}", ViewNames.ScoresView, navParams);
    }

    private ScoringTypes GetScoringType(int questionIndex)
    {
        var section = sections.GetCurrentSection();
        var overrideData = section.ScoringOverrides.FirstOrDefault(so => so.QuestionIndex == questionIndex);
        return overrideData?.ScoringType ?? section.ScoringType;
    }
    
    private string AnswersViewFromQuestionType(int questionIndex)
    {
        var scoringType = GetScoringType(questionIndex);

        return scoringType switch
        {
            ScoringTypes.Top => ViewNames.TopAnswersView,
            ScoringTypes.Numerical => ViewNames.NumericalAnswersView,
            ScoringTypes.HeadToHead => ViewNames.HeadToHeadAnswersView,
            ScoringTypes.TopMisc => ViewNames.TopMiscAnswersView,
            ScoringTypes.FullOrder => ViewNames.OrderedAnswersView,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    private string PredictionsViewFromQuestionType(int questionIndex)
    {
        var scoringType = GetScoringType(questionIndex);

        return scoringType switch
        {
            ScoringTypes.Numerical => ViewNames.ValuePredictionView,
            ScoringTypes.TopMisc => ViewNames.ValuePredictionView,
            _ => ViewNames.CompetitorPredictionView
        };
    }
}