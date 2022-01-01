using AutoMapper;
using F1Predictions.Core.Config;
using F1Predictions.Core.Models;
using F1Predictions.Core.Services;
using F1Predictions.Tests.Base;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace F1Predictions.Tests.Services;

[TestFixture]
public class CompetitorManagerTests : MapperTests
{
    [Test]
    public void Ctor_ShouldUseMapperToMapTeamsFromConfig()
    {
        // Arrange
        var config = CreateChampionshipConfig();
        var mapper = new Mock<IMapper>();

        // Act
        var sut = CreateSut(mapper.Object, config);

        // Assert
        mapper.Verify(m => m.Map<IEnumerable<Team>>(config.Competitors), Times.Once);
    }
    
    [Test]
    public void Ctor_ShouldUseMapperToMapDriversFromConfig()
    {
        // Arrange
        var config = CreateChampionshipConfig();
        var mapper = new Mock<IMapper>();

        // Act
        var sut = CreateSut(mapper.Object, config);

        // Assert
        mapper.Verify(m => m.Map<IEnumerable<Driver>>(config.Competitors), Times.Once);
    }
    
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    public void GetTeams_ShouldReturnAllMappedTeams(int numTeams)
    {
        // Arrange
        var config = CreateChampionshipConfig(numTeams);
        var sut = CreateSut(Mapper, config);

        // Act
        var teams = sut.GetTeams()?.ToList();

        // Assert
        teams.Should().NotBeNull();
        teams.Should().HaveCount(numTeams);
        foreach (var team in teams)
        {
            var teamConfig = GetTeamConfigFromTeamId(config.Competitors, team.Id);
            team.Id.Should().Be(teamConfig.Id);
        }
    }
    
    [TestCase(100)]
    [Repeat(10)]
    public void GetTeamById_ShouldReturnTeamWithSpecificId(int numTeams)
    {
        // Arrange
        var config = CreateChampionshipConfig(numTeams);
        var sut = CreateSut(Mapper, config);
        var random = new Random();
        var teamId = $"{TeamIdPrefix}{random.Next(0, numTeams)}";

        // Act
        var team = sut.GetTeamById(teamId);

        // Assert
        team.Should().NotBeNull();
        
        var targetTeamConfig = GetTeamConfigFromTeamId(config.Competitors, teamId);
        team.Id.Should().Be(targetTeamConfig.Id);
    }
    
    [TestCase(100)]
    [Repeat(10)]
    public void GetTeamByName_ShouldReturnTeamWithSpecificId(int numTeams)
    {
        // Arrange
        var config = CreateChampionshipConfig(numTeams);
        var sut = CreateSut(Mapper, config);
        var random = new Random();
        var teamName = $"{TeamNamePrefix}{random.Next(0, numTeams)}";

        // Act
        var team = sut.GetTeamByName(teamName);

        // Assert
        team.Should().NotBeNull();
        
        var targetTeamConfig = GetTeamConfigFromTeamId(config.Competitors, team.Id);
        team.Name.Should().Be(targetTeamConfig.Name);
    }
    
    [TestCase(1, 1)]
    [TestCase(5, 2)]
    [TestCase(10, 5)]
    public void GetDrivers_ShouldReturnAllMappedDrivers(int numTeams, int numDrivers)
    {
        // Arrange
        var config = CreateChampionshipConfig(numTeams, numDrivers);
        var sut = CreateSut(Mapper, config);

        // Act
        var drivers = sut.GetDrivers()?.ToList();

        // Assert
        drivers.Should().NotBeNull();
        drivers.Should().HaveCount(numTeams * numDrivers);
        foreach (var driver in drivers)
        {
            var driverConfig = GetDriverConfigFromDriverId(config.Competitors, driver.Id);
            driver.Id.Should().Be(driverConfig.Id);
        }
    }
    
    [TestCase(100, 5)]
    [Repeat(10)]
    public void GetDriverById_ShouldReturnDriverWithSpecificId(int numTeams, int numDrivers)
    {
        // Arrange
        var config = CreateChampionshipConfig(numTeams, numDrivers);
        var sut = CreateSut(Mapper, config);
        var random = new Random();
        var teamNumber = random.Next(0, numTeams);
        var driverNumber = random.Next(0, numDrivers);
        var driverId = $"{DriverIdPrefix}{driverNumber}-{teamNumber}";

        // Act
        var driver = sut.GetDriverById(driverId);

        // Assert
        driver.Should().NotBeNull();
        
        var targetDriverConfig = GetDriverConfigFromDriverId(config.Competitors, driverId);
        driver.Id.Should().Be(targetDriverConfig.Id);
    }
    
    [TestCase(100, 5)]
    [Repeat(10)]
    public void GetDriverByName_ShouldReturnDriverWithSpecificId(int numTeams, int numDrivers)
    {
        // Arrange
        var config = CreateChampionshipConfig(numTeams, numDrivers);
        var sut = CreateSut(Mapper, config);
        var random = new Random();
        var teamNumber = random.Next(0, numTeams);
        var driverNumber = random.Next(0, numDrivers);
        var driverName = $"{DriverNamePrefix}{driverNumber}-{teamNumber}";

        // Act
        var driver = sut.GetDriverByName(driverName);

        // Assert
        driver.Should().NotBeNull();
        
        var targetDriverConfig = GetDriverConfigFromDriverId(config.Competitors, driver.Id);
        driver.Name.Should().Be(targetDriverConfig.Name);
    }

    [TestCase(100, 5)]
    [Repeat(10)]
    public void GetCompetitorById_ShouldReturnDriverOrTeamWithSpecificId(int numTeams, int numDrivers)
    {
        // Arrange
        var config = CreateChampionshipConfig(numTeams, numDrivers);
        var sut = CreateSut(Mapper, config);
        var random = new Random();
        var teamNumber = random.Next(0, numTeams);
        var driverNumber = random.Next(0, numDrivers);
        var isDriver = random.Next(0, 2) == 1;
        var id = isDriver ? $"{DriverIdPrefix}{driverNumber}-{teamNumber}" : $"{TeamIdPrefix}{teamNumber}";

        // Act
        var competitor = sut.GetCompetitorById(id);

        // Assert
        competitor.Should().NotBeNull();

        if (isDriver)
        {
            var targetDriverConfig = GetDriverConfigFromDriverId(config.Competitors, id);
            competitor.Id.Should().Be(targetDriverConfig.Id);
        }
        else
        {
            var targetDriverConfig = GetTeamConfigFromTeamId(config.Competitors, id);
            competitor.Id.Should().Be(targetDriverConfig.Id);
        }
    }

    [TestCase(100, 5)]
    [Repeat(10)]
    public void GetCompetitorByName_ShouldReturnDriverWithSpecificId(int numTeams, int numDrivers)
    {
        // Arrange
        var config = CreateChampionshipConfig(numTeams, numDrivers);
        var sut = CreateSut(Mapper, config);
        var random = new Random();
        var teamNumber = random.Next(0, numTeams);
        var driverNumber = random.Next(0, numDrivers);
        var isDriver = random.Next(0, 2) == 1;
        var name = isDriver ? $"{DriverNamePrefix}{driverNumber}-{teamNumber}" : $"{TeamNamePrefix}{teamNumber}";

        // Act
        var competitor = sut.GetCompetitorByName(name);

        // Assert
        competitor.Should().NotBeNull();
        
        if (isDriver)
        {
            var targetDriverConfig = GetDriverConfigFromDriverId(config.Competitors, competitor.Id);
            competitor.Id.Should().Be(targetDriverConfig.Id);
        }
        else
        {
            var targetDriverConfig = GetTeamConfigFromTeamId(config.Competitors, competitor.Id);
            competitor.Id.Should().Be(targetDriverConfig.Id);
        }
    }

    private CompetitorManager CreateSut(IMapper mapper, ChampionshipConfig config)
    {
        return new CompetitorManager(mapper, config);
    }
}