using F1Predictions.Core.Models;

namespace F1Predictions.Core.Interfaces;

public interface IGoogleSheets
{
    TopQuestion FetchTopQuestion(int currentSectionNum, int currentQuestionNum);
}