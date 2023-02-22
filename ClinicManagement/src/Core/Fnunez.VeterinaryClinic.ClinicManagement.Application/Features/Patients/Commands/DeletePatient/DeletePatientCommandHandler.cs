using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.CreatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.SendIntegrationEvents.PatientDeleted;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Interfaces.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Interfaces.Settings;
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
    private readonly IUnitOfWork _unitOfWork;

    public DeletePatientCommandHandler(
        IClientStorageSetting clientStorageSetting,
        ICurrentUserService currentUserService,
        IFileSystemDeleterService fileSystemDeleterService,
        IMediator mediator,
        IUnitOfWork unitOfWork)
    {
        _clientStorageSetting = clientStorageSetting;
        _currentUserService = currentUserService;
        _fileSystemDeleterService = fileSystemDeleterService;
        _mediator = mediator;
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

        await SendIntegrationEventAsync(
            request.PatientId,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private void DeletePhoto(Patient patient)
    {
        string relativePhotoPath = Path.Combine(
            patient.ClientId.ToString(), patient.Photo.StoredName);

        string photoPath = Path.Combine(
            _clientStorageSetting.BasePath, relativePhotoPath);

        _fileSystemDeleterService.Delete(photoPath);
    }

    private async Task SendIntegrationEventAsync(
        int patientId,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var message = new PatientDeletedIntegrationEventContract
        {
            CausationId = correlationId,
            CorrelationId = correlationId,
            Id = Guid.NewGuid(),
            OccurredOn = DateTimeOffset.UtcNow,
            PatientId = patientId
        };

        await _mediator.Publish(
            new PatientDeletedSendIntegrationEvent(message),
            cancellationToken
        );
    }
}