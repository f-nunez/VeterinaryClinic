namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.UserSettings;

using Blazored.LocalStorage;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Settings;

public class UserSettingsComponentService : IUserSettingsComponentService
{
    private const string DefaultTimeZoneId = "Pacific Standard Time (Mexico)";
    private const string DefaultLanguageCulture = "en-US";
    private readonly ICookieSetting _cookieSetting;
    private readonly ILocalStorageService _localStorageService;
    private UserSettings? _userSettings { get; set; }

    public UserSettingsComponentService(
        ICookieSetting cookieSetting,
        ILocalStorageService localStorageService)
    {
        _cookieSetting = cookieSetting;
        _localStorageService = localStorageService;
    }

    public async Task<UserSettings> GetSettingsAsync()
    {
        _userSettings = await _localStorageService
            .GetItemAsync<UserSettings>(_cookieSetting.UserSettingsKey);

        if (_userSettings is null)
        {
            _userSettings = new UserSettings
            {
                LanguageCultureCode = DefaultLanguageCulture,
                TimeZoneId = DefaultTimeZoneId
            };

            await SaveSettingsAsync(_userSettings);
        }

        return _userSettings;
    }

    public async Task ResetSettingsAsync()
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    public async Task SaveSettingsAsync(UserSettings userSettings)
    {
        await _localStorageService
            .SetItemAsync(_cookieSetting.UserSettingsKey, userSettings);

        _userSettings = userSettings;
    }
}