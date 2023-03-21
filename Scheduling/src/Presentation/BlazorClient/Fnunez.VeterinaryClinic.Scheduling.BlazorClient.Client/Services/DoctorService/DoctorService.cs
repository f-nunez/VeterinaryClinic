using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctorById;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctors;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctorsFilterFullName;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctorsFilterId;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;

public class DoctorService : IDoctorService
{
    private readonly ISchedulingApiHttpService _httpService;
    private readonly ILogger<DoctorService> _logger;

    public DoctorService(
        ISchedulingApiHttpService httpService,
        ILogger<DoctorService> logger)
    {
        _httpService = httpService;
        _logger = logger;
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

    public async Task<DoctorDto> GetByIdAsync(GetDoctorByIdRequest request)
    {
        _logger.LogInformation($"GetById: {request.Id}");

        var response = await _httpService
            .HttpGetAsync<GetDoctorByIdResponse>($"Doctor/GetById/{request.Id}");

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Doctor;
    }
}