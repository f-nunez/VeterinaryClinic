using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClients;

public class GetClientsResponse : BaseResponse
{
    public DataGridResponse<ClientDto>? DataGridResponse { get; set; }

    public GetClientsResponse(Guid correlationId) : base(correlationId)
    {
    }
}