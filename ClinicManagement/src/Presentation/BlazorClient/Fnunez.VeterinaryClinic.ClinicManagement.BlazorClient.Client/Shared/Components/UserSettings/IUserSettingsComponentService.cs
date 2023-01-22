namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.UserSettings;

public interface IUserSettingsComponentService
{
    public Task<UserSettings> GetSettingsAsync();
    public Task ResetSettingsAsync();
    public Task SaveSettingsAsync(UserSettings userSettings);
}