using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterName;

public class GetAppointmentTypesFilterNameResponse : BaseResponse
{
    public List<string> AppointemntTypeNames { get; set; } = new();

    public GetAppointmentTypesFilterNameResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}