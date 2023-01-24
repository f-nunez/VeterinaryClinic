using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatientsFilterClient;

public class GetPatientsFilterClientResponse : BaseResponse
{
    public DataGridResponse<ClientFilterValueDto>? DataGridResponse { get; set; }

    public GetPatientsFilterClientResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}