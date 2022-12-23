using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicsFilterEmailAddress;

public class GetClinicsFilterEmailAddressResponse : BaseResponse
{
    public List<string> ClinicEmailAddresses { get; set; } = new();

    public GetClinicsFilterEmailAddressResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}