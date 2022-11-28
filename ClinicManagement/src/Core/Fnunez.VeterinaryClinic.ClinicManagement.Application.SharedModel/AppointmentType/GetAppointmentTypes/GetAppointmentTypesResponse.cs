using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypes;

public class GetAppointmentTypesResponse : BaseResponse
{
    public List<AppointmentTypeDto> AppointmentTypes { get; set; } = new List<AppointmentTypeDto>();
    public int Count { get; set; }

    public GetAppointmentTypesResponse(Guid correlationId) : base(correlationId)
    {
    }
}