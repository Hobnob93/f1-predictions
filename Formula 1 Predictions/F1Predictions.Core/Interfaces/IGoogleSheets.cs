using F1Predictions.Core.Models;

namespace F1Predictions.Core.Interfaces;

public interface IGoogleSheets
{
    string FetchSectionTitle(int currentSectionNum);
    TopQuestion FetchTopQuestion(int currentSectionNum, int currentQuestionNum);
}