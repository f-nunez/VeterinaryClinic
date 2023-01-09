using Microsoft.AspNetCore.Components;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.Spinner;

public partial class SpinnerComponent : ComponentBase
{
    [Inject]
    private ISpinnerComponentService _SpinnerComponentService { get; set; } = null!;

    protected bool IsVisible { get; set; }

    protected override void OnInitialized()
    {
        _SpinnerComponentService.OnShow += ShowSpinner;
        _SpinnerComponentService.OnHide += HideSpinner;
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