using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.Spinner;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.UserSettings;

public partial class UserSettingsDialogComponent : ComponentBase
{
    protected string LanguageCultureCode { get; private set; }

    protected string TimeZoneId { get; private set; }

    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    private NavigationManager _navigationManager { get; set; }

    [Inject]
    private ISpinnerComponentService _spinnerComponentService { get; set; }

    [Inject]
    private IUserSettingsComponentService _userSettingsComponentService { get; set; }
    
    [Inject]
    protected IStringLocalizer<UserSettingsDialogComponent> StringLocalizer { get; set; }

    protected override async Task OnInitializedAsync()
    {
        UserSettings userSettings = await _userSettingsComponentService
            .GetSettingsAsync();

        LanguageCultureCode = userSettings.LanguageCultureCode;

        TimeZoneId = userSettings.TimeZoneId;
    }

    protected async void SaveSettings()
    {
        _spinnerComponentService.Show();

        var userSettings = new UserSettings
        {
            LanguageCultureCode = LanguageCultureCode,
            TimeZoneId = TimeZoneId
        };

        await _userSettingsComponentService.SaveSettingsAsync(userSettings);

        _navigationManager.NavigateTo(_navigationManager.Uri, true);
    }

    protected void LanguageChanged(string value)
    {
        LanguageCultureCode = value;
    }

    protected void TimeZoneChanged(string value)
    {
        TimeZoneId = value;
    }
}