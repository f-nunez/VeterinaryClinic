using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterName;

public class GetAppointmentTypesFilterNameResponse : BaseResponse
{
    public List<string> AppointemntTypeNames { get; set; } = new();

    public GetAppointmentTypesFilterNameResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}