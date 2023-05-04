using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.DeleteClient;

public class DeleteClientResponse : BaseResponse
{
    public DeleteClientResponse(Guid correlationId) : base(correlationId)
    {
    }
}