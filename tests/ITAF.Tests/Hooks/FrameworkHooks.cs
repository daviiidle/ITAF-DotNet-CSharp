using ITAF.API.Clients;
using ITAF.Core.Configuration;
using ITAF.Core.Logging;
using ITAF.Core.Utilities;
using ITAF.UI.Browser;
using NUnit.Framework;
using Reqnroll;
using Serilog;

namespace ITAF.Tests.Hooks;

[Binding]
public sealed class FrameworkHooks
{
    private readonly ScenarioContext _scenarioContext;

    public FrameworkHooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        LogBootstrapper.Configure();
        Log.Information("Starting ITAF test run");
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        Log.Information("Finished ITAF test run");
        LogBootstrapper.Shutdown();
    }

    [BeforeScenario(Order = 0)]
    public void LoadConfiguration()
    {
        var settings = ConfigurationLoader.Load();
        _scenarioContext[TestContextKeys.Settings] = settings;
    }

    [BeforeScenario("@ui", Order = 10)]
    public async Task StartBrowser()
    {
        var settings = _scenarioContext.Get<FrameworkSettings>(TestContextKeys.Settings);
        var browserSession = new BrowserSession(settings);
        await browserSession.StartAsync();

        _scenarioContext[TestContextKeys.BrowserSession] = browserSession;
    }

    [BeforeScenario("@api", Order = 10)]
    public void StartApiClient()
    {
        var settings = _scenarioContext.Get<FrameworkSettings>(TestContextKeys.Settings);
        _scenarioContext[TestContextKeys.ApiClient] = new ApiClient(settings.Api);
    }

    [AfterScenario("@ui", Order = 0)]
    public async Task CaptureArtifacts()
    {
        if (!_scenarioContext.TryGetValue(TestContextKeys.BrowserSession, out BrowserSession? browserSession))
        {
            return;
        }

        var settings = _scenarioContext.Get<FrameworkSettings>(TestContextKeys.Settings);

        if (_scenarioContext.TestError is not null && settings.Reporting.CaptureScreenshotOnFailure)
        {
            var screenshotDirectory = PathResolver.EnsureDirectory(settings.Reporting.ScreenshotsDirectory);
            var scenarioName = PathResolver.SanitizeFileName(_scenarioContext.ScenarioInfo.Title);
            var screenshotPath = Path.Combine(screenshotDirectory, $"{scenarioName}.png");

            await browserSession.Page.ScreenshotAsync(new() { Path = screenshotPath, FullPage = true });
            TestContext.AddTestAttachment(screenshotPath, "Failure screenshot");
        }
    }

    [AfterScenario("@ui", Order = 100)]
    public async Task StopBrowser()
    {
        if (_scenarioContext.TryGetValue(TestContextKeys.BrowserSession, out BrowserSession? browserSession))
        {
            await browserSession.DisposeAsync();
        }
    }

    [AfterScenario("@api")]
    public void StopApiClient()
    {
        if (_scenarioContext.TryGetValue(TestContextKeys.ApiClient, out ApiClient? apiClient))
        {
            apiClient.Dispose();
        }
    }
}

