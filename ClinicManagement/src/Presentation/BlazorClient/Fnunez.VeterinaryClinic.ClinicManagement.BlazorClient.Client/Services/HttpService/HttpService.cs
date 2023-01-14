using System.Text;
using System.Text.Json;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public class HttpService : IHttpService
{
    private readonly string _apiUrl;
    private readonly HttpClient _httpClient;

    public HttpService(HttpClient httpClient)
    {
        if (httpClient is null)
            throw new ArgumentNullException(nameof(httpClient));

        if (httpClient.BaseAddress is null)
            throw new ArgumentNullException(nameof(httpClient.BaseAddress));

        _httpClient = httpClient;
        _apiUrl = _httpClient.BaseAddress.ToString();
    }

    public async Task<T?> HttpDeleteAsync<T>(string uri)
        where T : class
    {
        var result = await _httpClient.DeleteAsync($"{_apiUrl}{uri}");

        if (!result.IsSuccessStatusCode)
            return null;

        return await FromHttpResponseMessageAsync<T>(result);
    }

    public async Task<T?> HttpDeleteAsync<T>(string uri, object id)
        where T : class
    {
        var result = await _httpClient.DeleteAsync($"{_apiUrl}{uri}/{id}");

        if (!result.IsSuccessStatusCode)
            return null;

        return await FromHttpResponseMessageAsync<T>(result);
    }

    public async Task<T?> HttpGetAsync<T>(string uri)
        where T : class
    {
        var result = await _httpClient.GetAsync($"{_apiUrl}{uri}");

        if (!result.IsSuccessStatusCode)
            return null;

        return await FromHttpResponseMessageAsync<T>(result);
    }

    public async Task<string?> HttpGetAsync(string uri)
    {
        var result = await _httpClient.GetAsync($"{_apiUrl}{uri}");

        if (!result.IsSuccessStatusCode)
            return null;

        return await result.Content.ReadAsStringAsync();
    }

    public async Task<T?> HttpPostAsync<T>(string uri, object dataToSend)
        where T : class
    {
        var content = ToJson(dataToSend);
        var result = await _httpClient.PostAsync($"{_apiUrl}{uri}", content);

        if (!result.IsSuccessStatusCode)
            return null;

        return await FromHttpResponseMessageAsync<T>(result);
    }

    public async Task<T?> HttpPutAsync<T>(string uri, object dataToSend)
        where T : class
    {
        var content = ToJson(dataToSend);
        var result = await _httpClient.PutAsync($"{_apiUrl}{uri}", content);

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