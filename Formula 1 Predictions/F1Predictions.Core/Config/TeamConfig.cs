namespace F1Predictions.Core.Config;

public record TeamConfig
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public DriverConfig[] Drivers { get; set; }
}