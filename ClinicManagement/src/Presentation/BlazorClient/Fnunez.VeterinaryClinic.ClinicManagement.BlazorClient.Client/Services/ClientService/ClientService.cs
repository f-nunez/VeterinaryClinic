using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.CreateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.DeleteClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientDetail;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientEdit;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClients;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterEmailAddress;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterFullName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterPreferredDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterPreferredName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterSalutation;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.UpdateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public class ClientService : IClientService
{
    private readonly IClinicManagementApiHttpService _httpService;
    private readonly ILogger<ClientService> _logger;

    public ClientService(
        IClinicManagementApiHttpService httpService,
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

    public async Task<DataGridResponse<ClientDto>> DataGridAsync(
        GetClientsRequest request)
    {
        _logger.LogInformation($"DataGrid: {request.DataGridRequest.ToString()}");

        var response = await _httpService
            .HttpPostAsync<GetClientsResponse>(
                $"Client/DataGrid",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        if (response.DataGridResponse is null)
            throw new ArgumentNullException(nameof(response.DataGridResponse));

        return response.DataGridResponse;
    }

    public async Task<List<string>> DataGridFilterEmailAddressAsync(
        string filterValue)
    {
        _logger.LogInformation($"DataGridFilterEmailAddress: {filterValue}");

        var request = new GetClientsFilterEmailAddressRequest
        {
            EmailAddressFilterValue = filterValue
        };

        var response = await _httpService
            .HttpPostAsync<GetClientsFilterEmailAddressResponse>(
                $"Client/DataGridFilterEmailAddress",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.ClientEmailAddresses;
    }

    public async Task<List<string>> DataGridFilterFullNameAsync(
        string filterValue)
    {
        _logger.LogInformation($"DataGridFilterFullName: {filterValue}");

        var request = new GetClientsFilterFullNameRequest
        {
            FullNameFilterValue = filterValue
        };

        var response = await _httpService
            .HttpPostAsync<GetClientsFilterFullNameResponse>(
                $"Client/DataGridFilterFullName",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.ClientFullNames;
    }

    public async Task<List<string>> DataGridFilterIdAsync(
        string filterValue)
    {
        _logger.LogInformation($"DataGridFilterId: {filterValue}");

        var request = new GetClientsFilterIdRequest
        {
            IdFilterValue = filterValue
        };

        var response = await _httpService
            .HttpPostAsync<GetClientsFilterIdResponse>(
                $"Client/DataGridFilterId",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.ClientIds;
    }

    public async Task<DataGridResponse<PreferredDoctorFilterValueDto>> DataGridFilterPreferredDoctorAsync(
        GetClientsFilterPreferredDoctorRequest request)
    {
        var response = await _httpService
            .HttpPostAsync<GetClientsFilterPreferredDoctorResponse>(
                $"Client/DataGridFilterPreferredDoctor",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        if (response.DataGridResponse is null)
            throw new ArgumentNullException(nameof(response.DataGridResponse));

        return response.DataGridResponse;
    }

    public async Task<List<string>> DataGridFilterPreferredNameAsync(
        string filterValue)
    {
        _logger.LogInformation($"DataGridFilterPreferredName: {filterValue}");

        var request = new GetClientsFilterPreferredNameRequest
        {
            PreferredNameFilterValue = filterValue
        };

        var response = await _httpService
            .HttpPostAsync<GetClientsFilterPreferredNameResponse>(
                $"Client/DataGridFilterPreferredName",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.ClientPreferredNames;
    }

    public async Task<List<string>> DataGridFilterSalutationsync(
        string filterValue)
    {
        _logger.LogInformation($"DataGridFilterSalutation: {filterValue}");

        var request = new GetClientsFilterSalutationRequest
        {
            SalutationFilterValue = filterValue
        };

        var response = await _httpService
            .HttpPostAsync<GetClientsFilterSalutationResponse>(
                $"Client/DataGridFilterSalutation",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.ClientSalutations;
    }

    public async Task DeleteAsync(DeleteClientRequest request)
    {
        _logger.LogInformation($"Delete: {request.Id}");

        await _httpService
            .HttpDeleteAsync<DeleteClientResponse>("Client/Delete", request.Id);
    }

    public async Task<ClientDto> GetByIdAsync(GetClientByIdRequest request)
    {
        _logger.LogInformation($"GetById: {request.Id}");

        var response = await _httpService
            .HttpGetAsync<GetClientByIdResponse>($"Client/GetById/{request.Id}");

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Client;
    }

    public async Task<GetClientDetailResponse> GetClientDetailAsync(
        GetClientDetailRequest request)
    {
        _logger.LogInformation($"GetClientDetail: {request.CorrelationId}");

        var response = await _httpService
            .HttpPostAsync<GetClientDetailResponse>(
                $"Client/GetClientDetail",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response;
    }

    public async Task<GetClientEditResponse> GetClientEditAsync(
        GetClientEditRequest request)
    {
        _logger.LogInformation($"GetClientEdit: {request.CorrelationId}");

        var response = await _httpService
            .HttpPostAsync<GetClientEditResponse>(
                $"Client/GetClientEdit",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response;
    }

    public async Task<ClientDto> UpdateAsync(UpdateClientRequest client)
    {
        _logger.LogInformation($"Edit: {client}");

        var response = await _httpService
            .HttpPutAsync<UpdateClientResponse>("Client/Update", client);

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Client;
    }
}