using F1Predictions.Core.Config;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Google.Apis.Sheets.v4;

namespace F1Predictions.Core.Services;

public class GoogleSheets : IGoogleSheets
{
    private readonly string spreadsheetId = "16nlhSKutA22Z-Llu7NNYxTPUu1DgZQAtIQAWwMSHkyI";
    private readonly string predictionsSheetName = "Predictions";
    private readonly string resultsSheetName = "Results";
    private readonly SheetsService clientService;
    private readonly ICompetitorManager competitorManager;
    private readonly IParticipantsManager participantsManager;
    private readonly PredictionConfig config;

    public GoogleSheets(SheetsService clientService, ICompetitorManager competitorManager, IParticipantsManager participantsManager, PredictionConfig config)
    {
        this.clientService = clientService;
        this.competitorManager = competitorManager;
        this.participantsManager = participantsManager;
        this.config = config;
    }


    public TopQuestion FetchTopQuestion(int currentSectionNum, int currentQuestionNum)
    {
        var currentSection = config.PredictionSections[currentSectionNum];
        var row = (currentSection.StartingRow + 2) + currentQuestionNum;
        
        return new TopQuestion
        {
            Name = ReadString(predictionsSheetName, $"{config.QuestionColumn}{row}")
        };
    }

    private string ReadString(string sheet, string range)
    {
        var values = ReadValues(sheet, range);
        return values.FirstOrDefault()?.FirstOrDefault()?.ToString() ?? string.Empty;
    }
    
    private IList<IList<object>> ReadValues(string sheet, string range)
    {
        var request = clientService.Spreadsheets.Values.Get(spreadsheetId, $"{sheet}!{range}");
        var response = request.Execute();
        return response.Values;
    }
}