using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.SendIntegrationEvents.PatientCreated;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Settings;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.CreatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.ValueObjects;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.CreatePatient;

public class CreatePatientCommandHandler
    : IRequestHandler<CreatePatientCommand, CreatePatientResponse>
{
    private readonly IClientStorageSetting _clientStorageSetting;
    private readonly ICurrentUserService _currentUserService;
    private readonly IFileSystemWriterService _fileSystemWriterService;
    private readonly IMediator _mediator;
    private readonly INotificationRequestService _notificationRequestService;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePatientCommandHandler(
        IClientStorageSetting clientStorageSetting,
        ICurrentUserService currentUserService,
        IFileSystemWriterService fileSystemWriterService,
        IMediator mediator,
        INotificationRequestService notificationRequestService,
        IUnitOfWork unitOfWork)
    {
        _clientStorageSetting = clientStorageSetting;
        _currentUserService = currentUserService;
        _fileSystemWriterService = fileSystemWriterService;
        _mediator = mediator;
        _notificationRequestService = notificationRequestService;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreatePatientResponse> Handle(
        CreatePatientCommand command,
        CancellationToken cancellationToken)
    {
        CreatePatientRequest request = command.CreatePatientRequest;
        var response = new CreatePatientResponse(request.CorrelationId);
        var specification = new ClientByIdSpecification(request.ClientId);

        var client = await _unitOfWork
            .Repository<Client>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (client is null)
            throw new NotFoundException(
                nameof(request.ClientId), request.ClientId);

        var newPatient = await CreatePatientAsync(
            request, client, cancellationToken);

        await SendContractsToServiceBusAsync(
            newPatient, request.CorrelationId, cancellationToken);

        return response;
    }

    private async Task<Patient> CreatePatientAsync(
        CreatePatientRequest request,
        Client client,
        CancellationToken cancellationToken)
    {
        string savedPhotoName = await SavePhotoAsync(request);

        var newPatient = new Patient(
            request.ClientId,
            request.Name,
            (AnimalSex)Enum.ToObject(typeof(AnimalSex), request.Sex),
            new AnimalType(request.Breed, request.Species),
            new Photo(request.PhotoName, savedPhotoName),
            preferredDoctorId: request.PreferredDoctorId
        );

        newPatient.SetCreatedBy(_currentUserService.UserId);

        client.AddPatient(newPatient);

        await SaveClientAsync(client, cancellationToken);

        return newPatient;
    }

    private async Task SaveClientAsync(
        Client client,
        CancellationToken cancellationToken)
    {
        await _unitOfWork
            .Repository<Client>()
            .UpdateAsync(client, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);
    }

    private async Task<string> SavePhotoAsync(CreatePatientRequest request)
    {
        string photoExtension = Path.GetExtension(request.PhotoName).ToLower();

        string photoNameToSave = $"{Guid.NewGuid().ToString()}{photoExtension}";

        string relativePhotoPath = Path.Combine(
            request.ClientId.ToString(), photoNameToSave);

        string photoPath = Path.Combine(
            _clientStorageSetting.BasePath, relativePhotoPath);

        await _fileSystemWriterService.WriteAsync(request.PhotoData, photoPath);

        return photoNameToSave;
    }

    private async Task SendContractsToServiceBusAsync(
        Patient patient,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        await SendIntegrationEventAsync(
            patient,
            correlationId,
            cancellationToken
        );

        await SendNotificationRequestAsync(
            patient,
            correlationId,
            cancellationToken
        );
    }

    private async Task SendIntegrationEventAsync(
        Patient patient,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var message = new PatientCreatedIntegrationEventContract
        {
            CausationId = correlationId,
            CorrelationId = correlationId,
            Id = Guid.NewGuid(),
            OccurredOn = DateTimeOffset.UtcNow,
            PatientBreed = patient.AnimalType.Breed,
            PatientClientId = patient.ClientId,
            PatientId = patient.Id,
            PatientName = patient.Name,
            PatientPhotoName = patient.Photo.Name,
            PatientPhotoStoredName = patient.Photo.StoredName,
            PatientPreferredDoctorId = patient.PreferredDoctorId,
            PatientSex = (int)patient.AnimalSex,
            PatientSpecies = patient.AnimalType.Species
        };

        await _mediator.Publish(
            new PatientCreatedSendIntegrationEvent(message),
            cancellationToken
        );
    }

    private async Task SendNotificationRequestAsync(
        Patient patient,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new PatientCreatedNotificationRequestFactory(
            patient,
            correlationId,
            _currentUserService.UserId
        );

        await _notificationRequestService.CreateAndSendAsync(
            factory, cancellationToken);
    }
}