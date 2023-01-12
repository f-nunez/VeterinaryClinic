using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterFullName;

public class GetClientsFilterFullNameResponse : BaseResponse
{
    public List<string> ClientFullNames { get; set; } = new();

    public GetClientsFilterFullNameResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}