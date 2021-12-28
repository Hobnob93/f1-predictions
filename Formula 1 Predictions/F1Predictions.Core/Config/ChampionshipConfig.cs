using F1Predictions.Core.Models;

namespace F1Predictions.Core.Config;

public class ChampionshipConfig
{
    public const string Section = "Championship";
    
    public TeamConfig[] Competitors { get; set; }
}