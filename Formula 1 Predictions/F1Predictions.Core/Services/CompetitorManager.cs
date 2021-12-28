using AutoMapper;
using F1Predictions.Core.Config;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services;

public class CompetitorManager : ICompetitorManager
{
    private readonly Team[] teams;
    private readonly Driver[] drivers;

    public CompetitorManager(IMapperBase mapper, ChampionshipConfig championshipConfig)
    {
        teams = mapper.Map<Team[]>(championshipConfig.Competitors);
        drivers = mapper.Map<Driver[]>(championshipConfig.Competitors);
    }
    
    
    public IEnumerable<Team> GetTeams()
    {
        return teams;
    }

    public IEnumerable<Driver> GetDrivers()
    {
        return drivers;
    }

    public Team GetTeam(string id)
    {
        return teams.SingleOrDefault(t => t.Id == id);
    }

    public Driver GetDriver(string id)
    {
        return drivers.SingleOrDefault(d => d.Id == id);
    }
}