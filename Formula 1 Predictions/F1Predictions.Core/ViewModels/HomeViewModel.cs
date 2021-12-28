// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using AutoMapper;
using F1Predictions.Core.Config;
using F1Predictions.Core.Models;
using Prism.Mvvm;

namespace F1Predictions.Core.ViewModels;

public class HomeViewModel : BindableBase
{
    private List<Participant> participants;
    private List<Team> teams;
    private List<Driver> drivers;

    public HomeViewModel(IMapper mapper, PredictionConfig predictionConfig, ChampionshipConfig championshipConfig)
    {
        participants = mapper.Map<List<Participant>>(predictionConfig.Participants);
        teams = mapper.Map<List<Team>>(championshipConfig.Competitors);
        drivers = mapper.Map<List<Driver>>(championshipConfig.Competitors);
    }


    public List<Participant> Participants
    {
        get => participants;
        set => SetProperty(ref participants, value);
    }

    public List<Team> Teams
    {
        get => teams;
        set => SetProperty(ref teams, value);
    }

    public List<Driver> Drivers
    {
        get => drivers;
        set => SetProperty(ref drivers, value);
    }
}