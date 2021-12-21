using System.Windows;
using System.Windows.Input;
using F1Predictions.Core.Interfaces;
using Prism.Commands;
using Prism.Mvvm;

namespace F1Predictions.Core.ViewModels;

public class ToolbarViewModel : BindableBase
{
    private readonly IToolbarService _toolbar;
    
    private string _appName;
    private string _f1ImageRef;
    private string _backgroundColor;
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

        _toolbar.Initialize(this);
    }
    
    
    public ICommand CloseCommand { get; }
    public ICommand MaximizeCommand { get; }
    public ICommand RestoreCommand { get; }
    public ICommand MinimizeCommand { get; }
    public ICommand WindowDragCommand { get; }
    
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
    
    public string BackgroundColor
    {
        get => _backgroundColor;
        set => SetProperty(ref _backgroundColor, value);
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