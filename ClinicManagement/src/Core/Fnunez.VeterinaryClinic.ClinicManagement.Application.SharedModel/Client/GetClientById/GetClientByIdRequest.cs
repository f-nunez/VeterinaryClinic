using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientById;

public class GetClientByIdRequest : BaseRequest
{
    public int ClientId { get; set; }
}