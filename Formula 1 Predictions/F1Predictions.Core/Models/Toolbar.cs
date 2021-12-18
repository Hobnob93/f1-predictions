namespace F1Predictions.Core.Models;

public record Toolbar
{
    public string AppName { get; set; } = "F1 Predictions 2021";
    public string ImageLocation { get; set; } = "/Images/F1.png";
}