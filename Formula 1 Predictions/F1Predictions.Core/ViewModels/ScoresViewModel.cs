using F1Predictions.Core.Constants;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Prism.Mvvm;
using Prism.Regions;

namespace F1Predictions.Core.ViewModels;

public class ScoresViewModel : BindableBase, INavigationAware
{
    private readonly IParticipantsManager participantManager;

    private Participant[] participants;
    private int sectionId;
    private int questionId;
    
    public ScoresViewModel(IParticipantsManager participantManager)
    {
        this.participantManager = participantManager;
    }


    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        navigationContext.Parameters.TryGetValue(Navigation.SectionId, out sectionId);
        navigationContext.Parameters.TryGetValue(Navigation.QuestionId, out questionId);

        participants = participantManager.GetParticipants().ToArray();
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        navigationContext.Parameters.TryGetValue(Navigation.SectionId, out int otherSectionId);
        navigationContext.Parameters.TryGetValue(Navigation.QuestionId, out int otherQuestionId);

        return otherSectionId == sectionId && otherQuestionId == questionId;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
        
    }
}