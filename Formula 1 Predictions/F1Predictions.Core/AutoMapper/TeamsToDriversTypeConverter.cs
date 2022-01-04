using AutoMapper;
using F1Predictions.Core.Config;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.AutoMapper;

public class TeamsToDriversTypeConverter : ITypeConverter<IEnumerable<TeamConfig>,IEnumerable<Driver>>
{
    public IEnumerable<Driver> Convert(IEnumerable<TeamConfig> source, IEnumerable<Driver> destination, ResolutionContext context)
    {
        var sources = source.Select(s => new {TeamSource = s, DriverSources = s.Drivers});
        
        return (from comboSource in sources from driverSource in comboSource.DriverSources 
            let driver = context.Mapper.Map<Driver>(driverSource) 
            select driver with {Team = context.Mapper.Map<Team>(comboSource.TeamSource)})
            .ToList();
    }
}