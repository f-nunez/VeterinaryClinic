namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.Spinner;

public interface ISpinnerComponentService
{
    public event Action? OnHide;
    public event Action? OnShow;
    public void Hide();
    public void Show();
}