using F1Predictions.Core.Extensions;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.ViewModels;

namespace F1Predictions.Core.Services;

public class ToolbarService : IToolbarService
{
    private readonly IWindowService _window;
    
    public ToolbarService(IWindowService window)
    {
        _window = window;
    }

    public ToolbarViewModel ViewModel { get; set; }
    

    public void CloseWindow()
    {
        _window.Close();
    }

    public void MaximizeWindow()
    {
        _window.Maximize();
        FlipRestoreMaximize();
    }

    public void MinimizeWindow()
    {
        _window.Minimize();
    }

    public void RestoreWindow()
    {
        _window.Restore();
        FlipRestoreMaximize();
    }

    public void DragWindow()
    {
        _window.Drag();
    }

    private void FlipRestoreMaximize()
    {
        ViewModel.MaximizeVisibility = ViewModel.MaximizeVisibility.FlipCollapsed();
        ViewModel.RestoreVisibility = ViewModel.RestoreVisibility.FlipCollapsed();
    }
}