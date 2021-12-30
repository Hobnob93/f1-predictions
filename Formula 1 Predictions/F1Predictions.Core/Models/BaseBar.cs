namespace F1Predictions.Core.Models;

public record BaseBar
{
    public string Color { get; set; }
    public bool IsActive { get; set; }
    public bool IsComplete { get; set; }
};