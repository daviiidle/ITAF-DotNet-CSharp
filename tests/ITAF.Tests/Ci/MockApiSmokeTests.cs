using FluentAssertions;
using ITAF.API.Clients;
using ITAF.API.Models;
using ITAF.API.TestData;
using ITAF.Core.Configuration;
using NUnit.Framework;

namespace ITAF.Tests.Ci;

[TestFixture]
[Category("ci")]
public sealed class MockApiSmokeTests
{
    [Test]
    public async Task Api_client_can_read_existing_post_from_ci_mock()
    {
        var settings = ConfigurationLoader.Load();
        using var apiClient = new ApiClient(settings.Api);

        var response = await apiClient.GetAsync<Post>("/posts/1");

        response.IsSuccess.Should().BeTrue();
        response.Body.Should().NotBeNull();
        response.Body!.Id.Should().Be(1);
    }

    [Test]
    public async Task Api_client_can_create_post_against_ci_mock()
    {
        var settings = ConfigurationLoader.Load();
        using var apiClient = new ApiClient(settings.Api);
        var post = PostFactory.Create();

        var response = await apiClient.PostAsync<Post, Post>("/posts", post);

        response.IsSuccess.Should().BeTrue();
        response.Body.Should().NotBeNull();
        response.Body!.Title.Should().Be(post.Title);
    }
}

