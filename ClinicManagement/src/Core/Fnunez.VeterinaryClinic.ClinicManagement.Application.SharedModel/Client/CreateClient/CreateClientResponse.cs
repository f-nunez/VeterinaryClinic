using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.CreateClient;

public class CreateClientResponse : BaseResponse
{
    public ClientDto Client { get; set; } = new ClientDto();

    public CreateClientResponse(Guid correlationId) : base(correlationId)
    {
    }
}