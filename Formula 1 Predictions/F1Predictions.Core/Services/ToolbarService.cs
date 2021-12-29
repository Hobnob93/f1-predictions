using F1Predictions.Core.Config;
using F1Predictions.Core.Extensions;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.ViewModels;

namespace F1Predictions.Core.Services;

public class ToolbarService : IToolbarService
{
    private readonly IWindowService window;
    private readonly ToolbarConfig config;

    private ToolbarViewModel viewModel;
    
    public ToolbarService(IWindowService window, ToolbarConfig config)
    {
        this.window = window;
        this.config = config;
    }


    public void Initialize(ToolbarViewModel vm)
    {
        viewModel = vm;
        vm.AppName = config.Title;
        vm.F1ImageRef = config.F1Logo;
        vm.BackgroundColor = config.BackgroundColor;
    }

    public void CloseWindow()
    {
        window.Close();
    }

    public void MaximizeWindow()
    {
        window.Maximize();
        FlipRestoreMaximize();
    }

    public void MinimizeWindow()
    {
        window.Minimize();
    }

    public void RestoreWindow()
    {
        window.Restore();
        FlipRestoreMaximize();
    }

    public void DragWindow()
    {
        window.Drag();
    }

    private void FlipRestoreMaximize()
    {
        viewModel.MaximizeVisibility = viewModel.MaximizeVisibility.FlipCollapsed();
        viewModel.RestoreVisibility = viewModel.RestoreVisibility.FlipCollapsed();
    }
}