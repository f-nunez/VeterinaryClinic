namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.AppointmentTypes;

public class AppointmentTypeVm
{
    public string Code { get; set; } = string.Empty;
    public int? Duration { get; set; }
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}