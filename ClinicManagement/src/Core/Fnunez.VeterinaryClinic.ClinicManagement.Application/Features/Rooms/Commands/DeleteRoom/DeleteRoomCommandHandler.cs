using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.DeleteRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Commands.DeleteRoom;

public class DeleteRoomCommandHandler
    : IRequestHandler<DeleteRoomCommand, DeleteRoomResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IIntegrationEventSenderService _integrationEventSenderService;
    private readonly IMediator _mediator;
    private readonly INotificationRequestService _notificationRequestService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoomCommandHandler(
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

    public async Task<DeleteRoomResponse> Handle(
        DeleteRoomCommand command,
        CancellationToken cancellationToken)
    {
        DeleteRoomRequest request = command.DeleteRoomRequest;
        var response = new DeleteRoomResponse(request.CorrelationId);

        var roomToDelete = await _unitOfWork
            .Repository<Room>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (roomToDelete is null)
            throw new NotFoundException(nameof(roomToDelete), request.Id);

        roomToDelete.SetUpdatedBy(_currentUserService.UserId);

        await _unitOfWork
            .Repository<Room>()
            .DeleteAsync(roomToDelete, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        await SendContractsToServiceBusAsync(
            roomToDelete,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private async Task SendContractsToServiceBusAsync(
        Room room,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        await SendIntegrationEventAsync(
            room,
            correlationId,
            cancellationToken
        );

        await SendNotificationRequestAsync(
            room,
            correlationId,
            cancellationToken
        );
    }

    private async Task SendIntegrationEventAsync(
        Room room,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new RoomDeletedIntegrationEventFactory(room);

        await _integrationEventSenderService.SendAsync(
            factory, correlationId, cancellationToken);
    }

    private async Task SendNotificationRequestAsync(
        Room room,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new RoomDeletedNotificationRequestFactory
        (
            room,
            correlationId,
            _currentUserService.UserId
        );

        await _notificationRequestService.SendAsync(
            factory, cancellationToken);
    }
}