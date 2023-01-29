using AutoMapper;
using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.SendIntegrationEvents.RoomDeleted;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.DeleteRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Commands.DeleteRoom;

public class DeleteRoomCommandHandler
    : IRequestHandler<DeleteRoomCommand, DeleteRoomResponse>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoomCommandHandler(
        IMapper mapper,
        IMediator mediator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteRoomResponse> Handle(
        DeleteRoomCommand command,
        CancellationToken cancellationToken)
    {
        DeleteRoomRequest request = command.DeleteRoomRequest;
        var response = new DeleteRoomResponse(request.CorrelationId);
        var roomToDelete = _mapper.Map<Room>(request);

        await _unitOfWork.Repository<Room>()
            .DeleteAsync(roomToDelete, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        await SendIntegrationEventAsync(
            request.Id,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private async Task SendIntegrationEventAsync(
        int roomId,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var message = new RoomDeletedIntegrationEventContract
        {
            CausationId = correlationId,
            CorrelationId = correlationId,
            Id = Guid.NewGuid(),
            OccurredOn = DateTimeOffset.UtcNow,
            RoomId = roomId
        };

        await _mediator.Publish(
            new RoomDeletedSendIntegrationEvent(message),
            cancellationToken
        );
    }
}