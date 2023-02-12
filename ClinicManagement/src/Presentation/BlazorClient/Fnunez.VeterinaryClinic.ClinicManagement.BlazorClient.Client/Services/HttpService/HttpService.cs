using System.Text;
using System.Text.Json;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Settings;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public class HttpService : IHttpService
{
    private readonly string _localEndpoint;
    private readonly HttpClient _httpClient;

    public HttpService(
        IBackendForFrontendSetting bffSetting,
        HttpClient httpClient)
    {
        if (httpClient is null)
            throw new ArgumentNullException(nameof(httpClient));

        if (httpClient.BaseAddress is null)
            throw new ArgumentNullException(nameof(httpClient.BaseAddress));

        _httpClient = httpClient;
        _localEndpoint = bffSetting.LocalEndpointToRouteRemoteApiByReverseProxy;
    }

    public async Task<T?> HttpDeleteAsync<T>(string uri)
        where T : class
    {
        var result = await _httpClient.DeleteAsync($"{_localEndpoint}/{uri}");

        if (!result.IsSuccessStatusCode)
            return null;

        return await FromHttpResponseMessageAsync<T>(result);
    }

    public async Task<T?> HttpDeleteAsync<T>(string uri, object id)
        where T : class
    {
        var result = await _httpClient.DeleteAsync($"{_localEndpoint}/{uri}/{id}");

        if (!result.IsSuccessStatusCode)
            return null;

        return await FromHttpResponseMessageAsync<T>(result);
    }

    public async Task<T?> HttpGetAsync<T>(string uri)
        where T : class
    {
        var result = await _httpClient.GetAsync($"{_localEndpoint}/{uri}");

        if (!result.IsSuccessStatusCode)
            return null;

        return await FromHttpResponseMessageAsync<T>(result);
    }

    public async Task<string?> HttpGetAsync(string uri)
    {
        var result = await _httpClient.GetAsync($"{_localEndpoint}/{uri}");

        if (!result.IsSuccessStatusCode)
            return null;

        return await result.Content.ReadAsStringAsync();
    }

    public async Task<T?> HttpPostAsync<T>(string uri, object dataToSend)
        where T : class
    {
        var content = ToJson(dataToSend);
        var result = await _httpClient.PostAsync($"{_localEndpoint}/{uri}", content);

        if (!result.IsSuccessStatusCode)
            return null;

        return await FromHttpResponseMessageAsync<T>(result);
    }

    public async Task<T?> HttpPutAsync<T>(string uri, object dataToSend)
        where T : class
    {
        var content = ToJson(dataToSend);
        var result = await _httpClient.PutAsync($"{_localEndpoint}/{uri}", content);

        if (!result.IsSuccessStatusCode)
            return null;

        return await FromHttpResponseMessageAsync<T>(result);
    }


    private StringContent ToJson(object obj)
    {
        return new StringContent(
            JsonSerializer.Serialize(obj),
            Encoding.UTF8, "application/json"
        );
    }

    private async Task<T?> FromHttpResponseMessageAsync<T>(
        HttpResponseMessage result)
    {
        return JsonSerializer.Deserialize<T>(
            await result.Content.ReadAsStringAsync(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );
    }
}