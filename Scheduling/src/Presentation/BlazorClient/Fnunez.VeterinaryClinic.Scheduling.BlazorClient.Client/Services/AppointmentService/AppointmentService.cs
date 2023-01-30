using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.DeleteAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentAdd;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentDetail;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentEdit;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointments;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterAppointmentType;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterClient;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterClinic;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterDoctor;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterPatient;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterRoom;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.UpdateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IHttpService _httpService;
    private readonly ILogger<AppointmentService> _logger;

    public AppointmentService(
        IHttpService httpService,
        ILogger<AppointmentService> logger)
    {
        _httpService = httpService;
        _logger = logger;
    }

    public async Task<CreateAppointmentResponse> CreateAppointmentAsync(
        CreateAppointmentRequest request)
    {
        _logger.LogInformation($"CreateAppointment: {request.CorrelationId}");

        var response = await _httpService
            .HttpPostAsync<CreateAppointmentResponse>(
                "Appointment/Create",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response;
    }

    public async Task<DataGridResponse<AppointmentDto>> DataGridAsync(
        GetAppointmentsRequest request)
    {
        _logger.LogInformation($"DataGrid: {request.DataGridRequest.ToString()}");

        var response = await _httpService
            .HttpPostAsync<GetAppointmentsResponse>(
                $"Appointment/DataGrid",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        if (response.DataGridResponse is null)
            throw new ArgumentNullException(nameof(response.DataGridResponse));

        return response.DataGridResponse;
    }

    public async Task<DataGridResponse<AppointmentTypeFilterValueDto>> DataGridFilterAppointmentTypeAsync(
        GetAppointmentsFilterAppointmentTypeRequest request)
    {
        _logger.LogInformation($"DataGridFilterAppointmentType: {request.DataGridRequest.ToString()}");

        var response = await _httpService
            .HttpPostAsync<GetAppointmentsFilterAppointmentTypeResponse>(
                $"Appointment/DataGridFilterAppointmentType",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        if (response.DataGridResponse is null)
            throw new ArgumentNullException(nameof(response.DataGridResponse));

        return response.DataGridResponse;
    }

    public async Task<DataGridResponse<ClientFilterValueDto>> DataGridFilterClientAsync(
        GetAppointmentsFilterClientRequest request)
    {
        _logger.LogInformation($"DataGridFilterClient: {request.DataGridRequest.ToString()}");

        var response = await _httpService
            .HttpPostAsync<GetAppointmentsFilterClientResponse>(
                $"Appointment/DataGridFilterClient",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        if (response.DataGridResponse is null)
            throw new ArgumentNullException(nameof(response.DataGridResponse));

        return response.DataGridResponse;
    }

    public async Task<DataGridResponse<ClinicFilterValueDto>> DataGridFilterClinicAsync(
        GetAppointmentsFilterClinicRequest request)
    {
        _logger.LogInformation($"DataGridFilterClinic: {request.DataGridRequest.ToString()}");

        var response = await _httpService
            .HttpPostAsync<GetAppointmentsFilterClinicResponse>(
                $"Appointment/DataGridFilterClinic",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        if (response.DataGridResponse is null)
            throw new ArgumentNullException(nameof(response.DataGridResponse));

        return response.DataGridResponse;
    }

    public async Task<DataGridResponse<DoctorFilterValueDto>> DataGridFilterDoctorAsync(
        GetAppointmentsFilterDoctorRequest request)
    {
        _logger.LogInformation($"DataGridFilterDoctor: {request.DataGridRequest.ToString()}");

        var response = await _httpService
            .HttpPostAsync<GetAppointmentsFilterDoctorResponse>(
                $"Appointment/DataGridFilterDoctor",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        if (response.DataGridResponse is null)
            throw new ArgumentNullException(nameof(response.DataGridResponse));

        return response.DataGridResponse;
    }

    public async Task<List<PatientFilterValueDto>> DataGridFilterPatientAsync(
        GetAppointmentsFilterPatientRequest request)
    {
        _logger.LogInformation("DataGridFilterPatient");

        var response = await _httpService
            .HttpPostAsync<GetAppointmentsFilterPatientResponse>(
                $"Appointment/DataGridFilterPatient",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        if (response.PatientFilterValues is null)
            throw new ArgumentNullException(nameof(response.PatientFilterValues));

        return response.PatientFilterValues;
    }

    public async Task<DataGridResponse<RoomFilterValueDto>> DataGridFilterRoomAsync(
        GetAppointmentsFilterRoomRequest request)
    {
        _logger.LogInformation($"DataGridFilterRoom: {request.DataGridRequest.ToString()}");

        var response = await _httpService
            .HttpPostAsync<GetAppointmentsFilterRoomResponse>(
                $"Appointment/DataGridFilterRoom",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        if (response.DataGridResponse is null)
            throw new ArgumentNullException(nameof(response.DataGridResponse));

        return response.DataGridResponse;
    }

    public async Task<DeleteAppointmentResponse> DeleteAppointmentAsync(DeleteAppointmentRequest request)
    {
        _logger.LogInformation($"DeleteAppointment: {request.AppointmentId}");

        var response = await _httpService
            .HttpDeleteAsync<DeleteAppointmentResponse>(
                $"Appointment/Delete",
                request.AppointmentId
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response;
    }

    public async Task<GetAppointmentAddResponse> GetAppointmentAddAsync(
        GetAppointmentAddRequest request)
    {
        _logger.LogInformation($"GetAppointmentAdd: {request.CorrelationId}");

        var response = await _httpService
            .HttpPostAsync<GetAppointmentAddResponse>(
                "Appointment/GetAppointmentAdd",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response;
    }

    public async Task<GetAppointmentDetailResponse> GetAppointmentDetailAsync(
        GetAppointmentDetailRequest request)
    {
        _logger.LogInformation($"GetAppointmentDetail: {request.CorrelationId}");

        var response = await _httpService
            .HttpPostAsync<GetAppointmentDetailResponse>(
                $"Appointment/GetAppointmentDetail",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response;
    }

    public async Task<GetAppointmentEditResponse> GetAppointmentEditAsync(
        GetAppointmentEditRequest request)
    {
        _logger.LogInformation($"GetAppointmentEdit: {request.CorrelationId}");

        var response = await _httpService
            .HttpPostAsync<GetAppointmentEditResponse>(
                $"Appointment/GetAppointmentEdit",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response;
    }

    public async Task<UpdateAppointmentResponse> UpdateAppointmentAsync(
        UpdateAppointmentRequest request)
    {
        _logger.LogInformation($"UpdateAppointment: {request.CorrelationId}");

        var response = await _httpService
            .HttpPutAsync<UpdateAppointmentResponse>(
                "Appointment/Update",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response;
    }
}