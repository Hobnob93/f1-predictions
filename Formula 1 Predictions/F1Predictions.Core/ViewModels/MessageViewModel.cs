using Prism.Mvvm;

namespace F1Predictions.Core.ViewModels;

public class MessageViewModel : BindableBase
{
    private string _message = "Welcome To Prediction Results for F1 2021!";
    
    public string Message
    {
        get => _message;
        set => SetProperty(ref _message, value);
    }
}