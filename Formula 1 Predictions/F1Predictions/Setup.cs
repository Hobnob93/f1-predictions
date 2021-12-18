using F1Predictions.Core.MvvmCross;
using Microsoft.Extensions.Logging;
using MvvmCross.Platforms.Wpf.Core;
using Serilog;
using Serilog.Extensions.Logging;

namespace F1Predictions;

public class Setup : MvxWpfSetup<MvxStartup>
{
    protected override ILoggerProvider? CreateLogProvider()
    {
        return new SerilogLoggerProvider();
    }

    protected override ILoggerFactory? CreateLogFactory()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Trace()
            .CreateLogger();

        return new SerilogLoggerFactory();
    }
}