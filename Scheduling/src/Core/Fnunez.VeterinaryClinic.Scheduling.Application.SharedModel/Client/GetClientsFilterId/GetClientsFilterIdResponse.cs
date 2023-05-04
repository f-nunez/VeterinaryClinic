using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterId;

public class GetClientsFilterIdResponse : BaseResponse
{
    public List<string> ClientIds { get; set; } = new();

    public GetClientsFilterIdResponse(Guid correlationId) : base(correlationId)
    {
    }
}