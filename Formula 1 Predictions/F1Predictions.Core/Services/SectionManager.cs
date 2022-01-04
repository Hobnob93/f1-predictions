using AutoMapper;
using F1Predictions.Core.Config;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services;

public class SectionManager : ISectionManager
{
    private readonly PredictionConfig config;
    private readonly IMapper mapper;
    private readonly IGoogleSheets sheets;

    public SectionManager(PredictionConfig config, IMapper mapper, IGoogleSheets sheets)
    {
        this.config = config;
        this.mapper = mapper;
        this.sheets = sheets;
        
        //sections = config.PredictionSections.Select(mapper.Map<Section>)
        //    .Select((s, sectionIndex) => s with
        //    {
        //        Name = sheets.FetchSectionTitle(sectionIndex),
        //        Questions = Enumerable.Range(0, s.QuestionCount)
        //            .Select(questionIndex => sheets.FetchTopQuestion(sectionIndex, questionIndex))
        //            .ToArray()
        //    })
        //    .ToArray();
    }
}