using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinics;

public class GetClinicsResponse : BaseResponse
{
    public DataGridResponse<ClinicDto>? DataGridResponse { get; set; }

    public GetClinicsResponse(Guid correlationId) : base(correlationId)
    {
    }
}