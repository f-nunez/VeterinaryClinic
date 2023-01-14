using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.CreateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClients;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.UpdateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public interface IClientService
{
    public Task<ClientDto> CreateAsync(CreateClientRequest client);
    public Task<DataGridResponse<ClientDto>> DataGridAsync(GetClientsRequest request);
    public Task<List<string>> DataGridFilterEmailAddressAsync(string filterValue);
    public Task<List<string>> DataGridFilterFullNameAsync(string filterValue);
    public Task<List<string>> DataGridFilterIdAsync(string filterValue);
    public Task<List<string>> DataGridFilterPreferredNameAsync(string filterValue);
    public Task<List<string>> DataGridFilterSalutationsync(string filterValue);
    public Task DeleteAsync(int clientId);
    public Task<ClientDto> EditAsync(UpdateClientRequest client);
    public Task<ClientDto> GetByIdAsync(int clientId);
}