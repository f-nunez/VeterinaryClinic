using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.CreateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.DeleteDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctors;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorsFilterFullName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorsFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.UpdateDoctor;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public class DoctorService : IDoctorService
{
    private readonly IHttpService _httpService;
    private readonly ILogger<DoctorService> _logger;

    public DoctorService(
        IHttpService httpService,
        ILogger<DoctorService> logger)
    {
        _httpService = httpService;
        _logger = logger;
    }

    public async Task<DoctorDto> CreateAsync(
        CreateDoctorRequest createDoctorRequest)
    {
        _logger.LogInformation($"Create: {createDoctorRequest}");

        var response = await _httpService.HttpPostAsync<CreateDoctorResponse>(
            "Doctor/Create",
            createDoctorRequest
        );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Doctor;
    }

    public async Task<DataGridResponse<DoctorDto>> DataGridAsync(
        GetDoctorsRequest request)
    {
        _logger.LogInformation($"DataGrid: {request.DataGridRequest.ToString()}");

        var response = await _httpService
            .HttpPostAsync<GetDoctorsResponse>(
                $"Doctor/DataGrid",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        if (response.DataGridResponse is null)
            throw new ArgumentNullException(nameof(response.DataGridResponse));

        return response.DataGridResponse;
    }

    public async Task<List<string>> DataGridFilterFullNameAsync(
        string filterValue)
    {
        _logger.LogInformation($"DataGridFilterFullName: {filterValue}");

        var request = new GetDoctorsFilterFullNameRequest
        {
            FullNameFilterValue = filterValue
        };

        var response = await _httpService
            .HttpPostAsync<GetDoctorsFilterFullNameResponse>(
                $"Doctor/DataGridFilterFullName",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.DoctorFullNames;
    }

    public async Task<List<string>> DataGridFilterIdAsync(
        string filterValue)
    {
        _logger.LogInformation($"DataGridFilterId: {filterValue}");

        var request = new GetDoctorsFilterIdRequest
        {
            IdFilterValue = filterValue
        };

        var response = await _httpService
            .HttpPostAsync<GetDoctorsFilterIdResponse>(
                $"Doctor/DataGridFilterId",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.DoctorIds;
    }

    public async Task DeleteAsync(DeleteDoctorRequest request)
    {
        _logger.LogInformation($"Delete: {request.Id}");

        await _httpService
            .HttpDeleteAsync<DeleteDoctorResponse>("Doctor/Delete", request.Id);
    }

    public async Task<DoctorDto> GetByIdAsync(GetDoctorByIdRequest request)
    {
        _logger.LogInformation($"GetById: {request.Id}");

        var response = await _httpService
            .HttpGetAsync<GetDoctorByIdResponse>($"Doctor/GetById/{request.Id}");

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Doctor;
    }

    public async Task<DoctorDto> UpdateAsync(
        UpdateDoctorRequest updateDoctorRequest)
    {
        _logger.LogInformation($"Edit: {updateDoctorRequest}");

        var response = await _httpService.HttpPutAsync<UpdateDoctorResponse>(
            "Doctor/Update",
            updateDoctorRequest
        );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Doctor;
    }
}