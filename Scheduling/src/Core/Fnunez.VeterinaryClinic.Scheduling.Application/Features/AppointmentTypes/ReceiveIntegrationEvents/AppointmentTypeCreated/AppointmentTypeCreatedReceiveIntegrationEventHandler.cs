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
        AppointmentTypeCreatedReceiveIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var contract = integrationEvent
            .AppointmentTypeCreatedIntegrationEventContract;

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
            {contract.AppointmentTypeId},
            N'{contract.AppointmentTypeName}',
            N'{contract.AppointmentTypeCode}',
            {contract.AppointmentTypeDuration}
        );
        
        SET IDENTITY_INSERT [dbo].[AppointmentTypes] OFF;";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}