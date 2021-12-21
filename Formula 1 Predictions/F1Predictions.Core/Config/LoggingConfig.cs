namespace F1Predictions.Core.Config;

public class LoggingConfig
{
    public const string Section = "Logger";
    
    public string LoggerName { get; set; }
    public string LogFile { get; set; }
    public string RollingInterval { get; set; } 
}