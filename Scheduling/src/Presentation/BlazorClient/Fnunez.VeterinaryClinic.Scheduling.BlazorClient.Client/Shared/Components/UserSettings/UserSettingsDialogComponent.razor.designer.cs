using Microsoft.AspNetCore.Components;
using Radzen;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.UserSettings;

public partial class UserSettingsDialogComponent : ComponentBase
{
    protected string TimeZoneId { get; private set; }
    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    private IUserSettingsComponentService _userSettingsComponentService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        UserSettings userSettings = await _userSettingsComponentService.GetSettingsAsync();

        TimeZoneId = userSettings.TimeZoneId;
    }

    protected void ResetSettings()
    {
        _userSettingsComponentService.ResetSettingsAsync();

        _dialogService.CloseSide();
    }

    protected async Task TimeZoneChanged(string value)
    {
        var userSettings = await _userSettingsComponentService.GetSettingsAsync();

        userSettings.TimeZoneId = value;

        await _userSettingsComponentService.SaveSettingsAsync(userSettings);
    }
}