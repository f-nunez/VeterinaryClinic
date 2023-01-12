namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentDetail;

public class AppointmentDetailDto
{
    public Guid AppointmentId { get; set; }
    public string AppointmentTypeName { get; set; } = string.Empty;
    public string ClientFullName { get; set; } = string.Empty;
    public string ClinicName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DoctorFullName { get; set; } = string.Empty;
    public DateTimeOffset EndOn { get; set; }
    public bool IsConfirmed { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public string RoomName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateTimeOffset StartOn { get; set; }
}