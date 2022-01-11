namespace F1Predictions.Core.Models;

public record HeadToHeadQuestion : BaseQuestion
{
    public Prediction<ICompetitor>[] Predictions { get; set; }
    public Answer<ICompetitor>[] Answers { get; set; }

    public HeadToHead First => HeadToHeadFromAnswer(0);
    public HeadToHead Second => HeadToHeadFromAnswer(1);
    public string Result => First.Data > Second.Data ? "beat" : "equalled";

    private HeadToHead HeadToHeadFromAnswer(int index)
    {
        return new HeadToHead
        {
            Name = Answers[index].Value.Name,
            Color = Answers[index].Value.Color,
            Data = int.Parse(Answers[index].Data)
        };
    }
}