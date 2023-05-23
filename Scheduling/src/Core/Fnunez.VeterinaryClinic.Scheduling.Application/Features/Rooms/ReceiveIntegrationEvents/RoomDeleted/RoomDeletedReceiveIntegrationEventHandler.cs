using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.ReceiveIntegrationEvents.RoomDeleted;

public class RoomDeletedReceiveIntegrationEventHandler
    : INotificationHandler<RoomDeletedReceiveIntegrationEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public RoomDeletedReceiveIntegrationEventHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        RoomDeletedReceiveIntegrationEvent receiveIntegrationEvent,
        CancellationToken cancellationToken)
    {
        var integrationEvent = receiveIntegrationEvent
            .RoomDeletedIntegrationEvent;

        string sql = @$"
        UPDATE [dbo].[Rooms]
        SET [IsActive] = 0
        WHERE Id = {integrationEvent.RoomId}";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}