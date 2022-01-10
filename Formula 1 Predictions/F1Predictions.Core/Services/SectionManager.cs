using AutoMapper;
using F1Predictions.Core.Config;
using F1Predictions.Core.Constants;
using F1Predictions.Core.Enums;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services;

public class SectionManager : ISectionManager
{
    private readonly IGoogleSheets sheets;
    private readonly Section[] sections;

    public SectionManager(PredictionConfig config, IMapper mapper, IGoogleSheets sheets)
    {
        this.sheets = sheets;

        sections = config.PredictionSections.Select(mapper.Map<Section>)
            .ToArray();
    }

    public BaseQuestion GetQuestion(int sectionIndex, int questionIndex)
    {
        var section = sections[sectionIndex];
        if (questionIndex >= section.QuestionCount)
            return null;
        
        var overrideData = section.ScoringOverrides.FirstOrDefault(so => so.QuestionIndex == questionIndex);
        var scoringType = overrideData?.ScoringType ?? section.ScoringType;

        var question = (BaseQuestion) (scoringType switch
        {
            ScoringTypes.Top => GetTopQuestion(sectionIndex, questionIndex),
            ScoringTypes.Numerical => GetNumericalQuestion(sectionIndex, questionIndex),
            _ => null
        });

        if (question is null)
            return null;

        return question with
        {
            Section = sheets.FetchSectionTitle(sectionIndex)
        };
    }

    private TopQuestion GetTopQuestion(int sectionIndex, int questionIndex)
    {
        return sheets.FetchTopQuestion(sectionIndex, questionIndex);
    }
    
    private NumericalQuestion GetNumericalQuestion(int sectionIndex, int questionIndex)
    {
        return sheets.FetchNumericalQuestion(sectionIndex, questionIndex);
    }
}