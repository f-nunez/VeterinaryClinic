namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.Appointments;

public class AppointmentDetailVm
{
    public Guid AppointmentId { get; set; }
    public string AppointmentTypeName { get; set; } = string.Empty;
    public string ClientFullName { get; set; } = string.Empty;
    public string ClinicName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DoctorFullName { get; set; } = string.Empty;
    public DateTime EndOn { get; set; }
    public bool IsActive { get; set; }
    public bool IsConfirmed { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public byte[]? PatientPhotoData { get; set; }
    public string RoomName { get; set; } = string.Empty;
    public DateTime StartOn { get; set; }
    public string TimezoneName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}