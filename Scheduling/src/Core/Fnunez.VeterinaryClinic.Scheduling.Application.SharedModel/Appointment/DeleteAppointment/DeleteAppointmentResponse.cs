using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.DeleteAppointment;

public class DeleteAppointmentResponse : BaseResponse
{
    public ScheduleDto Schedule { get; set; } = new();

    public DeleteAppointmentResponse(Guid correlationId) : base(correlationId)
    {
    }
}