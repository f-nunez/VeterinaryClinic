namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterClient;

public class ClientFilterValueDto
{
    public int Id { get; set; }
    public string EmailAddress { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}