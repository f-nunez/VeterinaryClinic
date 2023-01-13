using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterId;

public class GetAppointmentTypesFilterIdResponse : BaseResponse
{
    public List<string> AppointmentTypeIds { get; set; } = null!;

    public GetAppointmentTypesFilterIdResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}