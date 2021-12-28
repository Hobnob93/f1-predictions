// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Collections.ObjectModel;
using AutoMapper;
using F1Predictions.Core.Config;
using F1Predictions.Core.Extensions;
using F1Predictions.Core.Models;
using Prism.Mvvm;

namespace F1Predictions.Core.ViewModels;

public class HomeViewModel : BindableBase
{
    private ObservableCollection<Participant> participants;
    private ObservableCollection<Team> teams;
    private ObservableCollection<Driver> drivers;

    public HomeViewModel(IMapper mapper, PredictionConfig predictionConfig, ChampionshipConfig championshipConfig)
    {
        Participants = mapper.Map<List<Participant>>(predictionConfig.Participants).ToObservableCollection();
        Teams = mapper.Map<List<Team>>(championshipConfig.Competitors).ToObservableCollection();
        Drivers = mapper.Map<List<Driver>>(championshipConfig.Competitors).ToObservableCollection();
    }


    public ObservableCollection<Participant> Participants
    {
        get => participants;
        set => SetProperty(ref participants, value);
    }

    public ObservableCollection<Team> Teams
    {
        get => teams;
        set => SetProperty(ref teams, value);
    }

    public ObservableCollection<Driver> Drivers
    {
        get => drivers;
        set => SetProperty(ref drivers, value);
    }
}