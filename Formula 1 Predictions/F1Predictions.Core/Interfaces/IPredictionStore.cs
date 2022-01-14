using F1Predictions.Core.Dtos;

namespace F1Predictions.Core.Interfaces;

public interface IPredictionStore
{
    PredictionFetchDto FetchPrediction(int sectionIndex, int questionIndex);
}