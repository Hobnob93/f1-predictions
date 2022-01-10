namespace F1Predictions.Core.Models;

public record NumericalQuestion : BaseQuestion
{
    public Prediction<int>[] Predictions { get; set; }
    public Answer<int> Answers { get; set; }
}