using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.ReceiveIntegrationEvents.AppointmentTypeUpdated;

public class AppointmentTypeUpdatedReceiveIntegrationEventHandler
    : INotificationHandler<AppointmentTypeUpdatedReceiveIntegrationEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentTypeUpdatedReceiveIntegrationEventHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        AppointmentTypeUpdatedReceiveIntegrationEvent receiveIntegrationEvent,
        CancellationToken cancellationToken)
    {
        var integrationEvent = receiveIntegrationEvent
            .AppointmentTypeUpdatedIntegrationEvent;

        string sql = @$"
        UPDATE [dbo].[AppointmentTypes]
        SET [Name] = N'{integrationEvent.AppointmentTypeName}'
            ,[Code] = N'{integrationEvent.AppointmentTypeCode}'
            ,[Duration] = {integrationEvent.AppointmentTypeDuration}
        WHERE Id = {integrationEvent.AppointmentTypeId};";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}