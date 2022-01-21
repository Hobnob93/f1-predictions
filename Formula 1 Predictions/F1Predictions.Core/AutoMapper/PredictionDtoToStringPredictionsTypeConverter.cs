using AutoMapper;
using F1Predictions.Core.Dtos;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.AutoMapper;

public class PredictionDtoToStringPredictionsTypeConverter : ITypeConverter<PredictionFetchDto,Prediction<string>[]>
{
    private readonly IParticipantsManager participantsManager;

    public PredictionDtoToStringPredictionsTypeConverter(IParticipantsManager participantsManager)
    {
        this.participantsManager = participantsManager;
    }
    
    
    public Prediction<string>[] Convert(PredictionFetchDto source, Prediction<string>[] destination, ResolutionContext context)
    {
        return source.Predictions.Select((p, i) => new Prediction<string>
            {
                Participant = participantsManager.GetParticipantByIndex(i),
                Value = p
            }).ToArray();
    }
}