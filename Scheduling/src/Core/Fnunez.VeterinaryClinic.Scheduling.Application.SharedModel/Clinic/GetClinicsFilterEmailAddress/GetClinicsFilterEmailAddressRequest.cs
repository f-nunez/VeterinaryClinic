using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicsFilterEmailAddress;

public class GetClinicsFilterEmailAddressRequest : BaseRequest
{
    public string EmailAddressFilterValue { get; set; } = null!;
}