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
        RoomCreatedReceiveIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var contract = integrationEvent
            .RoomCreatedIntegrationEventContract;

        string sql = @$"
        SET IDENTITY_INSERT [dbo].[Rooms] ON;

        INSERT [dbo].[Rooms] (
            [Id],
            [Name]
        ) VALUES (
            {contract.RoomId},
            N'{contract.RoomName}'
        );
        
        SET IDENTITY_INSERT [dbo].[Rooms] OFF;";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}