using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterFullName;

public class GetClientsFilterFullNameRequest : BaseRequest
{
    public string FullNameFilterValue { get; set; } = null!;
}