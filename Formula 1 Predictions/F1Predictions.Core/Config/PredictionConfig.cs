using F1Predictions.Core.Models;

namespace F1Predictions.Core.Config;

public class PredictionConfig
{
    public const string Section = "PredictionSettings";
    
    public string HeaderColumn { get; set; }
    public string QuestionColumn { get; set; }
    public string InfoColumn { get; set; }
    public SectionConfig[] PredictionSections { get; set; }
    public ParticipantConfig[] Participants { get; set; }
}