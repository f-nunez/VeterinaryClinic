using AutoMapper;
using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.SendIntegrationEvents.RoomUpdated;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.UpdateRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Commands.UpdateRoom;

public class UpdateRoomCommandHandler
    : IRequestHandler<UpdateRoomCommand, UpdateRoomResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly INotificationRequestService _notificationRequestService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRoomCommandHandler(
        ICurrentUserService currentUserService,
        IMapper mapper,
        IMediator mediator,
        INotificationRequestService notificationRequestService,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _mapper = mapper;
        _mediator = mediator;
        _notificationRequestService = notificationRequestService;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateRoomResponse> Handle(
        UpdateRoomCommand command,
        CancellationToken cancellationToken)
    {
        UpdateRoomRequest request = command.UpdateRoomRequest;
        var response = new UpdateRoomResponse(request.CorrelationId);

        var roomToUpdate = await _unitOfWork
            .Repository<Room>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (roomToUpdate is null)
            throw new NotFoundException(nameof(roomToUpdate), request.Id);

        await UpdateRoomAsync(request, roomToUpdate, cancellationToken);

        var roomDto = _mapper.Map<RoomDto>(roomToUpdate);
        response.Room = roomDto;

        await SendContractsToServiceBusAsync(
            roomToUpdate,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private async Task UpdateRoomAsync(
        UpdateRoomRequest request,
        Room room,
        CancellationToken cancellationToken)
    {
        room.UpdateName(request.Name);
        room.SetUpdatedBy(_currentUserService.UserId);

        await _unitOfWork
            .Repository<Room>()
            .UpdateAsync(room, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);
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
        var message = new RoomUpdatedIntegrationEventContract
        {
            CausationId = correlationId,
            CorrelationId = correlationId,
            Id = Guid.NewGuid(),
            OccurredOn = DateTimeOffset.UtcNow,
            RoomId = room.Id,
            RoomName = room.Name
        };

        await _mediator.Publish(
            new RoomUpdatedSendIntegrationEvent(message),
            cancellationToken
        );
    }

    private async Task SendNotificationRequestAsync(
        Room room,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new RoomUpdatedNotificationRequestFactory(
            room,
            correlationId,
            _currentUserService.UserId
        );

        await _notificationRequestService.CreateAndSendAsync(
            factory, cancellationToken);
    }
}