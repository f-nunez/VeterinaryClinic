using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterEmailAddress;

public class GetClientsFilterEmailAddressResponse : BaseResponse
{
    public List<string> ClientEmailAddresses { get; set; } = new();

    public GetClientsFilterEmailAddressResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}