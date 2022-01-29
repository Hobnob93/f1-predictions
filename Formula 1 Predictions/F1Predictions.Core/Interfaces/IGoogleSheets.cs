using F1Predictions.Core.Models;

namespace F1Predictions.Core.Interfaces;

public interface IGoogleSheets
{
    IEnumerable<IEnumerable<object>> FetchAllPredictions();
    IEnumerable<IEnumerable<object>> FetchAllAnswers();
    void SaveScores(List<ParticipantScore> participantScores);
}