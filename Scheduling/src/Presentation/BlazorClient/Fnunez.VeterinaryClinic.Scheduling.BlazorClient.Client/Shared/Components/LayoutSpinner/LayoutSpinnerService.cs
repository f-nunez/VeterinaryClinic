namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components;

public class LayoutSpinnerService
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