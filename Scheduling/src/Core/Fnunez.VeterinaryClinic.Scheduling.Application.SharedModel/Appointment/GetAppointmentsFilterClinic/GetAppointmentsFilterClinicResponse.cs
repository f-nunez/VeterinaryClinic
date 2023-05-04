using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterClinic;

public class GetAppointmentsFilterClinicResponse : BaseResponse
{
    public DataGridResponse<ClinicFilterValueDto>? DataGridResponse { get; set; }

    public GetAppointmentsFilterClinicResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}