using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientById;

public class GetClientByIdResponse : BaseResponse
{
    public ClientDto Client { get; set; } = new ClientDto();

    public GetClientByIdResponse(Guid correlationId) : base(correlationId)
    {
    }
}