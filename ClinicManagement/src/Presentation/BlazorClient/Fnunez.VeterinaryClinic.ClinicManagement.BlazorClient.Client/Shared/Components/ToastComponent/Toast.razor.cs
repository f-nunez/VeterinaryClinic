using Microsoft.AspNetCore.Components;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.ToastComponent;

public partial class Toast
{
    [Inject]
    ToastService? ToastService { get; set; }

    protected override void OnInitialized()
    {
        if (ToastService is null)
            return;

        ToastService.RefreshRequested += Refresh;
        base.OnInitialized();
    }
    private void Close()
    {
        if (ToastService is null)
            return;

        ToastService.IsShow = false;
    }

    private string AlertType
    {
        get
        {
            if (ToastService?.ToastType == ToastType.Error)
            {
                return "alert-danger";
            }
            else if (ToastService?.ToastType == ToastType.Info)
            {
                return "alert-info";
            }
            else if (ToastService?.ToastType == ToastType.Warning)
            {
                return "alert-warning";
            }

            return "alert-success";
        }
    }

    private void Refresh()
    {
        StateHasChanged();
    }
}