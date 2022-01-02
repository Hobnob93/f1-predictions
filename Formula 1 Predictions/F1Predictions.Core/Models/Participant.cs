namespace F1Predictions.Core.Models;

public record Participant
{
    public string Name { get; set; }
    public string Color { get; set; }
    public int Column { get; set; }
}