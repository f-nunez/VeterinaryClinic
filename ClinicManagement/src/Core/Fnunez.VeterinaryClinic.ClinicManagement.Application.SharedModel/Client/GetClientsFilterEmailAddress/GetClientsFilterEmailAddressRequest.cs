using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterEmailAddress;

public class GetClientsFilterEmailAddressRequest : BaseRequest
{
    public string EmailAddressFilterValue { get; set; } = null!;
}