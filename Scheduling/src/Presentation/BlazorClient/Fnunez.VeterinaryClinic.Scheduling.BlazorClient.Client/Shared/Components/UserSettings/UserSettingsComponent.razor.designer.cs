using Microsoft.AspNetCore.Components;
using Radzen;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.UserSettings;

public partial class UserSettingsComponent : ComponentBase
{
    [Inject]
    private DialogService _dialogService { get; set; }

    public async Task OpenSettingsDialog()
    {
        var options = new SideDialogOptions
        {
            Position = DialogPosition.Right
        };

        var result = await _dialogService
            .OpenSideAsync<UserSettingsDialogComponent>(
                title: "Configurations", options: options);
    }
}