using AutoMapper;
using F1Predictions.Core.Config;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services;

public class ParticipantsManager : IParticipantsManager
{
    private readonly Participant[] participants;

    public ParticipantsManager(IMapper mapper, PredictionConfig predictionConfig)
    {
        participants = mapper.Map<IEnumerable<Participant>>(predictionConfig.Participants).ToArray();
    }
    
    
    public IEnumerable<Participant> GetParticipants()
    {
        return participants;
    }
}