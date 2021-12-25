using F1Predictions.Core.Enums;

namespace F1Predictions.Core.Models;

public record Section(int StartingRow, int QuestionCount, ScoringTypes ScoringType, ScoreOverride[] ScoringOverrides);