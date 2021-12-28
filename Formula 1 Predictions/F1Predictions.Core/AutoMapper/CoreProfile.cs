using AutoMapper;
using F1Predictions.Core.Config;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.AutoMapper;

public class CoreProfile : Profile
{
    public CoreProfile()
    {
        CreateMap<ParticipantConfig, Participant>();
        CreateMap<DriverConfig, Driver>();
        CreateMap<TeamConfig, Team>();
        CreateMap<ScoreOverrideConfig, ScoreOverride>();
        CreateMap<SectionConfig, Section>();
        CreateMap<TeamConfig[], Driver[]>().ConvertUsing<TeamsToDriversTypeConverter>();
    }
}