using AutoMapper;
using F1Predictions.Core.Config;
using F1Predictions.Core.Enums;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services;

public class SectionManager : ISectionManager
{
    private readonly IProgressService progress;
    private readonly Section[] sections;

    public SectionManager(PredictionConfig config, IMapper mapper, IProgressService progress)
    {
        this.progress = progress;
        
        sections = config.PredictionSections.Select(mapper.Map<Section>)
            .ToArray();
    }

    public Section GetCurrentSection()
    {
        return sections[progress.CurrentSectionIndex];
    }

    public ScoringTypes GetCurrentQuestionScoringType()
    {
        var section = GetCurrentSection();
        
        var overrideData = section.ScoringOverrides.FirstOrDefault(so => so.QuestionIndex == progress.CurrentQuestionIndex);
        return overrideData?.ScoringType ?? section.ScoringType;
    }
}