using AutoMapper;
using F1Predictions.Core.Config;
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
        
        return questionIndex >= section.QuestionCount 
            ? null 
            : FetchBaseQuestion(section, sectionIndex, questionIndex);
    }

    private BaseQuestion FetchBaseQuestion(Section section, int sectionIndex, int questionIndex)
    {
        var overrideData = section.ScoringOverrides.FirstOrDefault(so => so.QuestionIndex == questionIndex);
        var scoringType = overrideData?.ScoringType ?? section.ScoringType;

        var question = (BaseQuestion) (scoringType switch
        {
            ScoringTypes.Top => sheets.FetchTopQuestion(sectionIndex, questionIndex),
            ScoringTypes.Numerical => sheets.FetchNumericalQuestion(sectionIndex, questionIndex),
            ScoringTypes.HeadToHead => sheets.FetchHeadToHeadQuestion(sectionIndex, questionIndex),
            _ => null
        });
        
        return question! with
        {
            Section = sheets.FetchSectionTitle(sectionIndex)
        };
    }
}