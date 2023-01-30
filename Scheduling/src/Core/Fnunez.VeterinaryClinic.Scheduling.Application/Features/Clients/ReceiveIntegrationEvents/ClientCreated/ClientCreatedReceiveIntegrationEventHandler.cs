using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.ReceiveIntegrationEvents.ClientCreated;

public class ClientCreatedReceiveIntegrationEventHandler
    : INotificationHandler<ClientCreatedReceiveIntegrationEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public ClientCreatedReceiveIntegrationEventHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        ClientCreatedReceiveIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var contract = integrationEvent
            .ClientCreatedIntegrationEventContract;

        string preferredDoctorId = contract.ClientPreferredDoctorId.HasValue
            ? $"{contract.ClientPreferredDoctorId}"
            : "NULL";

        string sql = @$"
        SET IDENTITY_INSERT [dbo].[Clients] ON;

        INSERT [dbo].[Clients] (
            [Id],
            [FullName],
            [PreferredName],
            [Salutation],
            [EmailAddress],
            [PreferredDoctorId]
        ) VALUES (
            {contract.ClientId},
            N'{contract.ClientFullName}',
            N'{contract.ClientPreferredName}',
            N'{contract.ClientSalutation}',
            N'{contract.ClientEmailAddress}',
            {preferredDoctorId}
        );
        
        SET IDENTITY_INSERT [dbo].[Clients] OFF;";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}