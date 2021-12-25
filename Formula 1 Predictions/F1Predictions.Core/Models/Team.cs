namespace F1Predictions.Core.Models;

public record Team
{
    public string Name { get; set; }
    public string Color { get; set; }
    public Driver[] Drivers { get; set; }
}