using F1Predictions.Core.Models;

namespace F1Predictions.Core.Interfaces;

public interface ICompetitorManager
{
    IEnumerable<Team> GetTeams();
    IEnumerable<Driver> GetDrivers();

    Team GetTeam(string id);
    Driver GetDriver(string id);
}