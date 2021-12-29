using AutoMapper;
using F1Predictions.Core.AutoMapper;
using F1Predictions.Core.Config;

namespace F1Predictions.Tests.Base;

public abstract class MapperTests
{
    protected const string TeamIdPrefix = "Team-";
    protected const string TeamNamePrefix = "Team";
    protected const string ColorPrefix = "Color";
    protected const string DriverIdPrefix = "Driver-";
    protected const string DriverNamePrefix = "Driver";
    
    protected readonly IMapper Mapper;

    protected MapperTests()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ConfigProfile>();
        });

        Mapper = mapperConfig.CreateMapper();
    }
    
    protected ChampionshipConfig CreateChampionshipConfig(int numTeams = 1, int numDrivers = 0)
    {
        var teams = new List<TeamConfig>();
        for (var i = 0; i < numTeams; i++)
        {
            var team = new TeamConfig
            {
                Id = $"{TeamIdPrefix}{i}",
                Name = $"{TeamNamePrefix}{i}",
                Color = $"{ColorPrefix}{i}"
            };

            var drivers = new List<DriverConfig>();
            for (var j = 0; j < numDrivers; j++)
            {
                drivers.Add(new DriverConfig
                {
                    Id = $"{DriverIdPrefix}{j}-{i}",
                    Name = $"{DriverNamePrefix}{j}-{i}"
                });
            }

            team.Drivers = drivers.ToArray();
            teams.Add(team);
        }

        return new ChampionshipConfig
        {
            Competitors = teams.ToArray()
        };
    }

    protected TeamConfig GetTeamConfigFromTeamId(IEnumerable<TeamConfig> teamsConfig, string teamId)
    {
        return teamsConfig.Single(t => t.Id == teamId);
    }
    
    protected TeamConfig GetTeamConfigFromDriverId(IEnumerable<TeamConfig> teamsConfig, string driverId)
    {
        var driverConfig = GetDriverConfigFromDriverId(teamsConfig, driverId);
        return teamsConfig.Single(t => t.Drivers.Contains(driverConfig));
    }

    protected DriverConfig GetDriverConfigFromDriverId(IEnumerable<TeamConfig> teamsConfig, string driverId)
    {
        return teamsConfig.SelectMany(t => t.Drivers)
            .Single(d => d.Id == driverId);
    }
}