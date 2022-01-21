using System;
using System.IO;
using System.Windows;
using AutoMapper;
using DryIoc;
using F1Predictions.Core.AutoMapper;
using F1Predictions.Core.Config;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prism.DryIoc;
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
        // App Options
        containerRegistry.RegisterInstance(config.GetSection(AppConfig.Section).Get<AppConfig>());
        containerRegistry.RegisterInstance(config.GetSection(ToolbarConfig.Section).Get<ToolbarConfig>());
        containerRegistry.RegisterInstance(config.GetSection(PredictionConfig.Section).Get<PredictionConfig>());
        containerRegistry.RegisterInstance(config.GetSection(ChampionshipConfig.Section).Get<ChampionshipConfig>());
        
        // Setting up complex dependencies
        RegisterLogging(containerRegistry);
        RegisterAutoMapper(containerRegistry);
        RegisterGoogleSheets(containerRegistry);
        
        // WPF Services
        containerRegistry.Register<IWindowService, WindowService>();
        containerRegistry.Register<IToolbarService, ToolbarService>();
        containerRegistry.Register<IQuestionFactory, QuestionFactory>();
        
        // Manager Services
        containerRegistry.RegisterSingleton<IParticipantsManager, ParticipantsManager>();
        containerRegistry.RegisterSingleton<ICompetitorManager, CompetitorManager>();
        containerRegistry.RegisterSingleton<ISectionManager, SectionManager>();
        containerRegistry.RegisterSingleton<IProgressService, ProgressService>();
        containerRegistry.RegisterSingleton<IAnswerStore, AnswerStore>();
        containerRegistry.RegisterSingleton<IPredictionStore, PredictionStore>();
        
        // Data Services
        containerRegistry.Register<IGoogleSheets, GoogleSheets>();
    }

    private void RegisterLogging(IContainerRegistry containerRegistry)
    {
        var logger = config.GetSection(LoggingConfig.Section).Get<LoggingConfig>();
        var appLogger = new SerilogLoggerProvider(Log.Logger).CreateLogger(logger.LoggerName);
        containerRegistry.RegisterInstance(appLogger);
    }

    private void RegisterAutoMapper(IContainerRegistry containerRegistry)
    {
        var container = containerRegistry.GetContainer();
        
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ConfigProfile>();
            cfg.ConstructServicesUsing(type => ActivatorUtilities.CreateInstance(container, type));
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

    private void RegisterGoogleSheets(IContainerRegistry containerRegistry)
    {
        using var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read);
        var credential = GoogleCredential.FromStream(stream)
            .CreateScoped(SheetsService.Scope.Spreadsheets);

        var appConfig = containerRegistry.GetContainer().GetService<AppConfig>();
        var clientService = new SheetsService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = appConfig.Name
        });

        containerRegistry.RegisterInstance(clientService);
    }
}