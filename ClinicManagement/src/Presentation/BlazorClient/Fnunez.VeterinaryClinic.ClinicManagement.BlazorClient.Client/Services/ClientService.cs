using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.CreateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.DeleteClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClients;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.UpdateClient;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public class ClientService
{
    private readonly HttpService _httpService;
    private readonly ILogger<ClientService> _logger;

    public ClientService(
        HttpService httpService,
        ILogger<ClientService> logger)
    {
        _httpService = httpService;
        _logger = logger;
    }

    public async Task<ClientDto> CreateAsync(CreateClientRequest client)
    {
        _logger.LogInformation($"Create: {client}");

        var response = await _httpService
            .HttpPostAsync<CreateClientResponse>("Client/Create", client);

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Client;
    }

    public async Task<ClientDto> EditAsync(UpdateClientRequest client)
    {
        _logger.LogInformation($"Edit: {client}");

        var response = await _httpService
            .HttpPutAsync<UpdateClientResponse>("Client/Update", client);

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Client;
    }

    public async Task DeleteAsync(int clientId)
    {
        _logger.LogInformation($"Delete: {clientId}");

        await _httpService
            .HttpDeleteAsync<DeleteClientResponse>("Client/Delete", clientId);
    }

    public async Task<ClientDto> GetByIdAsync(int clientId)
    {
        _logger.LogInformation($"GetById: {clientId}");

        var response = await _httpService
            .HttpGetAsync<GetClientByIdResponse>($"Client/GetById/{clientId}");

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Client;
    }

    public async Task<List<ClientDto>> ListAsync()
    {
        _logger.LogInformation("List");

        var response = await _httpService
            .HttpGetAsync<GetClientsResponse>($"Client/List");

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Clients;
    }

    public async Task<List<ClientDto>> ListPagedAsync(int pageSize)
    {
        _logger.LogInformation($"ListPaged: {pageSize}");

        var response = await _httpService
            .HttpGetAsync<GetClientsResponse>($"Client/List");

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Clients;
    }
}