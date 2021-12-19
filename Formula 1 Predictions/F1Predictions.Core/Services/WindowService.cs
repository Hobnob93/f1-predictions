using System.Windows;
using F1Predictions.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace F1Predictions.Core.Services;

public class WindowService : IWindowService
{
    //private ILogger _logger;
//
    //public WindowService(ILogger<WindowService> logger)
    //{
    //    _logger = logger;
    //}
    
    
    public void Close()
    {
        Application.Current.Shutdown();
    }

    public void Maximize()
    {
        Application.Current.MainWindow!.WindowState = WindowState.Maximized;
    }

    public void Restore()
    {
        Application.Current.MainWindow!.WindowState = WindowState.Normal;
    }

    public void Minimize()
    {
        Application.Current.MainWindow!.WindowState = WindowState.Minimized;
    }
}