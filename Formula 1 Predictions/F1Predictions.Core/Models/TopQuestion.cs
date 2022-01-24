namespace F1Predictions.Core.Models;

public record TopQuestion : BaseQuestion<ICompetitor>
{
    public Answer<ICompetitor>[] Answers { get; set; }
}