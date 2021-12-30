// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Collections.ObjectModel;
using System.Windows.Input;
using F1Predictions.Core.Events;
using F1Predictions.Core.Extensions;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace F1Predictions.Core.ViewModels;

public class HomeViewModel : BindableBase
{
    private readonly IEventAggregator eventAggregator;
    
    private ObservableCollection<Participant> participants;
    private ObservableCollection<Team> teams;
    private ObservableCollection<Driver> drivers;

    public HomeViewModel(IParticipantsManager participantsManager, ICompetitorManager competitorManager, IEventAggregator eventAggregator)
    {
        this.eventAggregator = eventAggregator;
        
        Participants = participantsManager.GetParticipants().ToObservableCollection();
        Teams = competitorManager.GetTeams().ToObservableCollection();
        Drivers = competitorManager.GetDrivers().ToObservableCollection();

        BeginCommand = new DelegateCommand(BeginAction);
    }

    
    public ICommand BeginCommand { get; }

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


    private void BeginAction()
    {
        eventAggregator.GetEvent<SectionChangedEvent>().Publish(true);
    }
}