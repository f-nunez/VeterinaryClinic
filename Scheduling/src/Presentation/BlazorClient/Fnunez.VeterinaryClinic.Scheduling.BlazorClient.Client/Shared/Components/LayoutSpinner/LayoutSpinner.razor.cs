using Microsoft.AspNetCore.Components;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components;

public partial class LayoutSpinnerComponent : ComponentBase
{
    [Inject]
    private LayoutSpinnerService _spinnerService { get; set; } = null!;

    protected bool IsVisible { get; set; }

    protected override void OnInitialized()
    {
        _spinnerService.OnShow += ShowSpinner;
        _spinnerService.OnHide += HideSpinner;
    }

    public void ShowSpinner()
    {
        IsVisible = true;

        if (IsVisible)
            StateHasChanged();
    }

    public void HideSpinner()
    {
        IsVisible = false;

        if (!IsVisible)
            StateHasChanged();
    }
}