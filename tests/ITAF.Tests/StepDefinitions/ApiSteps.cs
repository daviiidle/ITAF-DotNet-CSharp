using System.Net;
using FluentAssertions;
using ITAF.API.Clients;
using ITAF.API.Models;
using ITAF.API.TestData;
using ITAF.Tests.Hooks;
using Reqnroll;

namespace ITAF.Tests.StepDefinitions;

[Binding]
public sealed class ApiSteps
{
    private readonly ScenarioContext _scenarioContext;

    public ApiSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Given("I have a generated post payload")]
    public void GivenIHaveAGeneratedPostPayload()
    {
        _scenarioContext[TestContextKeys.CreatedPost] = PostFactory.Create();
    }

    [When("I request post {int}")]
    public async Task WhenIRequestPost(int postId)
    {
        var apiClient = _scenarioContext.Get<ApiClient>(TestContextKeys.ApiClient);
        var response = await apiClient.GetAsync<Post>($"/posts/{postId}");

        _scenarioContext[TestContextKeys.ApiResponse] = response;
    }

    [When("I submit the generated post")]
    public async Task WhenISubmitTheGeneratedPost()
    {
        var apiClient = _scenarioContext.Get<ApiClient>(TestContextKeys.ApiClient);
        var post = _scenarioContext.Get<Post>(TestContextKeys.CreatedPost);
        var response = await apiClient.PostAsync<Post, Post>("/posts", post);

        _scenarioContext[TestContextKeys.ApiResponse] = response;
    }

    [Then("the API response should be successful")]
    public void ThenTheApiResponseShouldBeSuccessful()
    {
        var response = _scenarioContext.Get<ApiResponse<Post?>>(TestContextKeys.ApiResponse);

        response.IsSuccess.Should().BeTrue($"status code was {(int)response.StatusCode} {response.StatusCode}");
    }

    [Then("the post response should have id {int}")]
    public void ThenThePostResponseShouldHaveId(int expectedId)
    {
        var response = _scenarioContext.Get<ApiResponse<Post?>>(TestContextKeys.ApiResponse);

        response.Body.Should().NotBeNull();
        response.Body!.Id.Should().Be(expectedId);
    }

    [Then("the created post should include the submitted title")]
    public void ThenTheCreatedPostShouldIncludeTheSubmittedTitle()
    {
        var submittedPost = _scenarioContext.Get<Post>(TestContextKeys.CreatedPost);
        var response = _scenarioContext.Get<ApiResponse<Post?>>(TestContextKeys.ApiResponse);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Body.Should().NotBeNull();
        response.Body!.Title.Should().Be(submittedPost.Title);
    }
}

