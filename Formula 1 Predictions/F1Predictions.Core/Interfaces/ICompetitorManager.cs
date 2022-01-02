using F1Predictions.Core.Models;

namespace F1Predictions.Core.Interfaces;

public interface ICompetitorManager
{
    IEnumerable<Team> GetTeams();
    IEnumerable<Driver> GetDrivers();

    Team GetTeamById(string id);
    Driver GetDriverById(string id);

    ICompetitor GetCompetitorById(string id);
    ICompetitor GetCompetitorByName(string name);
}