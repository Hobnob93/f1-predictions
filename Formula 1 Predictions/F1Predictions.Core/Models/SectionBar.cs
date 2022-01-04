namespace F1Predictions.Core.Models;

public record SectionBar : BaseBar
{
    public BaseBar[] ChildBars { get; set; } 
}