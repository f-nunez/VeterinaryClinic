using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClients;

public class GetClientsResponse : BaseResponse
{
    public List<ClientDto> Clients { get; set; } = new();
    public int Count { get; set; }

    public GetClientsResponse(Guid correlationId) : base(correlationId)
    {
    }
}