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
        ClientDeletedReceiveIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var contract = integrationEvent
            .ClientDeletedIntegrationEventContract;

        string sql = @$"
        DELETE FROM [dbo].[Clients]
        WHERE Id = {contract.ClientId}";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}