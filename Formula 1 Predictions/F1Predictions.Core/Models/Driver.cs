namespace F1Predictions.Core.Models;

public record Driver : ICompetitor
{
    private string color = "#FFFFFF";
    
    public string Id { get; set; }
    public string Name { get; set; }
    public Team Team { get; set; }
    public int ChampionshipPosition { get; set; }

    public string Color
    {
        get => Team?.Color ?? color;
        set => color = value;
    }
}