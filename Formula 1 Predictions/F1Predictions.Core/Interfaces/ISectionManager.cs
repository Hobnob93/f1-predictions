using F1Predictions.Core.Enums;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Interfaces;

public interface ISectionManager
{
    Section GetCurrentSection();
    ScoringTypes GetCurrentQuestionScoringType();
}