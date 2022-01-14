using F1Predictions.Core.Dtos;
using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.Services;

public class PredictionStore : IPredictionStore
{
    private readonly IGoogleSheets sheets;

    public PredictionStore(IGoogleSheets sheets)
    {
        this.sheets = sheets;
        
        InitializePredictions();
    }

    private void InitializePredictions()
    {
        
    }
    
    
    public PredictionFetchDto FetchPrediction(int sectionIndex, int questionIndex)
    {
        throw new NotImplementedException();
    }
}