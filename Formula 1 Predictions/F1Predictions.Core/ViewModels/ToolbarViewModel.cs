// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Windows;
using System.Windows.Input;
using F1Predictions.Core.Interfaces;
using Prism.Commands;
using Prism.Mvvm;

namespace F1Predictions.Core.ViewModels;

public class ToolbarViewModel : BindableBase
{
    private string appName;
    private string f1ImageRef;
    private string backgroundColor;
    private Visibility restoreVisibility = Visibility.Collapsed;
    private Visibility maximizeVisibility = Visibility.Visible;

    public ToolbarViewModel(IToolbarService toolbar)
    {
        CloseCommand = new DelegateCommand(toolbar.CloseWindow);
        MaximizeCommand = new DelegateCommand(toolbar.MaximizeWindow);
        RestoreCommand = new DelegateCommand(toolbar.RestoreWindow);
        MinimizeCommand = new DelegateCommand(toolbar.MinimizeWindow);
        WindowDragCommand = new DelegateCommand(toolbar.DragWindow);

        toolbar.Initialize(this);
    }


    public ICommand CloseCommand { get; }
    public ICommand MaximizeCommand { get; }
    public ICommand RestoreCommand { get; }
    public ICommand MinimizeCommand { get; }
    public ICommand WindowDragCommand { get; }
    
    public string AppName
    {
        get => appName;
        set => SetProperty(ref appName, value);
    }

    public string F1ImageRef
    {
        get => f1ImageRef;
        set => SetProperty(ref f1ImageRef, value);
    }
    
    public string BackgroundColor
    {
        get => backgroundColor;
        set => SetProperty(ref backgroundColor, value);
    }

    public Visibility RestoreVisibility
    {
        get => restoreVisibility;
        set => SetProperty(ref restoreVisibility, value);
    }
    
    public Visibility MaximizeVisibility
    {
        get => maximizeVisibility;
        set => SetProperty(ref maximizeVisibility, value);
    }
}