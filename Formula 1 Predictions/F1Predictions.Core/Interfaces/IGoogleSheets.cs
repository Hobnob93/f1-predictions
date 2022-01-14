using F1Predictions.Core.Models;

namespace F1Predictions.Core.Interfaces;

public interface IGoogleSheets
{
    string FetchSectionTitle(int currentSectionNum);
    TopQuestion FetchTopQuestion(int sectionIndex, int questionIndex);
    TopMiscQuestion FetchTopMiscQuestion(int sectionIndex, int questionIndex);
    NumericalQuestion FetchNumericalQuestion(int sectionIndex, int questionIndex);
    HeadToHeadQuestion FetchHeadToHeadQuestion(int sectionIndex, int questionIndex);
    IEnumerable<IEnumerable<object>> FetchAllPredictions();
    IEnumerable<IEnumerable<object>> FetchAllAnswers();
}