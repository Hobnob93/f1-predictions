namespace F1Predictions.Core.Models;

public class ParticipantScore
{
    public Participant Participant { get; set; }
    public int Score { get; set; }
    public int ForSection { get; set; }
    public int ForQuestion { get; set; }
}