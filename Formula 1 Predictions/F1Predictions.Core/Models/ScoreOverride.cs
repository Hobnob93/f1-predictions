using F1Predictions.Core.Enums;

namespace F1Predictions.Core.Models;

public record ScoreOverride
{
    public int QuestionIndex { get; set; }
    public ScoringTypes ScoringType { get; set; }
}