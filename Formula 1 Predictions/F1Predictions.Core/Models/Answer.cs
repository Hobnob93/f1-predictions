namespace F1Predictions.Core.Models;

public record Answer<T>
{
    public T Value { get; set; }
    public string Data { get; set; }
}