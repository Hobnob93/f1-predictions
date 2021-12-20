using System.Windows.Input;
using F1Predictions.Core.Interfaces;
using Prism.Commands;
using Prism.Mvvm;

namespace F1Predictions.Core.ViewModels;

public class ToolbarViewModel : BindableBase
{
    private readonly IWindowService _window;
    
    private string _appName = "F1 Predictions 2021";
    private string _f1ImageRef = "/Images/F1.png";

    public ToolbarViewModel(IWindowService window)
    {
        _window = window;

        CloseCommand = new DelegateCommand(_window.Close);
        MaximizeCommand = new DelegateCommand(_window.Maximize);
        RestoreCommand = new DelegateCommand(_window.Restore);
        MinimizeCommand = new DelegateCommand(_window.Minimize);
        WindowDragCommand = new DelegateCommand(_window.Drag);
    }
    
    
    public ICommand CloseCommand { get; private set; }
    public ICommand MaximizeCommand { get; private set; }
    public ICommand RestoreCommand { get; private set; }
    public ICommand MinimizeCommand { get; private set; }
    public ICommand WindowDragCommand { get; private set; }
    
    public string AppName
    {
        get => _appName;
        set => SetProperty(ref _appName, value);
    }

    public string F1ImageRef
    {
        get => _f1ImageRef;
        set => SetProperty(ref _f1ImageRef, value);
    }
}