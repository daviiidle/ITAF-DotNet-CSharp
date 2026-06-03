using Microsoft.Playwright;

namespace ITAF.UI.Pages;

public abstract class BasePage
{
    protected BasePage(IPage page)
    {
        Page = page;
    }

    protected IPage Page { get; }

    public Task<string> GetTitleAsync()
    {
        return Page.TitleAsync();
    }
}

