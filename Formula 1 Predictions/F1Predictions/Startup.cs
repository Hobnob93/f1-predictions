using System;
using System.IO;
using System.Windows;
using F1Predictions.Core.Config;
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
    
    
    public void InitializeLogging()
    {
        var logger = _config.GetSection(LoggingConfig.Section).Get<LoggingConfig>();
        var interval = Enum.Parse<RollingInterval>(logger.RollingInterval, true);
        
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File(logger.LogFile, rollingInterval: interval)
            .CreateLogger();
    }
    
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        RegisterLogging(containerRegistry);
        
        // WPF Services
        containerRegistry.Register<IWindowService, WindowService>();
        containerRegistry.Register<IToolbarService, ToolbarService>();

        // App Options
        containerRegistry.RegisterInstance(_config.GetSection(AppConfig.Section).Get<AppConfig>());
        containerRegistry.RegisterInstance(_config.GetSection(ToolbarConfig.Section).Get<ToolbarConfig>());
        containerRegistry.RegisterInstance(_config.GetSection(PredictionConfig.Section).Get<PredictionConfig>());
        containerRegistry.RegisterInstance(_config.GetSection(ChampionshipConfig.Section).Get<ChampionshipConfig>());
    }

    private void RegisterLogging(IContainerRegistry containerRegistry)
    {
        var logger = _config.GetSection(LoggingConfig.Section).Get<LoggingConfig>();
        var appLogger = new SerilogLoggerProvider(Log.Logger).CreateLogger(logger.LoggerName);
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