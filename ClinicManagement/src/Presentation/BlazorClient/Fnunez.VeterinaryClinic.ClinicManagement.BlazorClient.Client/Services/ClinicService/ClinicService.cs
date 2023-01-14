using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinics;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterAddress;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterEmailAddress;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public class ClinicService : IClinicService
{
    private readonly IHttpService _httpService;
    private readonly ILogger<ClinicService> _logger;

    public ClinicService(
        IHttpService httpService,
        ILogger<ClinicService> logger)
    {
        _httpService = httpService;
        _logger = logger;
    }

    public async Task<DataGridResponse<ClinicDto>> DataGridAsync(
        GetClinicsRequest request)
    {
        _logger.LogInformation($"DataGrid: {request.DataGridRequest.ToString()}");

        var response = await _httpService
            .HttpPostAsync<GetClinicsResponse>(
                $"Clinic/DataGrid",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        if (response.DataGridResponse is null)
            throw new ArgumentNullException(nameof(response.DataGridResponse));

        return response.DataGridResponse;
    }

    public async Task<List<string>> DataGridFilterAddressAsync(
        string filterValue)
    {
        _logger.LogInformation($"DataGridFilterAddress: {filterValue}");

        var request = new GetClinicsFilterAddressRequest
        {
            AddressFilterValue = filterValue
        };

        var response = await _httpService
            .HttpPostAsync<GetClinicsFilterAddressResponse>(
                $"Clinic/DataGridFilterAddress",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.ClinicAddresses;
    }

    public async Task<List<string>> DataGridFilterEmailAddressAsync(
        string filterValue)
    {
        _logger.LogInformation($"DataGridFilterEmailAddress: {filterValue}");

        var request = new GetClinicsFilterEmailAddressRequest
        {
            EmailAddressFilterValue = filterValue
        };

        var response = await _httpService
            .HttpPostAsync<GetClinicsFilterEmailAddressResponse>(
                $"Clinic/DataGridFilterEmailAddress",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.ClinicEmailAddresses;
    }

    public async Task<List<string>> DataGridFilterIdAsync(
        string filterValue)
    {
        _logger.LogInformation($"DataGridFilterId: {filterValue}");

        var request = new GetClinicsFilterIdRequest
        {
            IdFilterValue = filterValue
        };

        var response = await _httpService
            .HttpPostAsync<GetClinicsFilterIdResponse>(
                $"Clinic/DataGridFilterId",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.ClinicIds;
    }

    public async Task<List<string>> DataGridFilterNameAsync(
        string filterValue)
    {
        _logger.LogInformation($"DataGridFilterName: {filterValue}");

        var request = new GetClinicsFilterNameRequest
        {
            NameFilterValue = filterValue
        };

        var response = await _httpService
            .HttpPostAsync<GetClinicsFilterNameResponse>(
                $"Clinic/DataGridFilterName",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.ClinicNames;
    }
}