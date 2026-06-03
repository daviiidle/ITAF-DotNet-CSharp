using System.Net.Http.Json;
using System.Text.Json;
using ITAF.Core.Configuration;

namespace ITAF.API.Clients;

public sealed class ApiClient : IDisposable
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private readonly HttpClient _client;

    public ApiClient(ApiSettings settings)
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri(settings.BaseUrl),
            Timeout = TimeSpan.FromMilliseconds(settings.TimeoutMs)
        };
    }

    public async Task<ApiResponse<T?>> GetAsync<T>(string path, CancellationToken cancellationToken = default)
    {
        using var response = await _client.GetAsync(path, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);
        var payload = string.IsNullOrWhiteSpace(body) ? default : JsonSerializer.Deserialize<T>(body, JsonOptions);

        return new ApiResponse<T?>(response.StatusCode, payload, body);
    }

    public async Task<ApiResponse<TResponse?>> PostAsync<TRequest, TResponse>(
        string path,
        TRequest payload,
        CancellationToken cancellationToken = default)
    {
        using var response = await _client.PostAsJsonAsync(path, payload, JsonOptions, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);
        var responsePayload = string.IsNullOrWhiteSpace(body) ? default : JsonSerializer.Deserialize<TResponse>(body, JsonOptions);

        return new ApiResponse<TResponse?>(response.StatusCode, responsePayload, body);
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}

