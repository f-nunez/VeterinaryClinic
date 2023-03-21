using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatientDetail;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatients;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatientsFilterClient;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;

public class PatientService : IPatientService
{
    private readonly ISchedulingApiHttpService _httpService;
    private readonly ILogger<PatientService> _logger;

    public PatientService(
        ISchedulingApiHttpService httpService,
        ILogger<PatientService> logger)
    {
        _httpService = httpService;
        _logger = logger;
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

    public async Task<GetPatientDetailResponse> GetPatientDetailAsync(
        GetPatientDetailRequest request)
    {
        var response = await _httpService.HttpGetAsync<GetPatientDetailResponse>(
            $"Patient/GetPatientDetail/{request.PatientId}/Client/{request.ClientId}");

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
}