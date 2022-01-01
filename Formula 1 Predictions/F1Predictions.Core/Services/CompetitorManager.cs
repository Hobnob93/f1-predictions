using AutoMapper;
using F1Predictions.Core.Config;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services;

public class CompetitorManager : ICompetitorManager
{
    private readonly Team[] teams;
    private readonly Driver[] drivers;
    private readonly ICompetitor[] competitors;
    
    public CompetitorManager(IMapper mapper, ChampionshipConfig championshipConfig)
    {
        teams = mapper.Map<IEnumerable<Team>>(championshipConfig.Competitors).ToArray();
        drivers = mapper.Map<IEnumerable<Driver>>(championshipConfig.Competitors).ToArray();
        competitors = teams.Cast<ICompetitor>().Concat(drivers).ToArray();
    }
    
    
    public IEnumerable<Team> GetTeams()
    {
        return teams;
    }

    public IEnumerable<Driver> GetDrivers()
    {
        return drivers;
    }

    public Team GetTeamById(string id)
    {
        return teams.SingleOrDefault(t => t.Id == id);
    }

    public Team GetTeamByName(string name)
    {
        return teams.SingleOrDefault(t => t.Name == name);
    }

    public Driver GetDriverById(string id)
    {
        return drivers.SingleOrDefault(d => d.Id == id);
    }

    public Driver GetDriverByName(string name)
    {
        return drivers.SingleOrDefault(d => d.Name == name);
    }

    public ICompetitor GetCompetitorById(string id)
    {
        return competitors.SingleOrDefault(d => d.Id == id);
    }
    
    public ICompetitor GetCompetitorByName(string name)
    {
        return competitors.SingleOrDefault(d => d.Name == name);
    }
}