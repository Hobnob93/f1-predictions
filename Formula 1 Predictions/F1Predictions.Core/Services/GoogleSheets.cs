using F1Predictions.Core.Interfaces;
using Google.Apis.Sheets.v4;

namespace F1Predictions.Core.Services;

public class GoogleSheets : IGoogleSheets
{
    private readonly string spreadsheetId = "16nlhSKutA22Z-Llu7NNYxTPUu1DgZQAtIQAWwMSHkyI";
    private readonly string predictionsSheetName = "Predictions";
    private readonly string resultsSheetName = "Results";
    private readonly SheetsService clientService;

    public GoogleSheets(SheetsService clientService)
    {
        this.clientService = clientService;
    }
    
    private IList<IList<object>> ReadValues(string sheet, string range)
    {
        var request = clientService.Spreadsheets.Values.Get(spreadsheetId, $"{sheet}!{range}");
        var response = request.Execute();
        return response.Values;
    }
}