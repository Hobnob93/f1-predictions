using F1Predictions.Core.Config;
using F1Predictions.Core.Dtos;
using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.Services;

public class PredictionStore : IPredictionStore
{
    private readonly IGoogleSheets sheets;
    private readonly PredictionConfig config;

    private IList<IList<PredictionFetchDto>> predictions;

    public PredictionStore(IGoogleSheets sheets, PredictionConfig config)
    {
        this.sheets = sheets;
        this.config = config;

        InitializePredictions();
    }

    private void InitializePredictions()
    {
        var sections = sheets.FetchAllPredictions().ToList();

        predictions = new List<IList<PredictionFetchDto>>();

        foreach (var sectionConfig in config.PredictionSections)
        {
            var predictionsForSection = new List<PredictionFetchDto>();

            for (var q = 0; q < sectionConfig.QuestionCount; q++)
            {
                var row = sectionConfig.StartingRow + 2 + q;
                var sectionData = sections[row - 1].ToArray();
                var sectionNameData = sections[sectionConfig.StartingRow - 1];
                
                predictionsForSection.Add(new PredictionFetchDto
                {
                    Section = sectionNameData?.FirstOrDefault()?.ToString() ?? "Title Not Found",
                    Question = sectionData.FirstOrDefault()?.ToString() ?? "Question Not Found",
                    Note = sectionData.LastOrDefault()?.ToString() ?? string.Empty,
                    Predictions = sectionData.Take(1..^1)
                        .Select(o => o.ToString())
                        .ToArray()
                });
            }
            
            predictions.Add(predictionsForSection);
        }
    }
    
    
    public PredictionFetchDto FetchPrediction(int sectionIndex, int questionIndex)
    {
        return predictions[sectionIndex][questionIndex];
    }
}