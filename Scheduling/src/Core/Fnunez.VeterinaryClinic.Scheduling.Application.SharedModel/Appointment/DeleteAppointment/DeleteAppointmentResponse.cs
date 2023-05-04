using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.DeleteAppointment;

public class DeleteAppointmentResponse : BaseResponse
{
    public DeleteAppointmentResponse(Guid correlationId) : base(correlationId)
    {
    }
}