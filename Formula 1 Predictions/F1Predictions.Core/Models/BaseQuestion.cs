namespace F1Predictions.Core.Models;

public record BaseQuestion
{
    public string Section { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Scoring { get; set; }
}

public record BaseQuestion<T> : BaseQuestion
{
    public Prediction<T>[] Predictions { get; set; }
}