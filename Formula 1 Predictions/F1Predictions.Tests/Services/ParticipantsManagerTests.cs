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
public class ParticipantsManagerTests : MapperTests
{
    [Test]
    public void Ctor_ShouldUseMapperToMapParticipantsFromConfig()
    {
        // Arrange
        var config = CreatePredictionConfig();
        var mapper = new Mock<IMapper>();

        // Act
        var sut = CreateSut(mapper.Object, config);

        // Assert
        mapper.Verify(m => m.Map<IEnumerable<Participant>>(config.Participants), Times.Once);
    }
    
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    public void GetTeams_ShouldReturnAllMappedTeams(int numParticipants)
    {
        // Arrange
        var config = CreatePredictionConfig(numParticipants);
        var sut = CreateSut(Mapper, config);

        // Act
        var participants = sut.GetParticipants()?.ToList();

        // Assert
        participants.Should().NotBeNull();
        participants.Should().HaveCount(numParticipants);
        foreach (var participant in participants)
        {
            var participantConfig = GetParticipantConfigFromParticipantName(config.Participants, participant.Name);
            participant.Name.Should().Be(participantConfig.Name);
        }
    }
    
    private ParticipantsManager CreateSut(IMapper mapper, PredictionConfig config)
    {
        return new ParticipantsManager(mapper, config);
    }
}