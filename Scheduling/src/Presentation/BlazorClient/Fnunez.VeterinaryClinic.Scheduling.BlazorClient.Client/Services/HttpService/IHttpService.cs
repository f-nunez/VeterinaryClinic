namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;

public interface IHttpService
{
    public Task<T?> HttpDeleteAsync<T>(string uri) where T : class;
    public Task<T?> HttpDeleteAsync<T>(string uri, object id) where T : class;
    public Task<T?> HttpGetAsync<T>(string uri) where T : class;
    public Task<string?> HttpGetAsync(string uri);
    public Task<T?> HttpPostAsync<T>(string uri, object dataToSend) where T : class;
    public Task<T?> HttpPutAsync<T>(string uri, object dataToSend) where T : class;
}