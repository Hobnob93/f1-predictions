using System.Windows;
using F1Predictions.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;

namespace F1Predictions
{
    public partial class App : PrismApplication
    {
        private readonly Startup _startup;

        public App()
        {
            _startup = new Startup();
        }
        
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            _startup.InitializeLogging();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            _startup.RegisterTypes(containerRegistry);
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