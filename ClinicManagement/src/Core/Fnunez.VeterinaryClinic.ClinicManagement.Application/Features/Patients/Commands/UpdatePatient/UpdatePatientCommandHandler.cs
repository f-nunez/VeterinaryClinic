using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.SendIntegrationEvents.PatientUpdated;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Settings;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.UpdatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.ValueObjects;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.UpdatePatient;

public class UpdatePatientCommandHandler
    : IRequestHandler<UpdatePatientCommand, UpdatePatientResponse>
{
    private readonly IClientStorageSetting _clientStorageSetting;
    private readonly ICurrentUserService _currentUserService;
    private readonly IFileSystemDeleterService _fileSystemDeleterService;
    private readonly IFileSystemWriterService _fileSystemWriterService;
    private readonly IMediator _mediator;
    private readonly INotificationRequestService _notificationRequestService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePatientCommandHandler(
        IClientStorageSetting clientStorageSetting,
        ICurrentUserService currentUserService,
        IFileSystemDeleterService fileSystemDeleterService,
        IFileSystemWriterService fileSystemWriterService,
        IMediator mediator,
        INotificationRequestService notificationRequestService,
        IUnitOfWork unitOfWork)
    {
        _clientStorageSetting = clientStorageSetting;
        _currentUserService = currentUserService;
        _fileSystemDeleterService = fileSystemDeleterService;
        _fileSystemWriterService = fileSystemWriterService;
        _mediator = mediator;
        _notificationRequestService = notificationRequestService;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdatePatientResponse> Handle(
        UpdatePatientCommand command,
        CancellationToken cancellationToken)
    {
        UpdatePatientRequest request = command.UpdatePatientRequest;
        var response = new UpdatePatientResponse(request.CorrelationId);
        var specification = new ClientByIdSpecification(request.ClientId);

        var client = await _unitOfWork
            .Repository<Client>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (client is null)
            return response;

        var patientToUpdate = await UpdatePatientAsync(request, client, cancellationToken);

        await SendContractsToServiceBusAsync(
            patientToUpdate,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private async Task<Patient> UpdatePatientAsync(
        UpdatePatientRequest request,
        Client client,
        CancellationToken cancellationToken)
    {
        var patientToUpdate = client.Patients
            .FirstOrDefault(p => p.Id == request.PatientId);

        if (patientToUpdate is null)
            throw new NotFoundException(
                nameof(request.PatientId), request.PatientId);

        patientToUpdate.UpdateAnimalSex(
            (AnimalSex)Enum.ToObject(typeof(AnimalSex), request.Sex));

        patientToUpdate.UpdateAnimalType(
            new AnimalType(request.Breed, request.Species));

        patientToUpdate.UpdateName(request.Name);

        await UpdateNewPhotoAsync(request, patientToUpdate);

        patientToUpdate.UpdatePreferredDoctorId(request.PreferredDoctorId);

        patientToUpdate.SetUpdatedBy(_currentUserService.UserId);

        await _unitOfWork
            .Repository<Client>()
            .UpdateAsync(client, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return patientToUpdate;
    }

    private async Task UpdateNewPhotoAsync(
        UpdatePatientRequest request,
        Patient patientToUpdate)
    {
        if (!request.IsNewPhoto)
            return;

        DeleteCurrentPhoto(patientToUpdate);

        string savedNewPhotoName = await SaveNewPhotoAsync(request);

        patientToUpdate.UpdatePhoto(
            new Photo(request.PhotoName, savedNewPhotoName));
    }

    private void DeleteCurrentPhoto(Patient patient)
    {
        string relativePhotoPath = Path.Combine(
            patient.ClientId.ToString(), patient.Photo.StoredName);

        string photoPath = Path.Combine(
            _clientStorageSetting.BasePath, relativePhotoPath);

        _fileSystemDeleterService.Delete(photoPath);
    }

    private async Task<string> SaveNewPhotoAsync(UpdatePatientRequest request)
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
        var message = new PatientUpdatedIntegrationEventContract
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
            new PatientUpdatedSendIntegrationEvent(message),
            cancellationToken
        );
    }

    private async Task SendNotificationRequestAsync(
        Patient patient,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new PatientUpdatedNotificationRequestFactory(
            patient,
            correlationId,
            _currentUserService.UserId
        );

        await _notificationRequestService.CreateAndSendAsync(
            factory, cancellationToken);
    }
}