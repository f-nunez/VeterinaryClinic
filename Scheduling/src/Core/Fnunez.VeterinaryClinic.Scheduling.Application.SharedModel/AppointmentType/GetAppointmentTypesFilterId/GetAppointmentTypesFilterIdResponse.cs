using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterId;

public class GetAppointmentTypesFilterIdResponse : BaseResponse
{
    public List<string> AppointmentTypeIds { get; set; } = null!;

    public GetAppointmentTypesFilterIdResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}