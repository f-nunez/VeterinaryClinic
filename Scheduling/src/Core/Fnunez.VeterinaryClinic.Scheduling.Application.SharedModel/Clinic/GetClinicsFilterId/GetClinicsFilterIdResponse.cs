using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicsFilterId;

public class GetClinicsFilterIdResponse : BaseResponse
{
    public List<string> ClinicIds { get; set; } = new();

    public GetClinicsFilterIdResponse(Guid correlationId) : base(correlationId)
    {
    }
}