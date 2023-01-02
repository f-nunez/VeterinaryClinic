using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterAppointmentType;

public class GetAppointmentsFilterAppointmentTypeResponse : BaseResponse
{
    public DataGridResponse<AppointmentTypeFilterValueDto>? DataGridResponse { get; set; }

    public GetAppointmentsFilterAppointmentTypeResponse(Guid correlationId)
        : base(correlationId)
    {

    }
}