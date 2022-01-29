using F1Predictions.Core.Models;

namespace F1Predictions.Core.Interfaces;

public interface IScoreManager
{
    void SetScoreForQuestion(Participant participant, int sectionIndex, int questionIndex, int score);
    int GetScoreForQuestion(Participant participant, int sectionIndex, int questionIndex);
    int GetTotalScore(Participant participant);
}