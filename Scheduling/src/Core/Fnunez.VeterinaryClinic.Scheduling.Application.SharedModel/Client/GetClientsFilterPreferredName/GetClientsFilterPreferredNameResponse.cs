using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterPreferredName;

public class GetClientsFilterPreferredNameResponse : BaseResponse
{
    public List<string> ClientPreferredNames { get; set; } = new();

    public GetClientsFilterPreferredNameResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}