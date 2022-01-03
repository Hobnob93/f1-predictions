using AutoMapper;
using F1Predictions.Core.Config;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services;

public class SectionManager : ISectionManager
{
    private readonly IMapper mapper;
    private readonly Section[] sections;

    public SectionManager(PredictionConfig config, IMapper mapper, IGoogleSheets sheets)
    {
        this.mapper = mapper;

        sections = config.PredictionSections.Select(mapper.Map<Section>)
            .Select((s, i) => s with { Name = sheets.FetchSectionTitle(i) })
            .ToArray();
    }
}