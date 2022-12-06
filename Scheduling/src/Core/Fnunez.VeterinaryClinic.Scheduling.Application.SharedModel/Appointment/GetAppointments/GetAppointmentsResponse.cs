using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointments;

public class GetAppointmentsResponse : BaseResponse
{
    public List<AppointmentDto> Appointments { get; set; } = new();
    public int Count { get; set; }

    public GetAppointmentsResponse(Guid correlationId) : base(correlationId)
    {
    }
}
