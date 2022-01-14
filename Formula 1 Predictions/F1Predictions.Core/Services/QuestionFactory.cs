using AutoMapper;
using F1Predictions.Core.Dtos;
using F1Predictions.Core.Enums;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services;

public class QuestionFactory : IQuestionFactory
{
    private readonly ISectionManager sectionManager;
    private readonly IPredictionStore predictions;
    private readonly IAnswerStore answers;
    private readonly IMapper mapper;

    public QuestionFactory(ISectionManager sectionManager, IPredictionStore predictions, IAnswerStore answers, IMapper mapper)
    {
        this.sectionManager = sectionManager;
        this.predictions = predictions;
        this.answers = answers;
        this.mapper = mapper;
    }
    
    
    public BaseQuestion GetQuestion(int sectionIndex, int questionIndex)
    {
        var scoringType = sectionManager.GetCurrentQuestionScoringType();
        var predictionDto = predictions.FetchPrediction(sectionIndex, questionIndex);
        var answerDto = answers.FetchAnswer(sectionIndex, questionIndex);
        
        var question = (BaseQuestion) (scoringType switch
        {
            ScoringTypes.Top => CreateTopQuestion(predictionDto, answerDto),
            ScoringTypes.Numerical => CreateNumericalQuestion(predictionDto, answerDto),
            ScoringTypes.HeadToHead => CreateHeadToHeadQuestion(predictionDto, answerDto),
            ScoringTypes.TopMisc => CreateTopMiscQuestion(predictionDto, answerDto),
            _ => null
        });

        return question;
    }

    private TopQuestion CreateTopQuestion(PredictionFetchDto prediction, AnswerFetchDto answer)
    {
        return new TopQuestion
        {
            Name = prediction.Question,
            Description = prediction.Note,
            Predictions = mapper.Map<Prediction<ICompetitor>[]>(prediction),
            Answers = mapper.Map<Answer<ICompetitor>[]>(answer),
            Scoring = answer.Scoring
        };
    }
    
    private NumericalQuestion CreateNumericalQuestion(PredictionFetchDto prediction, AnswerFetchDto answer)
    {
        return new NumericalQuestion
        {
            Name = prediction.Question,
            Description = prediction.Note,
            Predictions = mapper.Map<Prediction<int>[]>(prediction),
            Answer = mapper.Map<Answer<int>>(answer),
            Scoring = answer.Scoring
        };
    }
    
    private HeadToHeadQuestion CreateHeadToHeadQuestion(PredictionFetchDto prediction, AnswerFetchDto answer)
    {
        var competitors = mapper.Map<Answer<ICompetitor>[]>(answer)
            .Where(c => c is not null)
            .Select(c => new HeadToHead
            {
                Name = c.Value.Name,
                Color = c.Value.Color,
                Data = int.Parse(c.Data)
            })
            .ToList();
        
        return new HeadToHeadQuestion
        {
            Name = prediction.Question,
            Description = prediction.Note,
            Predictions = mapper.Map<Prediction<ICompetitor>[]>(prediction),
            First = competitors.First(),
            Second = competitors.Last(),
            Scoring = answer.Scoring
        };
    }
    
    private TopMiscQuestion CreateTopMiscQuestion(PredictionFetchDto prediction, AnswerFetchDto answer)
    {
        return new TopMiscQuestion
        {
            Name = prediction.Question,
            Description = prediction.Note,
            Predictions = mapper.Map<Prediction<string>[]>(prediction),
            Answers = mapper.Map<Answer<string>[]>(answer),
            Scoring = answer.Scoring
        };
    }
}