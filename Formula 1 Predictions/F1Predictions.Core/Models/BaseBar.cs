namespace F1Predictions.Core.Models;

public record BaseBar
{
    public string Color { get; set; }
    public bool IsComplete { get; set; }

    public void Complete(string color)
    {
        IsComplete = true;
        Color = color;
    }
};