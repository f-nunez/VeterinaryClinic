using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.CreatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.DeletePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientDetail;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientEdit;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatients;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientsFilterClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientsFilterPreferredDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.UpdatePatient;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public class PatientService : IPatientService
{
    private readonly IClinicManagementApiHttpService _httpService;
    private readonly ILogger<PatientService> _logger;

    public PatientService(
        IClinicManagementApiHttpService httpService,
        ILogger<PatientService> logger)
    {
        _httpService = httpService;
        _logger = logger;
    }

    public async Task CreateAsync(
        CreatePatientRequest createPatientRequest)
    {
        _logger.LogInformation($"Create: {createPatientRequest}");

        var response = await _httpService.HttpPostAsync<CreatePatientResponse>(
            "Patient/Create",
            createPatientRequest
        );

        if (response is null)
            throw new ArgumentNullException(nameof(response));
    }

    public async Task<DataGridResponse<ClientFilterValueDto>> DataGridFilterClientAsync(
        GetPatientsFilterClientRequest request)
    {
        _logger.LogInformation($"DataGridFilterClient: {request.DataGridRequest.ToString()}");

        var response = await _httpService
            .HttpPostAsync<GetPatientsFilterClientResponse>(
                "Patient/DataGridFilterClient",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        if (response.DataGridResponse is null)
            throw new ArgumentNullException(nameof(response.DataGridResponse));

        return response.DataGridResponse;
    }

    public async Task<DataGridResponse<PreferredDoctorFilterValueDto>> DataGridFilterPreferredDoctorAsync(
        GetPatientsFilterPreferredDoctorRequest request)
    {
        var response = await _httpService
            .HttpPostAsync<GetPatientsFilterPreferredDoctorResponse>(
                "Patient/DataGridFilterPreferredDoctor",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        if (response.DataGridResponse is null)
            throw new ArgumentNullException(nameof(response.DataGridResponse));

        return response.DataGridResponse;
    }

    public async Task DeleteAsync(DeletePatientRequest request)
    {
        _logger.LogInformation($"Delete: {request.ClientId}, {request.PatientId}");

        var uri = $"Patient/Delete/{request.PatientId}/Client/{request.ClientId}";

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

    public async Task<GetPatientDetailResponse> GetPatientDetailAsync(
        GetPatientDetailRequest request)
    {
        var response = await _httpService.HttpGetAsync<GetPatientDetailResponse>(
            $"Patient/GetPatientDetail/{request.PatientId}/Client/{request.ClientId}");

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response;
    }

    public async Task<GetPatientEditResponse> GetPatientEditAsync(
        GetPatientEditRequest request)
    {
        var response = await _httpService.HttpGetAsync<GetPatientEditResponse>(
            $"Patient/GetPatientEdit/{request.PatientId}/Client/{request.ClientId}");

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response;
    }

    public async Task<List<PatientsDto>> GetPatientsAsync(
        GetPatientsRequest request)
    {
        var response = await _httpService.HttpGetAsync<GetPatientsResponse>(
            $"Patient/GetPatients/{request.ClientId}");

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Patients;
    }

    public async Task UpdateAsync(
        UpdatePatientRequest updatePatientRequest)
    {
        _logger.LogInformation($"Edit: {updatePatientRequest}");

        var response = await _httpService.HttpPutAsync<UpdatePatientResponse>(
            "Patient/Update",
            updatePatientRequest
        );

        if (response is null)
            throw new ArgumentNullException(nameof(response));
    }
}