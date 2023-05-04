using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterName;

public class GetClinicsFilterNameResponse : BaseResponse
{
    public List<string> ClinicNames { get; set; } = new();

    public GetClinicsFilterNameResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}