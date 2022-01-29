using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using System.Linq;

namespace F1Predictions.Core.Services;

public class ScoreManager : IScoreManager
{
    private readonly IParticipantsManager participants;
    private readonly Dictionary<string, List<ParticipantScore>> participantScoresMap = new();


    public ScoreManager(IParticipantsManager participants)
    {
        this.participants = participants;
    }
    
    
    public void SetScoreForQuestion(Participant participant, int sectionIndex, int questionIndex, int score)
    {
        var scoreId = QuestionIndicesToId(sectionIndex, questionIndex);

        if (participantScoresMap.ContainsKey(scoreId))
        {
            var participantScore = participantScoresMap[scoreId].FirstOrDefault(ps => ps.Participant == participant);
            if (participantScore is null)
            {
                participantScoresMap[scoreId].Add(new ParticipantScore
                {
                    Participant = participant,
                    Score = score
                });
            }
            else
            {
                participantScore.Score = score;
            }
        }
        else
        {
            participantScoresMap.Add(scoreId, new List<ParticipantScore>(new[]
            {
                new ParticipantScore
                {
                    Participant = participant,
                    Score = score
                }
            }));
        }
    }

    public int GetScoreForQuestion(Participant participant, int sectionIndex, int questionIndex)
    {
        var scoreId = QuestionIndicesToId(sectionIndex, questionIndex);

        return participantScoresMap.ContainsKey(scoreId) 
            ? participantScoresMap[scoreId].FirstOrDefault(ps => ps.Participant == participant)?.Score ?? 0
            : 0;
    }

    public int GetTotalScore(Participant participant)
    {
        return participantScoresMap
            .SelectMany(psm => psm.Value.Where(ps => ps.Participant == participant))
            .Sum(ps => ps.Score);
    }

    private string QuestionIndicesToId(int sectionIndex, int questionIndex)
    {
        return $"{sectionIndex}-{questionIndex}";
    }
}