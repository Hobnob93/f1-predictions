using F1Predictions.Core.Dtos;
using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.Services;

public class AnswerStore : IAnswerStore
{
    private readonly IGoogleSheets sheets;

    public AnswerStore(IGoogleSheets sheets)
    {
        this.sheets = sheets;
    }
    
    
    public AnswerFetchDto FetchPrediction(int sectionIndex, int questionIndex)
    {
        throw new NotImplementedException();
    }
}