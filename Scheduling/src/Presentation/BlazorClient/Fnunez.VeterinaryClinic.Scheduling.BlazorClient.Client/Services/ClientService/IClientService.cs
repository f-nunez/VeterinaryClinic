using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClients;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;

public interface IClientService
{
    public Task<DataGridResponse<ClientDto>> DataGridAsync(GetClientsRequest request);
    public Task<List<string>> DataGridFilterEmailAddressAsync(string filterValue);
    public Task<List<string>> DataGridFilterFullNameAsync(string filterValue);
    public Task<List<string>> DataGridFilterIdAsync(string filterValue);
    public Task<List<string>> DataGridFilterPreferredNameAsync(string filterValue);
    public Task<List<string>> DataGridFilterSalutationsync(string filterValue);
}