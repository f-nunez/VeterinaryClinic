using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentById;

public class GetAppointmentByIdResponse : BaseResponse
{
    public AppointmentDto Appointment { get; set; } = new();

    public GetAppointmentByIdResponse(Guid correlationId) : base(correlationId)
    {
    }
}