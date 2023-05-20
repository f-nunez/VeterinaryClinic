using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.SendIntegrationEvents.AppointmentTypeDeleted;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.DeleteAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.DeleteAppointmentType;

public class DeleteAppointmentTypeCommandHandler
    : IRequestHandler<DeleteAppointmentTypeCommand, DeleteAppointmentTypeResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMediator _mediator;
    private readonly INotificationRequestService _notificationRequestService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAppointmentTypeCommandHandler(
        ICurrentUserService currentUserService,
        IMediator mediator,
        INotificationRequestService notificationRequestService,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _mediator = mediator;
        _notificationRequestService = notificationRequestService;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteAppointmentTypeResponse> Handle(
        DeleteAppointmentTypeCommand command,
        CancellationToken cancellationToken)
    {
        DeleteAppointmentTypeRequest request = command
            .DeleteAppointmentTypeRequest;

        var response = new DeleteAppointmentTypeResponse(
            request.CorrelationId);

        var appointmentTypeToDelete = await _unitOfWork
            .Repository<AppointmentType>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (appointmentTypeToDelete is null)
            throw new NotFoundException(
                nameof(appointmentTypeToDelete), request.Id);

        appointmentTypeToDelete.SetCreatedBy(_currentUserService.UserId);

        await DeleteAppointmentTypeAsync(
            appointmentTypeToDelete, cancellationToken);

        await SendContractsToServiceBusAsync(
            appointmentTypeToDelete,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private async Task DeleteAppointmentTypeAsync(
        AppointmentType appointmentType,
        CancellationToken cancellationToken)
    {
        await _unitOfWork
            .Repository<AppointmentType>()
            .DeleteAsync(appointmentType, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);
    }

    private async Task SendContractsToServiceBusAsync(
        AppointmentType appointmentType,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        await SendIntegrationEventAsync(
            appointmentType,
            correlationId,
            cancellationToken
        );

        await SendNotificationRequestAsync(
            appointmentType,
            correlationId,
            cancellationToken
        );
    }

    private async Task SendIntegrationEventAsync(
        AppointmentType appointmentType,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var message = new AppointmentTypeDeletedIntegrationEventContract
        {
            CausationId = correlationId,
            CorrelationId = correlationId,
            Id = Guid.NewGuid(),
            OccurredOn = DateTimeOffset.UtcNow,
            AppointmentTypeId = appointmentType.Id
        };

        await _mediator.Publish(
            new AppointmentTypeDeletedSendIntegrationEvent(message),
            cancellationToken
        );
    }

    private async Task SendNotificationRequestAsync(
        AppointmentType appointmentType,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new AppointmentTypeDeletedNotificationRequestFactory(
            appointmentType,
            correlationId,
            _currentUserService.UserId
        );

        await _notificationRequestService.CreateAndSendAsync(
            factory, cancellationToken);
    }
}