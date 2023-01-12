using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterSalutation;

public class GetClientsFilterSalutationResponse : BaseResponse
{
    public List<string> ClientSalutations { get; set; } = new();

    public GetClientsFilterSalutationResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}