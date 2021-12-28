namespace F1Predictions.Core.Models;

public record Driver
{
    public string Id { get; set; }
    public string Name { get; set; }
    public Team Team { get; set; }
}