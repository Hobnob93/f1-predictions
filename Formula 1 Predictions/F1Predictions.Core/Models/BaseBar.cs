namespace F1Predictions.Core.Models;

public record BaseBar
{
    public string Color { get; set; }
    public bool IsComplete { get; set; }

    public void Complete(string color)
    {
        Color = color;
        IsComplete = true;
    }

    public void Activate(string color)
    {
        Color = color;
    }

    public void Deactivate(string defaultColor, string completedColor)
    {
        Color = IsComplete ? completedColor : defaultColor;
    }
};