namespace F1Predictions.Core.Config;

public record DriverConfig
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int ChampionshipPosition { get; set; }
};