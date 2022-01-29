namespace F1Predictions.Core.Config;

public record ParticipantConfig
{
    public string Name { get; set; }
    public int Index { get; set; }
    public string Column { get; set; }
    public string Color { get; set; }
}