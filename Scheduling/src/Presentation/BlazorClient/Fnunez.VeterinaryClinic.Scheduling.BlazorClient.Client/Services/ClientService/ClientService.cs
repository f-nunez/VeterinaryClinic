using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClients;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterEmailAddress;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterFullName;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterId;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterPreferredName;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterSalutation;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;

public class ClientService : IClientService
{
    private readonly IHttpService _httpService;
    private readonly ILogger<ClientService> _logger;

    public ClientService(
        IHttpService httpService,
        ILogger<ClientService> logger)
    {
        _httpService = httpService;
        _logger = logger;
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
}