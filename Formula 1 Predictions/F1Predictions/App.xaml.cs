using System.Windows;
using F1Predictions.Modules.Content;
using F1Predictions.Modules.Progress;
using F1Predictions.Modules.Toolbar;
using F1Predictions.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;

namespace F1Predictions
{
    public partial class App : PrismApplication
    {
        private readonly IStartup startup;

        public App() : this(new Startup())
        {
            
        }

        public App(IStartup startup)
        {
            this.startup = startup;
        }
        
        
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            startup.RegisterTypes(containerRegistry);
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<ShellWindow>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ToolbarModule>();
            moduleCatalog.AddModule<ProgressModule>();
            moduleCatalog.AddModule<ContentModule>();
        }
    }
}