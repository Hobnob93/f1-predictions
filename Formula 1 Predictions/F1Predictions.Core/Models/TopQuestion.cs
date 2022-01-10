namespace F1Predictions.Core.Models;

public record TopQuestion : BaseQuestion
{
    public Prediction<ICompetitor>[] Predictions { get; set; }
    public Answer<ICompetitor>[] Answers { get; set; }
}