using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.UpdateClient;

public class UpdateClientResponse : BaseResponse
{
    public ClientDto Client { get; set; } = new ClientDto();

    public UpdateClientResponse(Guid correlationId) : base(correlationId)
    {
    }
}