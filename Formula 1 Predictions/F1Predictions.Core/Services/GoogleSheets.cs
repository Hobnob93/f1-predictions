using System.Text;
using AutoMapper;
using F1Predictions.Core.Config;
using F1Predictions.Core.Dtos;
using F1Predictions.Core.Interfaces;
using Google.Apis.Sheets.v4;
using F1Predictions.Core.Models;

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


    public string FetchSectionTitle(int currentSectionNum)
    {
        var row = config.PredictionSections[currentSectionNum].StartingRow;

        return FetchTitle(row);
    }

    private int CalculateRow(int currentSectionNum, int currentQuestionNum)
    {
        var currentSection = config.PredictionSections[currentSectionNum];
        return currentSection.StartingRow + 2 + currentQuestionNum;
    }

    private string FetchTitle(int row)
    {
        var values = ReadValues(PredictionsSheetName, $"{config.HeaderColumn}{row}")?.ToArray();

        return values?.FirstOrDefault()?.FirstOrDefault()?.ToString() ?? "Title Not Found";
    }

    private AnswerFetchDto FetchAnswers(int row)
    {
        var values = ReadValues(AnswersSheetName, $"{config.QuestionColumn}{row}:{config.EndOfAnswerColumn}{row}")?.ToArray();
        var answersEnd = config.MaxAnswersPerQuestion + 1;
        var notesBegin = answersEnd;
        
        return new AnswerFetchDto
        {
            Question = values?.FirstOrDefault()?.FirstOrDefault()?.ToString() ?? "Question Not Found",
            Answers = values?.FirstOrDefault()?.Take(1..answersEnd)
                .Select(o => o.ToString())
                .ToArray() ?? Array.Empty<string>(),
            Notes = values?.FirstOrDefault()?.Take(notesBegin..^1)
                .Select(o => o.ToString())
                .ToArray() ?? Array.Empty<string>(),
            Scoring = values?.FirstOrDefault()?.LastOrDefault()?.ToString() ?? "No scoring could be found"
        };
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

    private string DetermineFetchRanges()
    {
        var strBuilder = new StringBuilder();

        foreach (var p in config.PredictionSections)
        {
            var initialRow = p.StartingRow;
            var finalRow = initialRow + p.QuestionCount + 1;

            var initialColumn = config.QuestionColumn;
            var finalColumn = config.EndOfAnswerColumn;

            if (strBuilder.Length > 0)
                strBuilder.Append(',');

            strBuilder.Append($"{initialColumn}{initialRow}:{finalColumn}{finalRow}");
        }

        return strBuilder.ToString();
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
}