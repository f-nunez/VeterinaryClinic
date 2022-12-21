using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterSalutation;

public class GetClientsFilterSalutationRequest : BaseRequest
{
    public string SalutationFilterValue { get; set; } = null!;
}