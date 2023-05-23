using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.ReceiveIntegrationEvents.ClientDeleted;

public class ClientDeletedReceiveIntegrationEventHandler
    : INotificationHandler<ClientDeletedReceiveIntegrationEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public ClientDeletedReceiveIntegrationEventHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        ClientDeletedReceiveIntegrationEvent receiveIntegrationEvent,
        CancellationToken cancellationToken)
    {
        var integrationEvent = receiveIntegrationEvent
            .ClientDeletedIntegrationEvent;

        string sql = @$"
        UPDATE [dbo].[Clients]
        SET [IsActive] = 0
        WHERE Id = {integrationEvent.ClientId}";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}