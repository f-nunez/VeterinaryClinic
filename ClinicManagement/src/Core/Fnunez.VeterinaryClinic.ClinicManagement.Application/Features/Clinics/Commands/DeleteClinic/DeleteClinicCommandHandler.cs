using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.DeleteClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Commands.DeleteClinic;

public class DeleteClinicCommandHandler
    : IRequestHandler<DeleteClinicCommand, DeleteClinicResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IIntegrationEventSenderService _integrationEventSenderService;
    private readonly IMediator _mediator;
    private readonly INotificationRequestService _notificationRequestService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClinicCommandHandler(
        ICurrentUserService currentUserService,
        IIntegrationEventSenderService integrationEventSenderService,
        IMediator mediator,
        INotificationRequestService notificationRequestService,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _integrationEventSenderService = integrationEventSenderService;
        _mediator = mediator;
        _notificationRequestService = notificationRequestService;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteClinicResponse> Handle(
        DeleteClinicCommand command,
        CancellationToken cancellationToken)
    {
        DeleteClinicRequest request = command.DeleteClinicRequest;
        var response = new DeleteClinicResponse(request.CorrelationId);

        var clinicToDelete = await _unitOfWork
            .Repository<Clinic>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (clinicToDelete is null)
            throw new NotFoundException(nameof(clinicToDelete), request.Id);

        clinicToDelete.SetUpdatedBy(_currentUserService.UserId);

        await _unitOfWork
            .Repository<Clinic>()
            .DeleteAsync(clinicToDelete, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        await SendContractsToServiceBusAsync(
            clinicToDelete,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private async Task SendContractsToServiceBusAsync(
        Clinic clinic,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        await SendIntegrationEventAsync(
            clinic,
            correlationId,
            cancellationToken
        );

        await SendNotificationRequestAsync(
            clinic,
            correlationId,
            cancellationToken
        );
    }

    private async Task SendIntegrationEventAsync(
        Clinic clinic,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new ClinicDeletedIntegrationEventFactory(clinic);

        await _integrationEventSenderService.SendAsync(
            factory, correlationId, cancellationToken);
    }

    private async Task SendNotificationRequestAsync(
        Clinic clinic,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new ClinicDeletedNotificationRequestFactory
        (
            clinic,
            correlationId,
            _currentUserService.UserId
        );

        await _notificationRequestService.SendAsync(
            factory, cancellationToken);
    }
}