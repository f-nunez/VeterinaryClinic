using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicsFilterAddress;

public class GetClinicsFilterAddressRequest : BaseRequest
{
    public string AddressFilterValue { get; set; } = null!;
}