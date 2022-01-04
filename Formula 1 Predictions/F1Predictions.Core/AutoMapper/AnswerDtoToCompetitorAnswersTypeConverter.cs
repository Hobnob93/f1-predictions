using AutoMapper;
using F1Predictions.Core.Dtos;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Google.Apis.Http;

namespace F1Predictions.Core.AutoMapper;

public class AnswerDtoToCompetitorAnswersTypeConverter : ITypeConverter<AnswerFetchDto,Answer<ICompetitor>[]>
{
    private readonly ICompetitorManager competitorManager;

    public AnswerDtoToCompetitorAnswersTypeConverter(ICompetitorManager competitorManager)
    {
        this.competitorManager = competitorManager;
    }
    
    
    public Answer<ICompetitor>[] Convert(AnswerFetchDto source, Answer<ICompetitor>[] destination, ResolutionContext context)
    {
        var answers = new List<Answer<ICompetitor>>();

        for (var i = 0; i < source.Answers.Length; i++)
        {
            var answer = source.Answers[i];
            var note = source.Notes[i];

            if (string.IsNullOrWhiteSpace(answer))
                continue;
            
            answers.Add(new Answer<ICompetitor>
            {
                Value = competitorManager.GetCompetitorByName(answer),
                Data = note
            });
        }
        
        return answers.Where(a => a.Value is not null).ToArray();
    }
}