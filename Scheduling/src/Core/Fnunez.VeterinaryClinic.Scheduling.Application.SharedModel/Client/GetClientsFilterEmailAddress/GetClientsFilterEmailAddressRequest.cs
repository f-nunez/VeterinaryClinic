using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterEmailAddress;

public class GetClientsFilterEmailAddressRequest : BaseRequest
{
    public string EmailAddressFilterValue { get; set; } = null!;
}