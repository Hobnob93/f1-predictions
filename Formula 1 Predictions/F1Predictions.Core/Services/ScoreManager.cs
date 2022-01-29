using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Google.Apis.Sheets.v4.Data;

namespace F1Predictions.Core.Services;

public class ScoreManager : IScoreManager
{
    private readonly IGoogleSheets sheets;
    private readonly Dictionary<string, List<ParticipantScore>> participantScoresMap = new();

    private bool dirty;
    
    
    public ScoreManager(IGoogleSheets sheets)
    {
        this.sheets = sheets;
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
                    Score = score,
                    ForSection = sectionIndex,
                    ForQuestion = questionIndex
                });

                dirty = true;
            }
            else
            {
                if (participantScore.Score != score)
                    dirty = true;
                
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
                    Score = score,
                    ForSection = sectionIndex,
                    ForQuestion = questionIndex
                }
            }));

            dirty = true;
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

    public void SaveIfDirty(int sectionIndex, int questionIndex)
    {
        if (dirty)
        {
            var scoreId = QuestionIndicesToId(sectionIndex, questionIndex);
            var participantScores = participantScoresMap[scoreId];
            sheets.SaveScores(participantScores);
        }

        dirty = false;
    }

    private string QuestionIndicesToId(int sectionIndex, int questionIndex)
    {
        return $"{sectionIndex}-{questionIndex}";
    }
}