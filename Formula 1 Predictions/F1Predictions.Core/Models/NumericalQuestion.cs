namespace F1Predictions.Core.Models;

public record NumericalQuestion : BaseQuestion<string>
{
    public Answer<int> Answer { get; set; }
}