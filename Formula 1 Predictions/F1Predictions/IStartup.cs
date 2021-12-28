using Prism.Ioc;

namespace F1Predictions;

public interface IStartup
{
    void RegisterTypes(IContainerRegistry containerRegistry);
}