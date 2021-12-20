using System.Windows;
using F1Predictions.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace F1Predictions.Core.Services;

public class WindowService : IWindowService
{
    private ILogger _logger;

    public WindowService(ILogger logger)
    {
        _logger = logger;
    }
    
    
    public void Close()
    {
        _logger.LogInformation("Closing Window");
        Application.Current.Shutdown();
    }

    public void Maximize()
    {
        _logger.LogInformation("Maximising Window");
        Application.Current.MainWindow!.WindowState = WindowState.Maximized;
    }

    public void Restore()
    {
        _logger.LogInformation("Restoring Window");
        Application.Current.MainWindow!.WindowState = WindowState.Normal;
    }

    public void Minimize()
    {
        _logger.LogInformation("Minimizing Window");
        Application.Current.MainWindow!.WindowState = WindowState.Minimized;
    }

    public void Drag()
    {
        _logger.LogInformation("Dragging Window");
        Application.Current.MainWindow!.DragMove();
    }
}