namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public interface IUserSettingsService
{
    public Task<string> GetLanguageCultureCode();
    public Task<string> GetTimeZoneNameAsync();
    public Task<int> GetUtcOffsetInMinutesAsync();
}