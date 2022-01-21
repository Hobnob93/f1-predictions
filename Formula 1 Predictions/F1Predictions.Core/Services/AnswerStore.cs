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
        var sections = sheets.FetchAllAnswers().ToList();

        answers = new List<IList<AnswerFetchDto>>();

        foreach (var sectionConfig in config.PredictionSections)
        {
            var answersForSection = new List<AnswerFetchDto>();

            for (var q = 0; q < sectionConfig.QuestionCount; q++)
            {
                var row = sectionConfig.StartingRow + 2 + q;
                var sectionData = sections[row - 1].ToArray();
                var sectionNameData = sections[sectionConfig.StartingRow - 1];
                
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

    public (AnswerFetchDto answer, int actualAnswerIndex) FetchOrderedAnswers(int sectionIndex, int questionIndex, int prevCount = 2, int afterCount = 2)
    {
        var totalSize = prevCount + afterCount + 1;
        var order = new AnswerFetchDto[totalSize];
        
        var section = answers[sectionIndex];
        var workingIndex = 0;
        for (var i = prevCount; i > 0; i--)
        {
            var offsetQuestionIndex = questionIndex - i;
            if (offsetQuestionIndex < 0)
                order[workingIndex] = null;
            else
                order[workingIndex] = section[offsetQuestionIndex];
            
            workingIndex++;
        }

        order[workingIndex] = section[questionIndex];
        var actualAnswerIndex = workingIndex++;

        for (var i = 1; i <= afterCount; i++)
        {
            var offsetQuestionIndex = questionIndex + i;
            if (offsetQuestionIndex >= section.Count)
                order[workingIndex] = null;
            else
                order[workingIndex] = section[offsetQuestionIndex];
            
            workingIndex++;
        }

        var answer = order[actualAnswerIndex] with
        {
            Answers = order.Select(o => o?.Answers?.FirstOrDefault()).ToArray(),
            Notes = order.Select(o => o?.Notes.FirstOrDefault()).ToArray()
        };
        
        return (answer, actualAnswerIndex);
    }
}