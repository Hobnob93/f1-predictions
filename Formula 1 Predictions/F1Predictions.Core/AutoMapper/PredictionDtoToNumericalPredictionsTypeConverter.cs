using AutoMapper;
using F1Predictions.Core.Dtos;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.AutoMapper;

public class PredictionDtoToNumericalPredictionsTypeConverter : ITypeConverter<PredictionFetchDto,Prediction<int>[]>
{
    private readonly IParticipantsManager participantsManager;

    public PredictionDtoToNumericalPredictionsTypeConverter(IParticipantsManager participantsManager)
    {
        this.participantsManager = participantsManager;
    }
    
    
    public Prediction<int>[] Convert(PredictionFetchDto source, Prediction<int>[] destination, ResolutionContext context)
    {
        return source.Predictions.Select((p, i) => new
            {
                Participant = participantsManager.GetParticipantByIndex(i),
                Value = int.Parse(p)
            })
            .Select(p => new Prediction<int>
            {
                Participant = p.Participant,
                Value = p.Value
            }).ToArray();
    }
}