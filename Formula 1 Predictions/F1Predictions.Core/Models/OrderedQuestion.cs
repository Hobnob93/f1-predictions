namespace F1Predictions.Core.Models;

public record OrderedQuestion : BaseQuestion<ICompetitor>
{
    public int Position { get; set; }
    public int AnswerAtIndex { get; set; }
    public Answer<ICompetitor>[] Answers { get; set; }
}