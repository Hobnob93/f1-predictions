using F1Predictions.Core.Enums;

namespace F1Predictions.Core.Models;

public record Section
{
    public string Name { get; set; }
    public int StartingRow { get; set; }
    public int QuestionCount { get; set; }
    public ScoringTypes ScoringType { get; set; }
    public ScoreOverride[] ScoringOverrides { get; set; } = Array.Empty<ScoreOverride>();
    public TopQuestion[] Questions { get; set; } = Array.Empty<TopQuestion>();
}