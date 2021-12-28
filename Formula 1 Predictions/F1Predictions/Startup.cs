using System;
using System.IO;
using System.Windows;
using AutoMapper;
using F1Predictions.Core.AutoMapper;
using F1Predictions.Core.Config;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Services;
using Microsoft.Extensions.Configuration;
using Prism.Ioc;
using Serilog;
using Serilog.Extensions.Logging;

namespace F1Predictions;

public class Startup : IStartup
{
    private readonly IConfiguration config;
    
    public Startup() : this(AddConfiguration())
    {
    }

    public Startup(IConfiguration config)
    {
        this.config = config;
        
        var logger = config.GetSection(LoggingConfig.Section).Get<LoggingConfig>();
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
        RegisterAutoMapper(containerRegistry);

        // App Options
        containerRegistry.RegisterInstance(config.GetSection(AppConfig.Section).Get<AppConfig>());
        containerRegistry.RegisterInstance(config.GetSection(ToolbarConfig.Section).Get<ToolbarConfig>());
        containerRegistry.RegisterInstance(config.GetSection(PredictionConfig.Section).Get<PredictionConfig>());
        containerRegistry.RegisterInstance(config.GetSection(ChampionshipConfig.Section).Get<ChampionshipConfig>());
        
        // WPF Services
        containerRegistry.Register<IWindowService, WindowService>();
        containerRegistry.Register<IToolbarService, ToolbarService>();
        
        // Manager Services
        containerRegistry.RegisterSingleton<IParticipantsManager, ParticipantsManager>();
        containerRegistry.RegisterSingleton<ICompetitorManager, CompetitorManager>();
    }

    private void RegisterLogging(IContainerRegistry containerRegistry)
    {
        var logger = config.GetSection(LoggingConfig.Section).Get<LoggingConfig>();
        var appLogger = new SerilogLoggerProvider(Log.Logger).CreateLogger(logger.LoggerName);
        containerRegistry.RegisterInstance(appLogger);
    }

    private void RegisterAutoMapper(IContainerRegistry containerRegistry)
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ConfigProfile>();
        });

        containerRegistry.RegisterInstance(mapperConfig.CreateMapper());
    }

    private static IConfiguration AddConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
    }
}