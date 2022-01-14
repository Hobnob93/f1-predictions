using F1Predictions.Core.Dtos;

namespace F1Predictions.Core.Interfaces;

public interface IAnswerStore
{
    AnswerFetchDto FetchAnswer(int sectionIndex, int questionIndex);
}