using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.DeleteAppointment;

public class DeleteAppointmentRequest : BaseRequest
{
    public Guid AppointmentId { get; set; }
    public Guid ScheduleId { get; set; }
}