using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterSalutation;

public class GetClientsFilterSalutationResponse : BaseResponse
{
    public List<string> ClientSalutations { get; set; } = new();

    public GetClientsFilterSalutationResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}