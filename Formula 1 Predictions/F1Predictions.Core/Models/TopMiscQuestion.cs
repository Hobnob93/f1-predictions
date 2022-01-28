namespace F1Predictions.Core.Models;

public record TopMiscQuestion : BaseQuestion<string>
{
    public Answer<string>[] Answers { get; set; }
}