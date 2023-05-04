using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterPreferredName;

public class GetClientsFilterPreferredNameRequest : BaseRequest
{
    public string PreferredNameFilterValue { get; set; } = null!;
}