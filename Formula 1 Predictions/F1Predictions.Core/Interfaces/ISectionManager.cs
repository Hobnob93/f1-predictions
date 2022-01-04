using F1Predictions.Core.Models;

namespace F1Predictions.Core.Interfaces;

public interface ISectionManager
{
    TopQuestion GetQuestion(int sectionIndex, int questionIndex);
}