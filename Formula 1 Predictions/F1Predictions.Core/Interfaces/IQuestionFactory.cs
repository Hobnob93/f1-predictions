using F1Predictions.Core.Models;

namespace F1Predictions.Core.Interfaces;

public interface IQuestionFactory
{
    BaseQuestion GetQuestion();
}