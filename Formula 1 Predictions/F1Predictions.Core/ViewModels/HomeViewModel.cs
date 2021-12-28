// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using F1Predictions.Core.Models;
using Prism.Mvvm;

namespace F1Predictions.Core.ViewModels;

public class HomeViewModel : BindableBase
{
    private List<Participant> participants;

    public List<Participant> Participants
    {
        get => participants;
        set => SetProperty(ref participants, value);
    }
}