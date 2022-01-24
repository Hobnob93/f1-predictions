namespace F1Predictions.Core.Models;

public record NumericalQuestion : BaseQuestion<int>
{
    public Answer<int> Answer { get; set; }
}