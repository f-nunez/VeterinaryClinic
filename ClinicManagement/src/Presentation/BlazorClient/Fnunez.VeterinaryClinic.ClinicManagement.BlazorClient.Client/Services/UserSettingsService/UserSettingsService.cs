using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.Language;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.TimeZone;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.UserSettings;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public class UserSettingsService : IUserSettingsService
{
    private readonly ILanguageComponentService _languageComponentService;
    private readonly ITimeZoneComponentService _timeZoneComponentService;
    private readonly IUserSettingsComponentService _userSettingsComponentService;

    public UserSettingsService(
        ILanguageComponentService languageComponentService,
        ITimeZoneComponentService timeZoneComponentService,
        IUserSettingsComponentService userSettingsComponentService)
    {
        _languageComponentService = languageComponentService;
        _timeZoneComponentService = timeZoneComponentService;
        _userSettingsComponentService = userSettingsComponentService;
    }

    public async Task<string> GetLanguageCultureCode()
    {
        var userSettings = await _userSettingsComponentService
            .GetSettingsAsync();

        var language = _languageComponentService.GetLanguage(userSettings.LanguageCultureCode);

        return language.CultureCode;
    }

    public async Task<string> GetTimeZoneNameAsync()
    {
        var userSettings = await _userSettingsComponentService
            .GetSettingsAsync();

        var timeZone = _timeZoneComponentService
            .GetTimeZone(userSettings.TimeZoneId);

        return timeZone.DisplayName;
    }

    public async Task<int> GetUtcOffsetInMinutesAsync()
    {
        var userSettings = await _userSettingsComponentService
            .GetSettingsAsync();

        var timeZone = _timeZoneComponentService
            .GetTimeZone(userSettings.TimeZoneId);

        return timeZone.UtcOffsetInMinutes;
    }
}