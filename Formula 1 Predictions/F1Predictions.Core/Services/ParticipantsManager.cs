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

    public Participant GetParticipantByIndex(int index)
    {
        return participants.Single(p => p.Index == index);
    }

    public void SetScore(Participant participant, int sectionIndex, int questionIndex, int score)
    {
        var scoreId = QuestionIndicesToId(sectionIndex, questionIndex);

        if (participant.Scores.ContainsKey(scoreId))
            participant.Scores[scoreId] = score;
        else
            participant.Scores.Add(scoreId, score);
    }

    public int GetScore(Participant participant, int sectionIndex, int questionIndex)
    {
        var scoreId = QuestionIndicesToId(sectionIndex, questionIndex);

        return participant.Scores.ContainsKey(scoreId) 
            ? participant.Scores[scoreId]
            : 0;
    }

    private string QuestionIndicesToId(int sectionIndex, int questionIndex)
    {
        return $"{sectionIndex}-{questionIndex}";
    }
}