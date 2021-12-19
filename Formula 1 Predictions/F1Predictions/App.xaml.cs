using System.Windows;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Services;
using F1Predictions.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;

namespace F1Predictions
{
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IWindowService, WindowService>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<ShellWindow>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ToolbarModule.ToolbarModule>();
        }
    }
}