using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypes;

public class GetAppointmentTypesResponse : BaseResponse
{
    public DataGridResponse<AppointmentTypeDto>? DataGridResponse { get; set; }

    public GetAppointmentTypesResponse(Guid correlationId) : base(correlationId)
    {
    }
}