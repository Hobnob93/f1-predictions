// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using Prism.Mvvm;

namespace F1Predictions.Core.ViewModels;

public class MessageViewModel : BindableBase
{
    private string message = "Welcome To Prediction Results for F1 2021!";
    
    public string Message
    {
        get => message;
        set => SetProperty(ref message, value);
    }
}