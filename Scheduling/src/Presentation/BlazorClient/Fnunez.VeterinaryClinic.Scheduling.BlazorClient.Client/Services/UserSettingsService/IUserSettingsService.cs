namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;

public interface IUserSettingsService
{
    public Task<int> GetUtcOffsetInMinutesAsync();
    public Task<string> GetTimeZoneNameAsync();
}