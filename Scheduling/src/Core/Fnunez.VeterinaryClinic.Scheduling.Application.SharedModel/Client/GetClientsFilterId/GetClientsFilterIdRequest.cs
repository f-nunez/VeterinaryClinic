using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterId;

public class GetClientsFilterIdRequest : BaseRequest
{
    public string IdFilterValue { get; set; } = null!;
}