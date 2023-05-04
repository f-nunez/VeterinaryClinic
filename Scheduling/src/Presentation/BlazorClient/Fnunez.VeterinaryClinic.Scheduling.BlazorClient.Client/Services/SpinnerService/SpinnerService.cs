using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.Spinner;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;

public class SpinnerService : ISpinnerService
{
    private readonly ISpinnerComponentService _spinnerComponentService;

    public SpinnerService(ISpinnerComponentService spinnerComponentService)
    {
        _spinnerComponentService = spinnerComponentService;
    }

    public void Hide()
    {
        _spinnerComponentService.Hide();
    }

    public void Show()
    {
        _spinnerComponentService.Show();
    }
}