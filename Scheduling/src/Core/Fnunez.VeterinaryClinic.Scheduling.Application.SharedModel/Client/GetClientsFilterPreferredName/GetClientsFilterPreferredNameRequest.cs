using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterPreferredName;

public class GetClientsFilterPreferredNameRequest : BaseRequest
{
    public string PreferredNameFilterValue { get; set; } = null!;
}