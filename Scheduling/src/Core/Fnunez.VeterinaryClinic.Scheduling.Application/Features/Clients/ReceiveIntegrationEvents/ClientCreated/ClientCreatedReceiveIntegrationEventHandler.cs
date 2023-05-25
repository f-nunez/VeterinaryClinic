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
        ClientCreatedReceiveIntegrationEvent receiveIntegrationEvent,
        CancellationToken cancellationToken)
    {
        var integrationEvent = receiveIntegrationEvent
            .ClientCreatedIntegrationEvent;

        string preferredDoctorId = integrationEvent.ClientPreferredDoctorId.HasValue
            ? $"{integrationEvent.ClientPreferredDoctorId}"
            : "NULL";

        string sql = @$"
        SET IDENTITY_INSERT [dbo].[Clients] ON;

        INSERT [dbo].[Clients] (
            [IsActive],
            [Id],
            [FullName],
            [PreferredName],
            [Salutation],
            [EmailAddress],
            [PreferredDoctorId],
            [PreferredLanguage]
        ) VALUES (
            1,
            {integrationEvent.ClientId},
            N'{integrationEvent.ClientFullName}',
            N'{integrationEvent.ClientPreferredName}',
            N'{integrationEvent.ClientSalutation}',
            N'{integrationEvent.ClientEmailAddress}',
            {preferredDoctorId},
            {integrationEvent.ClientPreferredLanguage}
        );
        
        SET IDENTITY_INSERT [dbo].[Clients] OFF;";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}