using F1Predictions.Core.Config;
using F1Predictions.Core.Extensions;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.ViewModels;

namespace F1Predictions.Core.Services;

public class ToolbarService : IToolbarService
{
    private readonly IWindowService _window;
    private readonly ToolbarConfig _config;

    private ToolbarViewModel _viewModel;
    
    public ToolbarService(IWindowService window, ToolbarConfig config, ChampionshipConfig champ, PredictionConfig pred)
    {
        _window = window;
        _config = config;
    }


    public void Initialize(ToolbarViewModel vm)
    {
        _viewModel = vm;
        vm.AppName = _config.Title;
        vm.F1ImageRef = _config.F1Logo;
        vm.BackgroundColor = _config.BackgroundColor;
    }

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
        _viewModel.MaximizeVisibility = _viewModel.MaximizeVisibility.FlipCollapsed();
        _viewModel.RestoreVisibility = _viewModel.RestoreVisibility.FlipCollapsed();
    }
}