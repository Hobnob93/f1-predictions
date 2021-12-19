namespace F1Predictions.Core.Interfaces;

public interface IWindowService
{
    void Close();
    void Maximize();
    void Restore();
    void Minimize();
}