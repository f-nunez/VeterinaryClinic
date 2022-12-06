using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentById;

public class GetAppointmentByIdRequest : BaseRequest
{
    public Guid AppointmentId { get; set; }
    public Guid ScheduleId { get; set; }
}