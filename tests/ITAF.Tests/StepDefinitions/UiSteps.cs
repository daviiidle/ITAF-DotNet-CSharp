using FluentAssertions;
using ITAF.Tests.Hooks;
using ITAF.UI.Browser;
using ITAF.UI.Pages;
using Reqnroll;

namespace ITAF.Tests.StepDefinitions;

[Binding]
public sealed class UiSteps
{
    private readonly ScenarioContext _scenarioContext;

    public UiSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Given("I open the configured UI application")]
    public async Task GivenIOpenTheConfiguredUiApplication()
    {
        var homePage = GetHomePage();
        await homePage.OpenAsync();
    }

    [Then("the documentation home page should be displayed")]
    public async Task ThenTheDocumentationHomePageShouldBeDisplayed()
    {
        var homePage = GetHomePage();
        var isLoaded = await homePage.IsLoadedAsync();

        isLoaded.Should().BeTrue("the configured UI smoke page should load successfully");
    }

    [Then("the page title should contain {string}")]
    public async Task ThenThePageTitleShouldContain(string expectedText)
    {
        var homePage = GetHomePage();
        var title = await homePage.GetTitleAsync();

        title.Should().Contain(expectedText);
    }

    private DocumentationHomePage GetHomePage()
    {
        var browserSession = _scenarioContext.Get<BrowserSession>(TestContextKeys.BrowserSession);
        return new DocumentationHomePage(browserSession.Page);
    }
}

