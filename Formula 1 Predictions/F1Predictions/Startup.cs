using System.Windows;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Services;
using Prism.Ioc;
using Serilog;
using Serilog.Extensions.Logging;

namespace F1Predictions;

public class Startup
{
    public Startup()
    {
        
    }

    public void Initialize(StartupEventArgs e)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs\\_Log.txt", rollingInterval: RollingInterval.Hour)
            .CreateLogger();
    }
    
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        SetupLogging(containerRegistry);
            
        containerRegistry.Register<IWindowService, WindowService>();
        containerRegistry.Register<IToolbarService, ToolbarService>();
    }

    private void SetupLogging(IContainerRegistry containerRegistry)
    {
        var appLogger = new SerilogLoggerProvider(Log.Logger).CreateLogger("App");
        containerRegistry.RegisterInstance(appLogger);
    }
}