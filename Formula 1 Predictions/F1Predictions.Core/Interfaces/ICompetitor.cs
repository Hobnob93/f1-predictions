namespace F1Predictions.Core.Models;

public interface ICompetitor
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
}