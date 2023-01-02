using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.UpdateAppointment;

public class UpdateAppointmentRequest : BaseRequest
{
    public Guid AppointmentId { get; set; }
    public int AppointmentTypeId { get; set; }
    public int DoctorId { get; set; }
    public int RoomId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateTimeOffset StartOn { get; set; }
    public DateTimeOffset EndOn { get; set; }
}