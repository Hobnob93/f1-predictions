using F1Predictions.Core.Enums;

namespace F1Predictions.Core.Config;

public record ScoreOverrideConfig
{
    public string Question { get; set; }
    public ScoringTypes ScoringType { get; set; }
}