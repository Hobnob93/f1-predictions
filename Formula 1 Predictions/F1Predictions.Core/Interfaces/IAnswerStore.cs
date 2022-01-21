using F1Predictions.Core.Dtos;

namespace F1Predictions.Core.Interfaces;

public interface IAnswerStore
{
    AnswerFetchDto FetchAnswer(int sectionIndex, int questionIndex);

    (AnswerFetchDto answer, int actualAnswerIndex) FetchOrderedAnswers(int sectionIndex, int questionIndex,
        int prevCount = 2, int afterCount = 2);
}