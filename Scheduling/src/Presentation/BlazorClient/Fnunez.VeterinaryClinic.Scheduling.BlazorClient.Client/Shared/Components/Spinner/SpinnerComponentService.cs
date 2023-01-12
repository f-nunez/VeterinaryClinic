namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.Spinner;

public class SpinnerComponentService : ISpinnerComponentService
{
    public event Action? OnShow;
    public event Action? OnHide;

    public void Show()
    {
        OnShow?.Invoke();
    }

    public void Hide()
    {
        OnHide?.Invoke();
    }
}