namespace F1Predictions.Core.Interfaces;

public interface IProgressStatus
{
    int CurrentSectionIndex { get; }
    int CurrentQuestionIndex { get; }
}