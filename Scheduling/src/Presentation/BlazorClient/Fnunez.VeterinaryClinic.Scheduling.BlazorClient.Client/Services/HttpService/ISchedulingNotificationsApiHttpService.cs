namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;

public interface ISchedulingNotificationsApiHttpService
{
    Task<T?> HttpDeleteAsync<T>(string uri) where T : class;
    Task<T?> HttpDeleteAsync<T>(string uri, object id) where T : class;
    Task<T?> HttpGetAsync<T>(string uri) where T : class;
    Task<string?> HttpGetAsync(string uri);
    Task<T?> HttpPostAsync<T>(string uri, object dataToSend) where T : class;
    Task<T?> HttpPutAsync<T>(string uri, object dataToSend) where T : class;
}