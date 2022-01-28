namespace F1Predictions.Core.Models;

public record HeadToHeadQuestion : BaseQuestion<ICompetitor>
{
    public HeadToHead First { get; set; }
    public HeadToHead Second { get; set; }
    public string Result => First.Data > Second.Data ? "beat" : "equalled";
}