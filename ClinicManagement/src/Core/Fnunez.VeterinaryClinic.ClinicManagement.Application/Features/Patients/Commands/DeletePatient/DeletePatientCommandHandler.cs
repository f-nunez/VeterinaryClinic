using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.CreatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.SendIntegrationEvents.PatientDeleted;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Settings;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.DeletePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.DeletePatient;

public class DeletePatientCommandHandler
    : IRequestHandler<DeletePatientCommand, DeletePatientResponse>
{
    private readonly IClientStorageSetting _clientStorageSetting;
    private readonly ICurrentUserService _currentUserService;
    private readonly IFileSystemDeleterService _fileSystemDeleterService;
    private readonly IMediator _mediator;
    private readonly INotificationRequestService _notificationRequestService;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePatientCommandHandler(
        IClientStorageSetting clientStorageSetting,
        ICurrentUserService currentUserService,
        IFileSystemDeleterService fileSystemDeleterService,
        IMediator mediator,
        INotificationRequestService notificationRequestService,
        IUnitOfWork unitOfWork)
    {
        _clientStorageSetting = clientStorageSetting;
        _currentUserService = currentUserService;
        _fileSystemDeleterService = fileSystemDeleterService;
        _mediator = mediator;
        _notificationRequestService = notificationRequestService;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeletePatientResponse> Handle(
        DeletePatientCommand command,
        CancellationToken cancellationToken)
    {
        DeletePatientRequest request = command.DeletePatientRequest;
        var response = new DeletePatientResponse(request.CorrelationId);

        var specification = new ClientByIdSpecification(
            request.ClientId);

        var client = await _unitOfWork
            .Repository<Client>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (client is null)
            throw new NotFoundException(
                nameof(client), request.ClientId);

        var patientToDelete = await DeletePatientAsync(
            request, client, cancellationToken);

        await SendContractsToServiceBusAsync(
            patientToDelete, request.CorrelationId, cancellationToken);

        return response;
    }

    private async Task<Patient> DeletePatientAsync(
        DeletePatientRequest request,
        Client client,
        CancellationToken cancellationToken)
    {
        var patientToDelete = client.Patients
            .FirstOrDefault(p => p.Id == request.PatientId);

        if (patientToDelete is null)
            throw new NotFoundException(
                nameof(patientToDelete), request.PatientId);

        DeletePhoto(patientToDelete);

        patientToDelete.SetUpdatedBy(_currentUserService.UserId);

        client.RemovePatient(patientToDelete);

        await _unitOfWork
            .Repository<Client>()
            .UpdateAsync(client);

        await _unitOfWork.CommitAsync(cancellationToken);

        return patientToDelete;
    }

    private void DeletePhoto(Patient patient)
    {
        string relativePhotoPath = Path.Combine(
            patient.ClientId.ToString(), patient.Photo.StoredName);

        string photoPath = Path.Combine(
            _clientStorageSetting.BasePath, relativePhotoPath);

        _fileSystemDeleterService.Delete(photoPath);
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
        var message = new PatientDeletedIntegrationEventContract
        {
            CausationId = correlationId,
            CorrelationId = correlationId,
            Id = Guid.NewGuid(),
            OccurredOn = DateTimeOffset.UtcNow,
            PatientId = patient.Id
        };

        await _mediator.Publish(
            new PatientDeletedSendIntegrationEvent(message),
            cancellationToken
        );
    }

    private async Task SendNotificationRequestAsync(
        Patient patient,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new PatientDeletedNotificationRequestFactory(
            patient,
            correlationId,
            _currentUserService.UserId
        );

        await _notificationRequestService.CreateAndSendAsync(
            factory, cancellationToken);
    }
}