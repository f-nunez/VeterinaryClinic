using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.CreateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.DeleteDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctors;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.UpdateDoctor;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public class DoctorService
{
    private readonly HttpService _httpService;
    private readonly ILogger<DoctorService> _logger;

    public DoctorService(
        HttpService httpService,
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

    public async Task<DoctorDto> EditAsync(
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

    public async Task DeleteAsync(int doctorId)
    {
        _logger.LogInformation($"Delete: {doctorId}");

        await _httpService
            .HttpDeleteAsync<DeleteDoctorResponse>("Doctor/Delete", doctorId);
    }

    public async Task<DoctorDto> GetByIdAsync(int doctorId)
    {
        _logger.LogInformation($"GetById: {doctorId}");

        var response = await _httpService
            .HttpGetAsync<GetDoctorByIdResponse>($"Doctor/GetById/{doctorId}");

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Doctor;
    }

    public async Task<List<DoctorDto>> ListAsync()
    {
        _logger.LogInformation("List");

        var response = await _httpService
            .HttpGetAsync<GetDoctorsResponse>($"Doctor/List");

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Doctors;
    }

    public async Task<List<DoctorDto>> ListPagedAsync(int pageSize)
    {
        _logger.LogInformation($"ListPaged: {pageSize}");

        var response = await _httpService
            .HttpGetAsync<GetDoctorsResponse>($"Doctor/List");

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Doctors;
    }
}