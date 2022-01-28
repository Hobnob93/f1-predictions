namespace F1Predictions.Core.Models;

public record Participant
{
    public string Name { get; set; }
    public string Color { get; set; }
    public int Index { get; set; }
    public Dictionary<string, int> Scores { get; set; } = new();
}