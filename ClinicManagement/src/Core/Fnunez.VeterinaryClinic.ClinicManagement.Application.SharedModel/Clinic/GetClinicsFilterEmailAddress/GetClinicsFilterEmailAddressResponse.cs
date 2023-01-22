using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterEmailAddress;

public class GetClinicsFilterEmailAddressResponse : BaseResponse
{
    public List<string> ClinicEmailAddresses { get; set; } = new();

    public GetClinicsFilterEmailAddressResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}