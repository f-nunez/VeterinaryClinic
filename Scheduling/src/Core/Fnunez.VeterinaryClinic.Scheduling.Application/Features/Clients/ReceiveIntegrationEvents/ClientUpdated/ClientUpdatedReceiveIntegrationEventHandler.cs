using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.ReceiveIntegrationEvents.ClientUpdated;

public class ClientUpdatedReceiveIntegrationEventHandler
    : INotificationHandler<ClientUpdatedReceiveIntegrationEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public ClientUpdatedReceiveIntegrationEventHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        ClientUpdatedReceiveIntegrationEvent receiveIntegrationEvent,
        CancellationToken cancellationToken)
    {
        var integrationEvent = receiveIntegrationEvent
            .ClientUpdatedIntegrationEvent;

        string preferredDoctorId = integrationEvent.ClientPreferredDoctorId.HasValue
            ? $"{integrationEvent.ClientPreferredDoctorId}"
            : "NULL";

        string sql = @$"
        UPDATE [dbo].[Clients]
        SET [FullName] = N'{integrationEvent.ClientFullName}'
            ,[PreferredName] = N'{integrationEvent.ClientPreferredName}'
            ,[Salutation] = N'{integrationEvent.ClientSalutation}'
            ,[EmailAddress] = N'{integrationEvent.ClientEmailAddress}'
            ,[PreferredDoctorId] = {preferredDoctorId}
            ,[PreferredLanguage] = {integrationEvent.ClientPreferredLanguage}
        WHERE Id = {integrationEvent.ClientId}";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}