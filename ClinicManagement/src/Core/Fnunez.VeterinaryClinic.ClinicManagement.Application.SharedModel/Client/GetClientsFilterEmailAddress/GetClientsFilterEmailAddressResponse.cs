using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterEmailAddress;

public class GetClientsFilterEmailAddressResponse : BaseResponse
{
    public List<string> ClientEmailAddresses { get; set; } = new();

    public GetClientsFilterEmailAddressResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}