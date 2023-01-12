namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.Appointments;

public class AppointmentVm
{
    public DateTime EndOn { get; set; }
    public Guid Id { get; set; }
    public bool IsConfirmed { get; set; }
    public DateTime StartOn { get; set; }
    public string Title { get; set; } = string.Empty;
}