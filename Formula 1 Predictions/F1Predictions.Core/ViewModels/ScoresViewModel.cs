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

public class ScoresViewModel : BindableBase, INavigationAware
{
    private readonly IParticipantsManager participantManager;

    private ObservableCollection<ParticipantScore> participantScores;
    private int sectionId;
    private int questionId;
    
    public ScoresViewModel(IParticipantsManager participantManager)
    {
        this.participantManager = participantManager;
        
        BigSubtractCommand = new DelegateCommand<ParticipantScore>(BigSubtractAction);
        SmallSubtractCommand = new DelegateCommand<ParticipantScore>(SmallSubtractAction);
        BigAddCommand = new DelegateCommand<ParticipantScore>(BigAddAction);
        SmallAddCommand = new DelegateCommand<ParticipantScore>(SmallAddAction);
    }

    public ObservableCollection<ParticipantScore> ParticipantScores
    {
        get => participantScores;
        set => SetProperty(ref participantScores, value);
    }
    
    public ICommand BigSubtractCommand { get; }
    public ICommand SmallSubtractCommand { get; }
    public ICommand BigAddCommand { get; }
    public ICommand SmallAddCommand { get; }


    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        navigationContext.Parameters.TryGetValue(Navigation.SectionId, out sectionId);
        navigationContext.Parameters.TryGetValue(Navigation.QuestionId, out questionId);

        var participants = participantManager.GetParticipants();
        participantScores = participants.Select(p => new ParticipantScore
        {
            Participant = p,
            Score = participantManager.GetScoreForQuestion(p, sectionId, questionId)
        }).ToObservableCollection();
        
        participantScores.Refresh();
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        navigationContext.Parameters.TryGetValue(Navigation.SectionId, out int otherSectionId);
        navigationContext.Parameters.TryGetValue(Navigation.QuestionId, out int otherQuestionId);

        return otherSectionId == sectionId && otherQuestionId == questionId;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
        foreach (var ps in participantScores)
        {
            participantManager.SetScoreForQuestion(ps.Participant, sectionId, questionId, ps.Score);
        }
    }

    private void BigAddAction(ParticipantScore ps)
    {
        ManipulateScore(ps, 5);
    }
    
    private void SmallAddAction(ParticipantScore ps)
    {
        ManipulateScore(ps, 1);
    }
    
    private void BigSubtractAction(ParticipantScore ps)
    {
        ManipulateScore(ps, -5);
    }
    
    private void SmallSubtractAction(ParticipantScore ps)
    {
        ManipulateScore(ps, -1);
    }

    private void ManipulateScore(ParticipantScore ps, int scoreChange)
    {
        ps.Score += scoreChange;
        
        participantScores.Refresh();
    }
}