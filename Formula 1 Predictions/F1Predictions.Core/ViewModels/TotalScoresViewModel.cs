using System.Collections.ObjectModel;
using System.Windows.Input;
using F1Predictions.Core.Constants;
using F1Predictions.Core.Extensions;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace F1Predictions.Core.ViewModels;

public class TotalScoresViewModel : BindableBase, INavigationAware
{
    private readonly IParticipantsManager participantManager;
    private readonly IScoreManager scoreManager;

    private ObservableCollection<ParticipantScore> participantScores;
    

    public TotalScoresViewModel(IParticipantsManager participantManager, IScoreManager scoreManager)
    {
        this.participantManager = participantManager;
        this.scoreManager = scoreManager;
    }

    public ObservableCollection<ParticipantScore> ParticipantScores
    {
        get => participantScores;
        set => SetProperty(ref participantScores, value);
    }


    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        var participants = participantManager.GetParticipants();
        ParticipantScores = participants.Select(p => new ParticipantScore
        {
            Participant = p,
            Score = scoreManager.GetTotalScore(p)
        })
            .OrderByDescending(ps => ps.Score)
            .ToObservableCollection();
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
        
    }
}