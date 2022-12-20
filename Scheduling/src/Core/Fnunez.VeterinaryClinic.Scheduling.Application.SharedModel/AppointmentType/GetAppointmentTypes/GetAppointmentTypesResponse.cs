using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypes;

public class GetAppointmentTypesResponse : BaseResponse
{
    public DataGridResponse<AppointmentTypeDto>? DataGridResponse { get; set; }

    public GetAppointmentTypesResponse(Guid correlationId) : base(correlationId)
    {
    }
}
