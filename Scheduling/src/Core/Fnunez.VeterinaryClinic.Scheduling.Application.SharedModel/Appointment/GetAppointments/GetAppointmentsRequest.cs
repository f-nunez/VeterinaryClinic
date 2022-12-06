using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointments;

public class GetAppointmentsRequest : BaseRequest
{
    public Guid ScheduleId { get; set; }
}