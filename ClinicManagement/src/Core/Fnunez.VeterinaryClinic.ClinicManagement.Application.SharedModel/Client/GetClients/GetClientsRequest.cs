using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClients;

public class GetClientsRequest : BaseRequest
{
    public DataGridRequest DataGridRequest { get; set; } = new();
    public string EmailAddressFilterValue { get; set; } = string.Empty;
    public string FullNameFilterValue { get; set; } = string.Empty;
    public string IdFilterValue { get; set; } = string.Empty;
    public string PreferredNameFilterValue { get; set; } = string.Empty;
    public string SalutationFilterValue { get; set; } = string.Empty;
}