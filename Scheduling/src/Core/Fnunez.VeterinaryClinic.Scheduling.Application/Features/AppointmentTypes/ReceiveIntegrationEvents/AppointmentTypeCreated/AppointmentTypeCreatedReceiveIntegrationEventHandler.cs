using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.ReceiveIntegrationEvents.AppointmentTypeCreated;

public class AppointmentTypeCreatedReceiveIntegrationEventHandler
    : INotificationHandler<AppointmentTypeCreatedReceiveIntegrationEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentTypeCreatedReceiveIntegrationEventHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        AppointmentTypeCreatedReceiveIntegrationEvent receiveIntegrationEvent,
        CancellationToken cancellationToken)
    {
        var integrationEvent = receiveIntegrationEvent
            .AppointmentTypeCreatedIntegrationEvent;

        string sql = @$"
        SET IDENTITY_INSERT [dbo].[AppointmentTypes] ON;

        INSERT [dbo].[AppointmentTypes] (
            [IsActive],
            [Id],
            [Name],
            [Code],
            [Duration]
        ) VALUES (
            1,
            {integrationEvent.AppointmentTypeId},
            N'{integrationEvent.AppointmentTypeName}',
            N'{integrationEvent.AppointmentTypeCode}',
            {integrationEvent.AppointmentTypeDuration}
        );
        
        SET IDENTITY_INSERT [dbo].[AppointmentTypes] OFF;";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}