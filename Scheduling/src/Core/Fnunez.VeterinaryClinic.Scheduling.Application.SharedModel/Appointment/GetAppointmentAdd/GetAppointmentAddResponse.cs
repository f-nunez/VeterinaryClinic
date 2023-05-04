using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentAdd;

public class GetAppointmentAddResponse : BaseResponse
{
    public AppointmentAddDto? Appointment { get; set; }
    public GetAppointmentAddResponse(Guid correlationId) : base(correlationId)
    {
    }
}