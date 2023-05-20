namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.Appointments;

public class AddEditAppointmentVm
{
    public Guid AppointmentId { get; set; }
    public int AppointmentTypeDuration { get; set; }
    public int AppointmentTypeId { get; set; }
    public int ClientId { get; set; }
    public string ClientFullName { get; set; } = string.Empty;
    public int ClinicId { get; set; }
    public string ClinicName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int DoctorId { get; set; }
    public bool IsConfirmed { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public byte[]? PatientPhotoData { get; set; }
    public int RoomId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime StartOn { get; set; }
    public DateTime EndOn { get; set; }
}