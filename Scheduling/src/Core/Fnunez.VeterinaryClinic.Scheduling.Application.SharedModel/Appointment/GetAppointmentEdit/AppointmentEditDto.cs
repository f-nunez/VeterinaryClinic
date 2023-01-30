namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentEdit;

public class AppointmentEditDto
{
    public Guid AppointmentId { get; set; }
    public int AppointmentTypeId { get; set; }
    public int ClientId { get; set; }
    public string ClientFullName { get; set; } = string.Empty;
    public int ClinicId { get; set; }
    public string ClinicName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int DoctorId { get; set; }
    public string DoctorFullName { get; set; } = string.Empty;
    public DateTimeOffset EndOn { get; set; }
    public bool IsConfirmed { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public byte[] PatientPhotoData { get; set; } = null!;
    public int RoomId { get; set; }
    public string RoomName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateTimeOffset StartOn { get; set; }
}