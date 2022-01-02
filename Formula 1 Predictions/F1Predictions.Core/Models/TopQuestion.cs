namespace F1Predictions.Core.Models;

public record TopQuestion
{
    public string Name { get; set; }
    public Prediction<ICompetitor>[] Predictions { get; set; }
    public Answer<ICompetitor>[] Answers { get; set; }
}