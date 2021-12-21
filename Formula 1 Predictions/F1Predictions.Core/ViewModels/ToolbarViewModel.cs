using System.Windows;
using System.Windows.Input;
using F1Predictions.Core.Interfaces;
using Prism.Commands;
using Prism.Mvvm;

namespace F1Predictions.Core.ViewModels;

public class ToolbarViewModel : BindableBase
{
    private readonly IToolbarService _toolbar;
    
    private string _appName = "F1 Predictions 2021";
    private string _f1ImageRef = "/Images/F1.png";
    private Visibility _restoreVisibility = Visibility.Collapsed;
    private Visibility _maximizeVisibility = Visibility.Visible;

    public ToolbarViewModel(IToolbarService toolbar)
    {
        _toolbar = toolbar;

        CloseCommand = new DelegateCommand(_toolbar.CloseWindow);
        MaximizeCommand = new DelegateCommand(_toolbar.MaximizeWindow);
        RestoreCommand = new DelegateCommand(_toolbar.RestoreWindow);
        MinimizeCommand = new DelegateCommand(_toolbar.MinimizeWindow);
        WindowDragCommand = new DelegateCommand(_toolbar.DragWindow);

        _toolbar.ViewModel = this;
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

    public Visibility RestoreVisibility
    {
        get => _restoreVisibility;
        set => SetProperty(ref _restoreVisibility, value);
    }
    
    public Visibility MaximizeVisibility
    {
        get => _maximizeVisibility;
        set => SetProperty(ref _maximizeVisibility, value);
    }
}