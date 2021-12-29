using F1Predictions.Core.Config;
using F1Predictions.Core.Models;
using F1Predictions.Tests.Base;
using FluentAssertions;
using NUnit.Framework;

namespace F1Predictions.Tests.Core.AutoMapper;

[TestFixture]
public class TeamsToDriversTypeConverterTests : MapperTests
{
    [TestCase(1, 1)]
    [TestCase(5, 5)]
    [TestCase(10, 10)]
    public void Convert_ShouldReturnCorrectNumberOfDrivers(int numTeams, int numDrivers)
    {
        // Arrange
        var teamsConfig = CreateTeamsAndDriversConfig(numTeams, numDrivers);

        // Act
        var drivers = Mapper.Map<IEnumerable<Driver>>(teamsConfig)?.ToList();

        // Assert
        drivers.Should().NotBeNull();
        drivers.Should().HaveCount(numTeams * numDrivers);
    }

    [TestCase(1, 1)]
    [TestCase(5, 5)]
    [TestCase(10, 10)]
    public void Convert_EachDriverShouldContainCorrectNameAndId(int numTeams, int numDrivers)
    {
        // Arrange
        var teamsConfig = CreateTeamsAndDriversConfig(numTeams, numDrivers);

        // Act
        var drivers = Mapper.Map<IEnumerable<Driver>>(teamsConfig)
            .OrderBy(d => d.Id)
            .ToList();

        // Assert
        var driverConfigs = teamsConfig.SelectMany(t => t.Drivers)
            .OrderBy(d => d.Id)
            .ToList(); 
        
        drivers.Should().NotBeNull();
        drivers.Should().HaveCount(driverConfigs.Count);
        for (var i = 0; i < driverConfigs.Count; i++)
        {
            var driver = drivers[i];
            driver.Id.Should().NotBeNull();
            driver.Name.Should().NotBeNull();

            var config = driverConfigs[i];
            driver.Id.Should().Be(config.Id);
            driver.Name.Should().Be(config.Name);
        }
    }
    
    [TestCase(1, 1)]
    [TestCase(5, 5)]
    [TestCase(10, 10)]
    public void Convert_EachDriverShouldContainCorrectTeam(int numTeams, int numDrivers)
    {
        // Arrange
        var teamsConfig = CreateTeamsAndDriversConfig(numTeams, numDrivers);

        // Act
        var drivers = Mapper.Map<IEnumerable<Driver>>(teamsConfig)?.ToList();

        // Assert
        drivers.Should().NotBeNull();
        foreach (var driver in drivers!)
        {
            driver.Team.Should().NotBeNull();
            var expectedTeam = GetTeamConfigFromDriverId(teamsConfig, driver.Id);
            driver.Team.Id.Should().Be(expectedTeam.Id);
        }
    }

    private IEnumerable<TeamConfig> CreateTeamsAndDriversConfig(int numTeams, int numDrivers)
    {
        var teams = new List<TeamConfig>();
        for (var i = 0; i < numTeams; i++)
        {
            var team = new TeamConfig
            {
                Id = $"Team-{i}",
                Name = $"Team {i}"
            };

            var drivers = new List<DriverConfig>();
            for (var j = 0; j < numDrivers; j++)
            {
                drivers.Add(new DriverConfig
                {
                    Id = $"Driver-{j}-{i}",
                    Name = $"Driver {j} {i}"
                });
            }

            team.Drivers = drivers.ToArray();
            teams.Add(team);
        }

        return teams;
    }

    private TeamConfig GetTeamConfigFromDriverId(IEnumerable<TeamConfig> teamsConfig, string driverId)
    {
        var teamConfigs = teamsConfig.ToArray();
        var driverConfig = teamConfigs.SelectMany(t => t.Drivers)
            .Single(d => d.Id == driverId);

        return teamConfigs.Single(t => t.Drivers.Contains(driverConfig));
    }
}