using System.IO;
using System.Windows;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Services;
using Microsoft.Extensions.Configuration;
using Prism.Ioc;
using Serilog;
using Serilog.Extensions.Logging;

namespace F1Predictions;

public class Startup
{
    private readonly IConfiguration _config;
    
    public Startup() : this(AddConfiguration())
    {
    }

    public Startup(IConfiguration config)
    {
        _config = config;
    }
    
    
    public void Initialize(StartupEventArgs e)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs\\_Log.txt", rollingInterval: RollingInterval.Day)
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

    private static IConfiguration AddConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
    }
}