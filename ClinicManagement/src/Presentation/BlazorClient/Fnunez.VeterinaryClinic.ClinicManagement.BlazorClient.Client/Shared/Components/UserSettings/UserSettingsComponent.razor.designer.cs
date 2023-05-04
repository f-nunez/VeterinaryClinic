using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.UserSettings;

public partial class UserSettingsComponent : ComponentBase
{
    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    private IStringLocalizer<UserSettingsDialogComponent> _stringLocalizer { get; set; }

    public async Task OpenSettingsDialog()
    {
        var options = new SideDialogOptions
        {
            Position = DialogPosition.Right
        };

        var result = await _dialogService.OpenSideAsync<UserSettingsDialogComponent>(
            title: _stringLocalizer["UserSettingsDialog_Label_Configurations"],
            options: options
        );
    }
}