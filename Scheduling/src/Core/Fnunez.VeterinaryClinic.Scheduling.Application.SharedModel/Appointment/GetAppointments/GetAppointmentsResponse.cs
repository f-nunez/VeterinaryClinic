using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointments;

public class GetAppointmentsResponse : BaseResponse
{
    public DataGridResponse<AppointmentDto>? DataGridResponse { get; set; }

    public GetAppointmentsResponse(Guid correlationId) : base(correlationId)
    {
    }
}