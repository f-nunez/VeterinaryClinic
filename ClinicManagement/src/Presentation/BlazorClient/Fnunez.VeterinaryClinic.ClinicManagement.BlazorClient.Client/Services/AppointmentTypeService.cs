using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.CreateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.DeleteAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypeById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypes;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.UpdateAppointmentType;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public class AppointmentTypeService
{
    private readonly HttpService _httpService;
    private readonly ILogger<AppointmentTypeService> _logger;

    public AppointmentTypeService(
        HttpService httpService,
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

    public async Task<List<AppointmentTypeDto>> ListAsync()
    {
        _logger.LogInformation("List");

        var response = await _httpService
            .HttpGetAsync<GetAppointmentTypesResponse>($"AppointmentType/List");

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.AppointmentTypes;
    }

    public async Task<List<AppointmentTypeDto>> ListPagedAsync(int pageSize)
    {
        _logger.LogInformation($"ListPaged: {pageSize}");

        var response = await _httpService
            .HttpGetAsync<GetAppointmentTypesResponse>($"AppointmentType/List");

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.AppointmentTypes;
    }
}