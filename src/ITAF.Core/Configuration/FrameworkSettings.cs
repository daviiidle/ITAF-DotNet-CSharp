namespace ITAF.Core.Configuration;

public sealed class FrameworkSettings
{
    public UiSettings Ui { get; init; } = new();
    public ApiSettings Api { get; init; } = new();
    public ReportingSettings Reporting { get; init; } = new();
}

public sealed class UiSettings
{
    public string BaseUrl { get; init; } = "https://playwright.dev/dotnet";
    public string Browser { get; init; } = "chromium";
    public bool Headless { get; init; } = true;
    public int TimeoutMs { get; init; } = 30000;
    public string Viewport { get; init; } = "1440x900";
}

public sealed class ApiSettings
{
    public string BaseUrl { get; init; } = "https://jsonplaceholder.typicode.com";
    public int TimeoutMs { get; init; } = 30000;
}

public sealed class ReportingSettings
{
    public string ScreenshotsDirectory { get; init; } = "reports/screenshots";
    public bool CaptureScreenshotOnFailure { get; init; } = true;
}

