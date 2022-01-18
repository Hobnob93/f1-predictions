using F1Predictions.Core.Config;
using F1Predictions.Core.Dtos;
using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.Services;

public class AnswerStore : IAnswerStore
{
    private readonly IGoogleSheets sheets;
    private readonly PredictionConfig config;
    
    private IList<IList<AnswerFetchDto>> answers;
    
    public AnswerStore(IGoogleSheets sheets, PredictionConfig config)
    {
        this.sheets = sheets;
        this.config = config;
        
        InitializePredictions();
    }
    
    private void InitializePredictions()
    {
        var sections = sheets.FetchAllPredictions().ToList();

        answers = new List<IList<AnswerFetchDto>>();

        foreach (var sectionConfig in config.PredictionSections)
        {
            var answersForSection = new List<AnswerFetchDto>();

            for (var q = 0; q < sectionConfig.QuestionCount; q++)
            {
                var row = sectionConfig.StartingRow + 2 + q;
                var sectionData = sections[row].ToArray();
                var sectionNameData = sections[sectionConfig.StartingRow];
                
                var answersEnd = config.MaxAnswersPerQuestion + 1;

                answersForSection.Add(new AnswerFetchDto
                {
                    Section = sectionNameData?.FirstOrDefault()?.ToString() ?? "Title Not Found",
                    Question = sectionData.FirstOrDefault()?.ToString() ?? "Question Not Found",
                    Answers = sectionData.Take(1..answersEnd)
                        .Select(o => o.ToString())
                        .ToArray(),
                    Notes = sectionData.Take(answersEnd..^1)
                        .Select(o => o.ToString())
                        .ToArray(),
                    Scoring = sectionData.LastOrDefault()?.ToString() ?? "No scoring could be found"
                });
            }
            
            answers.Add(answersForSection);
        }
    }
    
    
    public AnswerFetchDto FetchAnswer(int sectionIndex, int questionIndex)
    {
        return answers[sectionIndex][questionIndex];
    }
}