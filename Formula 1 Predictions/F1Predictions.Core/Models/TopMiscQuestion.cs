namespace F1Predictions.Core.Models;

public record TopMiscQuestion : BaseQuestion
{
    public Prediction<string>[] Predictions { get; set; }
    public Answer<string>[] Answers { get; set; }
}