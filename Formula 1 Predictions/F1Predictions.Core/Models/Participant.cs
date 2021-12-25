namespace F1Predictions.Core.Models;

public record Participant
{
    public string Name { get; set; }
    public string Column { get; set; }
    public string Color { get; set; }
}