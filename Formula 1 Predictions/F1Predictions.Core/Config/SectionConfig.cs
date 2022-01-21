using F1Predictions.Core.Enums;

namespace F1Predictions.Core.Config;

public record SectionConfig
{
    public int StartingRow { get; set; }
    public int QuestionCount { get; set; }
    public ScoringTypes ScoringType { get; set; }
    public ScoreOverrideConfig[] ScoringOverrides { get; set; } = Array.Empty<ScoreOverrideConfig>();
}