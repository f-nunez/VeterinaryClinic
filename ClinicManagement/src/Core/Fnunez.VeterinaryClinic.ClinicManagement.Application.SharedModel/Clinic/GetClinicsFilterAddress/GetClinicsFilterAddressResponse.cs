using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterAddress;

public class GetClinicsFilterAddressResponse : BaseResponse
{
    public List<string> ClinicAddresses { get; set; } = new();

    public GetClinicsFilterAddressResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}