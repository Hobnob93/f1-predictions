using AutoMapper;
using F1Predictions.Core.Dtos;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Google.Apis.Http;

namespace F1Predictions.Core.AutoMapper;

public class AnswerDtoToNumericalAnswerTypeConverter : ITypeConverter<AnswerFetchDto,Answer<int>>
{
    public Answer<int> Convert(AnswerFetchDto source, Answer<int> destination, ResolutionContext context)
    {
        var answerAsString = source.Answers.FirstOrDefault(string.Empty);
        if (!int.TryParse(answerAsString, out var answer))
            answer = 0;

        return new Answer<int>
        {
            Value = answer
        };
    }
}