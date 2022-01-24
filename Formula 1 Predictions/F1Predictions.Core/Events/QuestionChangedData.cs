namespace F1Predictions.Core.Events;

public record QuestionChangedData
{
    public int QuestionIndex { get; set; }
    public int SectionIndex { get; set; }
    public bool IsProgression { get; set; }
}