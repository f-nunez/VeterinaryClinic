namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentAdd;

public class AppointmentAddDto
{
    public int ClientId { get; set; }
    public string ClientFullName { get; set; } = string.Empty;
    public int ClinicId { get; set; }
    public string ClinicName { get; set; } = string.Empty;
    public int PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public byte[] PatientPhotoData { get; set; } = null!;
}