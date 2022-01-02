namespace F1Predictions.Core.Models;

public record Prediction<T>
{
    public Participant Participant { get; set; }
    public T Value { get; set; }
}