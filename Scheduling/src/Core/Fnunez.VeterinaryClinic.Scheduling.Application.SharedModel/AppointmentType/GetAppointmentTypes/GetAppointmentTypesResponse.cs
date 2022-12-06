using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypes;

public class GetAppointmentTypesResponse : BaseResponse
{
    public List<AppointmentTypeDto> AppointmentTypes { get; set; } = new();
    public int Count { get; set; }

    public GetAppointmentTypesResponse(Guid correlationId) : base(correlationId)
    {
    }
}
