using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterAddress;

public class GetClinicsFilterAddressRequest : BaseRequest
{
    public string AddressFilterValue { get; set; } = null!;
}