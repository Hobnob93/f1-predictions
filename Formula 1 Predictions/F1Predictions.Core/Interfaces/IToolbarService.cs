using F1Predictions.Core.ViewModels;

namespace F1Predictions.Core.Interfaces;

public interface IToolbarService
{
    void Initialize(ToolbarViewModel vm);
    void CloseWindow();
    void MaximizeWindow();
    void MinimizeWindow();
    void RestoreWindow();
    void DragWindow();
}