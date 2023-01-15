using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.CreateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.DeleteClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientById;
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
    public Task DeleteAsync(DeleteClientRequest request);
    public Task<ClientDto> GetByIdAsync(GetClientByIdRequest request);
    public Task<ClientDto> UpdateAsync(UpdateClientRequest client);
}