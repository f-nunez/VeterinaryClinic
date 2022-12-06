using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;

public class CreateAppointmentResponse : BaseResponse
{
    public AppointmentDto Appointment { get; set; } = new();

    public CreateAppointmentResponse(Guid correlationId) : base(correlationId)
    {
    }
}