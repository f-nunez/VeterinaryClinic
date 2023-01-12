using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterClient;

public class GetAppointmentsFilterClientResponse : BaseResponse
{
    public DataGridResponse<ClientFilterValueDto>? DataGridResponse { get; set; }

    public GetAppointmentsFilterClientResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}