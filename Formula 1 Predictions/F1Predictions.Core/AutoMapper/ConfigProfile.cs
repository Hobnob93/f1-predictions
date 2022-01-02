using AutoMapper;
using F1Predictions.Core.Config;
using F1Predictions.Core.Dtos;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.AutoMapper;

public class ConfigProfile : Profile
{
    public ConfigProfile()
    {
        CreateMap<ParticipantConfig, Participant>();
        CreateMap<DriverConfig, Driver>();
        CreateMap<TeamConfig, Team>();
        CreateMap<ScoreOverrideConfig, ScoreOverride>();
        CreateMap<SectionConfig, Section>();
        
        CreateMap<ICollection<TeamConfig>, IEnumerable<Driver>>().ConvertUsing<TeamsToDriversTypeConverter>();
        CreateMap<PredictionFetchDto, Prediction<ICompetitor>[]>();
        CreateMap<AnswerFetchDto, Answer<ICompetitor>[]>();
    }
}