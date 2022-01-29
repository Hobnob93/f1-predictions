namespace F1Predictions.Core.Interfaces;

public interface IProgressService
{
    int CurrentSectionIndex { get; }
    int CurrentQuestionIndex { get; }

    void GoToQuestion(int sectionIndex, int questionIndex);
}