using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.CreatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.DeletePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatients;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.UpdatePatient;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public class PatientService
{
    private readonly HttpService _httpService;
    private readonly ILogger<PatientService> _logger;

    public PatientService(
        HttpService httpService,
        ILogger<PatientService> logger)
    {
        _httpService = httpService;
        _logger = logger;
    }

    public async Task<PatientDto> CreateAsync(
        CreatePatientRequest createPatientRequest)
    {
        _logger.LogInformation($"Create: {createPatientRequest}");

        var response = await _httpService.HttpPostAsync<CreatePatientResponse>(
            "Patient/Create",
            createPatientRequest
        );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Patient;
    }

    public async Task<PatientDto> EditAsync(
        UpdatePatientRequest updatePatientRequest)
    {
        _logger.LogInformation($"Edit: {updatePatientRequest}");

        var response = await _httpService.HttpPutAsync<UpdatePatientResponse>(
            "Patient/Update",
            updatePatientRequest
        );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Patient;
    }

    public async Task DeleteAsync(int clientId, int patientId)
    {
        _logger.LogInformation($"Delete: {clientId}, {patientId}");

        var uri = $"Patient/Delete/{patientId}/Client/{clientId}";

        await _httpService.HttpDeleteAsync<DeletePatientResponse>(uri);
    }

    public async Task<PatientDto> GetByIdAsync(int patientId)
    {
        _logger.LogInformation($"GetById: {patientId}");

        var response = await _httpService.HttpGetAsync<GetPatientByIdResponse>(
            $"Patient/GetById/{patientId}");

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Patient;
    }

    public async Task<List<PatientDto>> ListAsync(int clientId)
    {
        _logger.LogInformation($"List: {clientId}");

        var response = await _httpService.HttpGetAsync<GetPatientsResponse>(
            $"Patient/List?ClientId={clientId}");

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Patients;
    }

    public async Task<List<PatientDto>> ListPagedAsync(int pageSize)
    {
        _logger.LogInformation($"ListPaged: {pageSize}");

        var response = await _httpService.HttpGetAsync<GetPatientsResponse>(
            $"Patient/List");

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Patients;
    }
}