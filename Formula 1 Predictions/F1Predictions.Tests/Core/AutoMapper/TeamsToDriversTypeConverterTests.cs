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
        var teamsConfig = CreateChampionshipConfig(numTeams, numDrivers).Competitors;

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
        var teamsConfig = CreateChampionshipConfig(numTeams, numDrivers).Competitors;

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
        var teamsConfig = CreateChampionshipConfig(numTeams, numDrivers).Competitors;

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
}