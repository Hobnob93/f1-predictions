using AutoMapper;
using F1Predictions.Core.Config;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services;

public class ParticipantsManager : IParticipantsManager
{
    private readonly Participant[] participants;

    public ParticipantsManager(IMapperBase mapper, PredictionConfig predictionConfig)
    {
        participants = mapper.Map<Participant[]>(predictionConfig.Participants);
    }
    
    
    public IEnumerable<Participant> GetParticipants()
    {
        return participants;
    }

    public Participant GetParticipant(string name)
    {
        return participants.SingleOrDefault(p => p.Name == name);
    }
}