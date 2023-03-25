namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.EmailRequest.Requests;

public class AppointmentDeletedEmailRequest : BaseEmailRequest
{
    public DateTimeOffset AppointmentEndOn { get; set; }
    public Guid AppointmentId { get; set; }
    public DateTimeOffset AppointmentStartOn { get; set; }
    public string? ClientFullName { get; set; }
    public string? ClinicAddress { get; set; }
    public string? ClinicName { get; set; }
    public string? DoctorFullName { get; set; }
    public string? PatientName { get; set; }
}