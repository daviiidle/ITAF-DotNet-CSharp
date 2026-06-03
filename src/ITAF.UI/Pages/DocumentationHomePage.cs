using Microsoft.Playwright;

namespace ITAF.UI.Pages;

public sealed class DocumentationHomePage : BasePage
{
    public DocumentationHomePage(IPage page) : base(page)
    {
    }

    public ILocator GetStartedLink => Page.GetByRole(AriaRole.Link, new() { Name = "Get started" }).First;

    public Task OpenAsync()
    {
        return Page.GotoAsync("/");
    }

    public async Task<bool> IsLoadedAsync()
    {
        return await Page.GetByRole(AriaRole.Heading, new() { Name = "Playwright for .NET" }).IsVisibleAsync();
    }
}

