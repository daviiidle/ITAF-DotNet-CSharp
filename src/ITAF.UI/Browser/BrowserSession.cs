using ITAF.Core.Configuration;
using Microsoft.Playwright;

namespace ITAF.UI.Browser;

public sealed class BrowserSession : IAsyncDisposable
{
    private readonly FrameworkSettings _settings;

    public BrowserSession(FrameworkSettings settings)
    {
        _settings = settings;
    }

    public IPlaywright Playwright { get; private set; } = default!;
    public IBrowser Browser { get; private set; } = default!;
    public IBrowserContext Context { get; private set; } = default!;
    public IPage Page { get; private set; } = default!;

    public async Task StartAsync()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await new BrowserFactory(_settings.Ui).LaunchAsync(Playwright);

        var viewport = ParseViewport(_settings.Ui.Viewport);
        Context = await Browser.NewContextAsync(new BrowserNewContextOptions
        {
            BaseURL = _settings.Ui.BaseUrl,
            ViewportSize = viewport
        });

        Context.SetDefaultTimeout(_settings.Ui.TimeoutMs);
        Page = await Context.NewPageAsync();
    }

    public async ValueTask DisposeAsync()
    {
        if (Context is not null)
        {
            await Context.CloseAsync();
        }

        if (Browser is not null)
        {
            await Browser.CloseAsync();
        }

        Playwright?.Dispose();
    }

    private static ViewportSize ParseViewport(string viewport)
    {
        var parts = viewport.Split('x', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (parts.Length != 2 || !int.TryParse(parts[0], out var width) || !int.TryParse(parts[1], out var height))
        {
            return new ViewportSize { Width = 1440, Height = 900 };
        }

        return new ViewportSize { Width = width, Height = height };
    }
}

