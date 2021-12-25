using F1Predictions.Core.Enums;

namespace F1Predictions.Core.Models;

public record Section
{
    public int StartingRow { get; set; }
    public int QuestionCount { get; set; }
    public ScoringTypes ScoringType { get; set; }
    public ScoreOverride[] ScoringOverrides { get; set; } = Array.Empty<ScoreOverride>();
}