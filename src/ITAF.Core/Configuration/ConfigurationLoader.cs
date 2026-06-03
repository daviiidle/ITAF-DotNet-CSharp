using Microsoft.Extensions.Configuration;

namespace ITAF.Core.Configuration;

public static class ConfigurationLoader
{
    public static FrameworkSettings Load(string? basePath = null)
    {
        basePath ??= AppContext.BaseDirectory;

        var environment = Environment.GetEnvironmentVariable("ITAF_ENVIRONMENT") ?? "local";

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables(prefix: "ITAF_")
            .Build();

        return configuration.Get<FrameworkSettings>() ?? new FrameworkSettings();
    }
}

