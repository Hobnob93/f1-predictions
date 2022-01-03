using AutoMapper;
using F1Predictions.Core.Config;
using F1Predictions.Core.Dtos;
using F1Predictions.Core.Interfaces;
using Google.Apis.Sheets.v4;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services;

public class GoogleSheets : IGoogleSheets
{
    private readonly string spreadsheetId = "16nlhSKutA22Z-Llu7NNYxTPUu1DgZQAtIQAWwMSHkyI";
    private readonly string predictionsSheetName = "Predictions";
    private readonly string resultsSheetName = "Results";
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
    
    public TopQuestion FetchTopQuestion(int currentSectionNum, int currentQuestionNum)
    {
        var row = CalculateRow(currentSectionNum, currentQuestionNum);
        var question = FetchQuestion(row);
        var answers = FetchAnswers(row);
        
        return new TopQuestion
        {
            Name = question.Question,
            Predictions = mapper.Map<Prediction<ICompetitor>[]>(question),
            Answers = mapper.Map<Answer<ICompetitor>[]>(answers)
        };
    }

    private int CalculateRow(int currentSectionNum, int currentQuestionNum)
    {
        var currentSection = config.PredictionSections[currentSectionNum];
        return currentSection.StartingRow + 2 + currentQuestionNum;
    }

    private string FetchTitle(int row)
    {
        var values = ReadValues(predictionsSheetName, $"{config.HeaderColumn}{row}")?.ToArray();

        return values?.FirstOrDefault()?.FirstOrDefault()?.ToString() ?? "Title Not Found";
    }
    
    private PredictionFetchDto FetchQuestion(int row)
    {
        var values = ReadValues(predictionsSheetName, $"{config.QuestionColumn}{row}:{config.InfoColumn}{row}")?.ToArray();
        
        return new PredictionFetchDto
        {
            Question = values?.FirstOrDefault()?.FirstOrDefault()?.ToString() ?? "Question Not Found",
            Note = values?.FirstOrDefault()?.LastOrDefault()?.ToString() ?? string.Empty,
            Predictions = values?.FirstOrDefault()?.Take(1..^1)
                .Select(o => o.ToString())
                .ToArray() ?? Array.Empty<string>()
        };
    }

    private AnswerFetchDto FetchAnswers(int row)
    {
        var values = ReadValues(resultsSheetName, $"{config.QuestionColumn}{row}:{config.EndOfAnswerColumn}{row}")?.ToArray();
        var answersEnd = config.MaxAnswersPerQuestion + 1;
        var notesBegin = answersEnd;
        
        return new AnswerFetchDto
        {
            Question = values?.FirstOrDefault()?.FirstOrDefault()?.ToString() ?? "Question Not Found",
            Answers = values?.FirstOrDefault()?.Take(1..answersEnd)
                .Select(o => o.ToString())
                .ToArray() ?? Array.Empty<string>(),
            Notes = values?.FirstOrDefault()?.Take(notesBegin..)
                .Select(o => o.ToString())
                .ToArray() ?? Array.Empty<string>(),
        };
    }
    
    private IEnumerable<IEnumerable<object>> ReadValues(string sheet, string range)
    {
        var request = clientService.Spreadsheets.Values.Get(spreadsheetId, $"{sheet}!{range}");
        var response = request.Execute();
        return response.Values;
    }
}