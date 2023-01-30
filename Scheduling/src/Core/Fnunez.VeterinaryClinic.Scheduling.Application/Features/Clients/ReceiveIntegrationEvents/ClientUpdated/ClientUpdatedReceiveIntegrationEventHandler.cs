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
        ClientUpdatedReceiveIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var contract = integrationEvent
            .ClientUpdatedIntegrationEventContract;

        string preferredDoctorId = contract.ClientPreferredDoctorId.HasValue
            ? $"{contract.ClientPreferredDoctorId}"
            : "NULL";

        string sql = @$"
        UPDATE [dbo].[Clients]
        SET [FullName] = N'{contract.ClientFullName}'
            ,[PreferredName] = N'{contract.ClientPreferredName}'
            ,[Salutation] = N'{contract.ClientSalutation}'
            ,[EmailAddress] = N'{contract.ClientEmailAddress}'
            ,[PreferredDoctorId] = {preferredDoctorId}
        WHERE Id = {contract.ClientId}";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}