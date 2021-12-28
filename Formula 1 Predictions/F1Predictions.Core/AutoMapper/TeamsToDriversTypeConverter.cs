using AutoMapper;
using F1Predictions.Core.Config;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.AutoMapper;

public class TeamsToDriversTypeConverter : ITypeConverter<TeamConfig[], Driver[]>
{
    public Driver[] Convert(TeamConfig[] source, Driver[] destination, ResolutionContext context)
    {
        var drivers = new List<Driver>();
        var sources = source.Select(s => new {TeamSource = s, Drivers = s.DriverSources});
        foreach (var comboSource in sources)
        {
            foreach (var driverSource in comboSource.Drivers)
            {
                var driver = context.Mapper.Map<Driver>(driverSource);
                drivers.Add(driver with { Team = context.Mapper.Map<Team>(comboSource.TeamSource) });
            }
        }
        
        return drivers.ToArray();
    }
}