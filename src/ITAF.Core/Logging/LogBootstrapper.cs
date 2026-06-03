using Serilog;

namespace ITAF.Core.Logging;

public static class LogBootstrapper
{
    private static bool _configured;

    public static void Configure()
    {
        if (_configured)
        {
            return;
        }

        Directory.CreateDirectory("reports");

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("reports/itaf-.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        _configured = true;
    }

    public static void Shutdown()
    {
        Log.CloseAndFlush();
        _configured = false;
    }
}

