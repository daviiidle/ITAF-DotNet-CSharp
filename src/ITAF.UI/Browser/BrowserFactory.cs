using ITAF.Core.Configuration;
using Microsoft.Playwright;

namespace ITAF.UI.Browser;

public sealed class BrowserFactory
{
    private readonly UiSettings _settings;

    public BrowserFactory(UiSettings settings)
    {
        _settings = settings;
    }

    public async Task<IBrowser> LaunchAsync(IPlaywright playwright)
    {
        var launchOptions = new BrowserTypeLaunchOptions
        {
            Headless = _settings.Headless
        };

        return _settings.Browser.ToLowerInvariant() switch
        {
            "firefox" => await playwright.Firefox.LaunchAsync(launchOptions),
            "webkit" => await playwright.Webkit.LaunchAsync(launchOptions),
            _ => await playwright.Chromium.LaunchAsync(launchOptions)
        };
    }
}

