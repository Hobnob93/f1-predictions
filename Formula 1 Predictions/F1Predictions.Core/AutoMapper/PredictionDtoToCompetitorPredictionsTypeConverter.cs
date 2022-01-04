using AutoMapper;
using F1Predictions.Core.Dtos;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.AutoMapper;

public class PredictionDtoToCompetitorPredictionsTypeConverter : ITypeConverter<PredictionFetchDto,Prediction<ICompetitor>[]>
{
    private readonly IParticipantsManager participantsManager;
    private readonly ICompetitorManager competitorManager;

    public PredictionDtoToCompetitorPredictionsTypeConverter(IParticipantsManager participantsManager, ICompetitorManager competitorManager)
    {
        this.participantsManager = participantsManager;
        this.competitorManager = competitorManager;
    }
    
    
    public Prediction<ICompetitor>[] Convert(PredictionFetchDto source, Prediction<ICompetitor>[] destination, ResolutionContext context)
    {
        return source.Predictions.Select((p, i) => new
            {
                Participant = participantsManager.GetParticipantByIndex(i),
                Competitor = competitorManager.GetCompetitorByName(p)
            })
            .Select(p => new Prediction<ICompetitor>
            {
                Participant = p.Participant,
                Value = p.Competitor
            }).ToArray();
    }
}