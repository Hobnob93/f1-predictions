using AutoMapper;
using F1Predictions.Core.Config;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace F1Predictions.Core.Services;

public class GoogleSheets : IGoogleSheets
{
    private const string SpreadsheetId = "16nlhSKutA22Z-Llu7NNYxTPUu1DgZQAtIQAWwMSHkyI";
    private const string PredictionsSheetName = "Predictions";
    private const string AnswersSheetName = "Answers";
    private const string ResultsSheetName = "Results";
    
    private readonly SheetsService clientService;
    private readonly PredictionConfig config;
    private readonly IMapper mapper;

    public GoogleSheets(SheetsService clientService, PredictionConfig config, IMapper mapper)
    {
        this.clientService = clientService;
        this.config = config;
        this.mapper = mapper;
    }

    public IEnumerable<IEnumerable<object>> FetchAllPredictions()
    {
        var range = DetermineFetchRanges();

        return ReadValues(PredictionsSheetName, range);
    }
    
    public IEnumerable<IEnumerable<object>> FetchAllAnswers()
    {
        var range = DetermineFetchRanges();
        
        return ReadValues(AnswersSheetName, range);
    }

    public void SaveScores(List<ParticipantScore> participantScores)
    {
        var sectionIndex = participantScores.First().ForSection;
        var questionIndex = participantScores.First().ForQuestion;

        var orderedScores = participantScores.OrderBy(ps => ps.Participant.Index);
        var outerList = new List<IList<object>>();
        var innerList = orderedScores.Select(o => o.Score as object).ToList();
        outerList.Add(innerList);

        var section = config.PredictionSections[sectionIndex];
        var row = section.StartingRow + questionIndex + 2;

        var orderedParticipantConfig = config.Participants.OrderBy(p => p.Index).ToList();
        var firstCol = orderedParticipantConfig.First().Column;
        var lastCol = orderedParticipantConfig.Last().Column;
        var updatedCellsCount = WriteValues(ResultsSheetName, $"{firstCol}{row}:{lastCol}{row}", outerList);
    }

    private string DetermineFetchRanges()
    {
        var firstSection = config.PredictionSections.First();
        var lastSection = config.PredictionSections.Last();

        var initialRow = firstSection.StartingRow;
        var finalRow = lastSection.StartingRow + lastSection.QuestionCount + 1;
        
        var initialColumn = config.QuestionColumn;
        var finalColumn = config.EndOfAnswerColumn;

        return $"{initialColumn}{initialRow}:{finalColumn}{finalRow}";
    }
    
    private IEnumerable<IEnumerable<object>> ReadValues(string sheet, string range)
    {
        var request = clientService.Spreadsheets.Values.Get(SpreadsheetId, $"{sheet}!{range}");
        var requestResponse = request.Execute();
        var response = requestResponse.Values;

        foreach (var values in response)
        {
            for (var j = 0; j < values.Count; j++)
            {
                var val = values[j];
                if (val is "/")
                    values[j] = string.Empty;
            }
        }

        return response;
    }

    private int WriteValues(string sheet, string range, IList<IList<object>> values)
    {
        var valueRange = new ValueRange
        {
            Range = $"{sheet}!{range}",
            Values = values
        };
        
        var request = clientService.Spreadsheets.Values.Update(valueRange, SpreadsheetId, $"{sheet}!{range}");
        request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
        request.IncludeValuesInResponse = true;
        var requestResponse = request.Execute();
        return requestResponse.UpdatedCells ?? 0;
    }
}