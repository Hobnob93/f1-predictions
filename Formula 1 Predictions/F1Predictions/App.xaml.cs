using System.Windows;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Services;
using F1Predictions.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Serilog;
using Serilog.Extensions.Logging;

namespace F1Predictions
{
    public partial class App : PrismApplication
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs\\_Log.txt", rollingInterval: RollingInterval.Hour)
                .CreateLogger();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var appLogger = new SerilogLoggerProvider(Log.Logger).CreateLogger("App");
            containerRegistry.RegisterInstance(appLogger);
            
            containerRegistry.Register<IWindowService, WindowService>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<ShellWindow>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ToolbarModule.Module>();
        }
    }
}