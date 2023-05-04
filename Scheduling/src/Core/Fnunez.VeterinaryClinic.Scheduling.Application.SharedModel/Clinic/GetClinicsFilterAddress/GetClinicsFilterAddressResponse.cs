using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicsFilterAddress;

public class GetClinicsFilterAddressResponse : BaseResponse
{
    public List<string> ClinicAddresses { get; set; } = new();

    public GetClinicsFilterAddressResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}