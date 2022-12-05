using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClients;

public class GetClientsResponse : BaseResponse
{
    public List<ClientDto> Clients { get; set; } = new List<ClientDto>();
    public int Count { get; set; }

    public GetClientsResponse(Guid correlationId) : base(correlationId)
    {
    }
}