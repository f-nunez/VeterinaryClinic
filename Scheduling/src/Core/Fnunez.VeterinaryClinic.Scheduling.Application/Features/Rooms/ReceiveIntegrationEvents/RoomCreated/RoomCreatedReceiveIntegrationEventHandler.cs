using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.ReceiveIntegrationEvents.RoomCreated;

public class RoomCreatedReceiveIntegrationEventHandler
    : INotificationHandler<RoomCreatedReceiveIntegrationEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public RoomCreatedReceiveIntegrationEventHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        RoomCreatedReceiveIntegrationEvent receiveIntegrationEvent,
        CancellationToken cancellationToken)
    {
        var integrationEvent = receiveIntegrationEvent
            .RoomCreatedIntegrationEvent;

        string sql = @$"
        SET IDENTITY_INSERT [dbo].[Rooms] ON;

        INSERT [dbo].[Rooms] (
            [IsActive],
            [Id],
            [Name]
        ) VALUES (
            1,
            {integrationEvent.RoomId},
            N'{integrationEvent.RoomName}'
        );
        
        SET IDENTITY_INSERT [dbo].[Rooms] OFF;";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}