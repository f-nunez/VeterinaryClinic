using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientsFilterClient;

public class GetPatientsFilterClientResponse : BaseResponse
{
    public DataGridResponse<ClientFilterValueDto>? DataGridResponse { get; set; }

    public GetPatientsFilterClientResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}