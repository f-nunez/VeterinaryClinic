using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.UpdateAppointment;

public class UpdateAppointmentResponse : BaseResponse
{
    public AppointmentDto Appointment { get; set; } = new();

    public UpdateAppointmentResponse(Guid correlationId) : base(correlationId)
    {
    }
}