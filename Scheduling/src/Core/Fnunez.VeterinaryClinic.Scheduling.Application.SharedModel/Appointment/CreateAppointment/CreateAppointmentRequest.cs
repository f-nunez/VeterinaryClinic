using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;

public class CreateAppointmentRequest : BaseRequest
{
    public int AppointmentTypeId { get; set; }
    public int ClientId { get; set; }
    public int ClinicId { get; set; }
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public int RoomId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateTimeOffset StartOn { get; set; }
    public DateTimeOffset EndOn { get; set; }
}