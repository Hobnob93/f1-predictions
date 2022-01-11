using AutoMapper;
using F1Predictions.Core.Dtos;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.AutoMapper;

public class AnswerDtoToStringAnswersTypeConverter : ITypeConverter<AnswerFetchDto,Answer<string>[]>
{
    public Answer<string>[] Convert(AnswerFetchDto source, Answer<string>[] destination, ResolutionContext context)
    {
        return source.Answers.Select(a => new Answer<string>
        {
            Value = a
        }).ToArray();
    }
}