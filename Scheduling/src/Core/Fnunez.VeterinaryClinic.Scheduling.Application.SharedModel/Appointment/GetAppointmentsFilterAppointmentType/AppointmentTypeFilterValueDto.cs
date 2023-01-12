namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterAppointmentType;

public class AppointmentTypeFilterValueDto
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public int Duration { get; set; }
    public string Name { get; set; } = string.Empty;
}