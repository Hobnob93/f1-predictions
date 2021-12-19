using Prism.Mvvm;

namespace F1Predictions.Core.ViewModels;

public class ToolbarViewModel : BindableBase
{
    private string appName = "F1 Predictions 2021";
    public string AppName
    {
        get => appName;
        set => SetProperty(ref appName, value);
    }

    private string f1ImageRef = "/Images/F1.png";
    public string F1ImageRef
    {
        get => f1ImageRef;
        set => SetProperty(ref f1ImageRef, value);
    }
}