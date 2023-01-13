using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterId;

public class GetClientsFilterIdResponse : BaseResponse
{
    public List<string> ClientIds { get; set; } = new();

    public GetClientsFilterIdResponse(Guid correlationId) : base(correlationId)
    {
    }
}