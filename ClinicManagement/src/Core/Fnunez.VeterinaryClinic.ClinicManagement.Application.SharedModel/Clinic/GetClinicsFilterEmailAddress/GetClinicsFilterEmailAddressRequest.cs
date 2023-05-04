using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterEmailAddress;

public class GetClinicsFilterEmailAddressRequest : BaseRequest
{
    public string EmailAddressFilterValue { get; set; } = null!;
}