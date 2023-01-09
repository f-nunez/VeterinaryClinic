using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.TimeZone;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.UserSettings;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;

public class UserSettingsService : IUserSettingsService
{
    private readonly IUserSettingsComponentService _userSettingsComponentService;
    private readonly ITimeZoneComponentService _timeZoneComponentService;

    public UserSettingsService(
        IUserSettingsComponentService userSettingsComponentService,
        ITimeZoneComponentService timeZoneComponentService)
    {
        _userSettingsComponentService = userSettingsComponentService;
        _timeZoneComponentService = timeZoneComponentService;
    }

    public async Task<int> GetUtcOffsetInMinutesAsync()
    {
        var userSettings = await _userSettingsComponentService
            .GetSettingsAsync();

        var timeZone = _timeZoneComponentService
            .GetTimeZone(userSettings.TimeZoneId);

        return timeZone.UtcOffsetInMinutes;
    }

    public async Task<string> GetTimeZoneNameAsync()
    {
        var userSettings = await _userSettingsComponentService
            .GetSettingsAsync();

        var timeZone = _timeZoneComponentService
            .GetTimeZone(userSettings.TimeZoneId);

        return timeZone.DisplayName;
    }
}