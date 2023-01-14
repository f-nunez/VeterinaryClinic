using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.CreateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.DeleteAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypeById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypes;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterCode;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterDuration;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.UpdateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

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

    public async Task<AppointmentTypeDto> CreateAsync(
        CreateAppointmentTypeRequest createAppointmentTypeRequest)
    {
        _logger.LogInformation($"Create: {createAppointmentTypeRequest}");

        var response = await _httpService
            .HttpPostAsync<CreateAppointmentTypeResponse>(
                "AppointmentType/Create",
                createAppointmentTypeRequest
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.AppointmentType;
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

    public async Task DeleteAsync(int appointmentTypeId)
    {
        _logger.LogInformation($"Delete: {appointmentTypeId}");

        await _httpService.HttpDeleteAsync<DeleteAppointmentTypeResponse>(
            "AppointmentType/Delete",
            appointmentTypeId
        );
    }

    public async Task<AppointmentTypeDto> EditAsync(
        UpdateAppointmentTypeRequest updateAppointmentTypeRequest)
    {
        _logger.LogInformation($"Edit: {updateAppointmentTypeRequest}");

        var response = await _httpService
            .HttpPutAsync<UpdateAppointmentTypeResponse>(
                "AppointmentType/Update",
                updateAppointmentTypeRequest
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.AppointmentType;
    }

    public async Task<AppointmentTypeDto> GetByIdAsync(int appointmentTypeId)
    {
        _logger.LogInformation($"GetById: {appointmentTypeId}");

        var response = await _httpService
            .HttpGetAsync<GetAppointmentTypeByIdResponse>(
                $"AppointmentType/GetById/{appointmentTypeId}");

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.AppointmentType;
    }
}