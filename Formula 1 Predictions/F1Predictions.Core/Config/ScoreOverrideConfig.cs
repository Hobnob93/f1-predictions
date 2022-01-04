using F1Predictions.Core.Enums;

namespace F1Predictions.Core.Config;

public record ScoreOverrideConfig
{
    public int QuestionIndex { get; set; }
    public ScoringTypes ScoringType { get; set; }
}