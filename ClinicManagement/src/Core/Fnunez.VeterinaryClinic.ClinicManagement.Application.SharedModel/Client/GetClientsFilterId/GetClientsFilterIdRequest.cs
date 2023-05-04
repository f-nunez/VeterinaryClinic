using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterId;

public class GetClientsFilterIdRequest : BaseRequest
{
    public string IdFilterValue { get; set; } = null!;
}