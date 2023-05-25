using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.ReceiveIntegrationEvents.ClinicCreated;

public class ClinicCreatedReceiveIntegrationEventHandler
    : INotificationHandler<ClinicCreatedReceiveIntegrationEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public ClinicCreatedReceiveIntegrationEventHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        ClinicCreatedReceiveIntegrationEvent receiveIntegrationEvent,
        CancellationToken cancellationToken)
    {
        var integrationEvent = receiveIntegrationEvent
            .ClinicCreatedIntegrationEvent;

        string sql = @$"
        SET IDENTITY_INSERT [dbo].[Clinics] ON;

        INSERT [dbo].[Clinics] (
            [IsActive],
            [Id],
            [Address],
            [EmailAddress],
            [Name])
        VALUES (
            1,
            {integrationEvent.ClinicId},
            N'{integrationEvent.ClinicAddress}',
            N'{integrationEvent.ClinicEmailAddress}',
            N'{integrationEvent.ClinicName}'
        );
        
        SET IDENTITY_INSERT [dbo].[Clinics] OFF;";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}