using Prism.Mvvm;

namespace F1Predictions.Core.ViewModels;

public class ToolbarViewModel : BindableBase
{
    private string _appName = "F1 Predictions 2021";
    public string AppName
    {
        get => _appName;
        set => SetProperty(ref _appName, value);
    }

    private string _f1ImageRef = "/Images/F1.png";
    public string F1ImageRef
    {
        get => _f1ImageRef;
        set => SetProperty(ref _f1ImageRef, value);
    }
}