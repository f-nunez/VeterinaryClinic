using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypes;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterCode;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterDuration;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterId;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterName;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;

public class AppointmentTypeService : IAppointmentTypeService
{
    private readonly IHttpService _httpService;
    private readonly ILogger<AppointmentTypeService> _logger;

    public AppointmentTypeService(
        IHttpService httpService,
        ILogger<AppointmentTypeService> logger)
    {
        _httpService = httpService;
        _logger = logger;
    }

    public async Task<DataGridResponse<AppointmentTypeDto>> DataGridAsync(
        GetAppointmentTypesRequest request)
    {
        _logger.LogInformation($"DataGrid: {request.DataGridRequest.ToString()}");

        var response = await _httpService
            .HttpPostAsync<GetAppointmentTypesResponse>(
                $"AppointmentType/DataGrid",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        if (response.DataGridResponse is null)
            throw new ArgumentNullException(nameof(response.DataGridResponse));

        return response.DataGridResponse;
    }

    public async Task<List<string>> DataGridFilterCodeAsync(
        string filterValue)
    {
        _logger.LogInformation($"DataGridFilterCode: {filterValue}");

        var request = new GetAppointmentTypesFilterCodeRequest
        {
            CodeFilterValue = filterValue
        };

        var response = await _httpService
            .HttpPostAsync<GetAppointmentTypesFilterCodeResponse>(
                $"AppointmentType/DataGridFilterCode",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.AppointemntTypeCodes;
    }

    public async Task<List<string>> DataGridFilterDurationAsync(
        string filterValue)
    {
        _logger.LogInformation($"DataGridFilterDuration: {filterValue}");

        var request = new GetAppointmentTypesFilterDurationRequest
        {
            DurationFilterValue = filterValue
        };

        var response = await _httpService
            .HttpPostAsync<GetAppointmentTypesFilterDurationResponse>(
                $"AppointmentType/DataGridFilterDuration",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.AppointmentTypeDurations;
    }

    public async Task<List<string>> DataGridFilterIdAsync(
        string filterValue)
    {
        _logger.LogInformation($"DataGridFilterId: {filterValue}");

        var request = new GetAppointmentTypesFilterIdRequest
        {
            IdFilterValue = filterValue
        };

        var response = await _httpService
            .HttpPostAsync<GetAppointmentTypesFilterIdResponse>(
                $"AppointmentType/DataGridFilterId",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.AppointmentTypeIds;
    }

    public async Task<List<string>> DataGridFilterNameAsync(
        string filterValue)
    {
        _logger.LogInformation($"DataGridFilterName: {filterValue}");

        var request = new GetAppointmentTypesFilterNameRequest
        {
            NameFilterValue = filterValue
        };

        var response = await _httpService
            .HttpPostAsync<GetAppointmentTypesFilterNameResponse>(
                $"AppointmentType/DataGridFilterName",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.AppointemntTypeNames;
    }
}