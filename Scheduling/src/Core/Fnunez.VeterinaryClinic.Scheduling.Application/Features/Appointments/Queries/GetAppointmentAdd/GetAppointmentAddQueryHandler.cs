using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services;
using Fnunez.VeterinaryClinic.Scheduling.Application.Settings;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentAdd;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentAdd;

public class GetAppointmentAddQueryHandler
    : IRequestHandler<GetAppointmentAddQuery, GetAppointmentAddResponse>
{
    private readonly IClientStorageSetting _clientStorageSetting;
    private readonly IFileSystemReaderService _fileSystemReaderService;
    private readonly ILogger<GetAppointmentAddQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentAddQueryHandler(
        IClientStorageSetting clientStorageSetting,
        IFileSystemReaderService fileSystemReaderService,
        ILogger<GetAppointmentAddQueryHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _clientStorageSetting = clientStorageSetting;
        _fileSystemReaderService = fileSystemReaderService;
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppointmentAddResponse> Handle(
        GetAppointmentAddQuery query,
        CancellationToken cancellationToken)
    {
        GetAppointmentAddRequest request = query.GetAppointmentAddRequest;
        var response = new GetAppointmentAddResponse(request.CorrelationId);
        var specification = new ClientByIdSpecification(request.ClientId);

        var client = await _unitOfWork
            .ReadRepository<Client>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (client is null)
            throw new NotFoundException(nameof(client), request.ClientId);

        var patient = client.Patients
            .FirstOrDefault(p => p.Id == request.PatientId);

        if (patient is null)
            throw new NotFoundException(nameof(patient), request.PatientId);

        var clinic = await _unitOfWork
            .ReadRepository<Clinic>()
            .GetByIdAsync(request.ClinicId, cancellationToken);

        if (clinic is null)
            throw new NotFoundException(nameof(clinic), request.ClinicId);

        response.Appointment = await MapAppointmentAddDtoAsync(
            client, clinic, patient);

        return response;
    }

    private async Task<AppointmentAddDto> MapAppointmentAddDtoAsync(
        Client client,
        Clinic clinic,
        Patient patient)
    {
        return new AppointmentAddDto
        {
            ClientFullName = client.FullName,
            ClientId = client.Id,
            ClinicId = clinic.Id,
            ClinicName = clinic.Name,
            PatientId = patient.Id,
            PatientName = patient.Name,
            PatientPhotoData = await GetPatientPhotoDataAsync(patient)
        };
    }

    private async Task<byte[]?> GetPatientPhotoDataAsync(Patient patient)
    {
        string relativePhotoPath = Path.Combine(
            patient.ClientId.ToString(), patient.Photo.StoredName);

        string photoPath = Path.Combine(
            _clientStorageSetting.BasePath, relativePhotoPath);

        byte[]? photoData = null;

        try
        {
            photoData = await _fileSystemReaderService.ReadAsync(photoPath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }

        return photoData;
    }
}