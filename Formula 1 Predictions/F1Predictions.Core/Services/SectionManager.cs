using AutoMapper;
using F1Predictions.Core.Config;
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

    public TopQuestion GetQuestion(int sectionIndex, int questionIndex)
    {
        var section = sections[sectionIndex];
        if (questionIndex >= section.QuestionCount)
            return null;

        return sheets.FetchTopQuestion(sectionIndex, questionIndex) with
        {
            Section = sheets.FetchSectionTitle(sectionIndex)
        };
    }
}