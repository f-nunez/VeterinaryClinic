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
        AppointmentTypeUpdatedReceiveIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var contract = integrationEvent
            .AppointmentTypeUpdatedIntegrationEventContract;

        string sql = @$"
        UPDATE [dbo].[AppointmentTypes]
        SET [Name] = N'{contract.AppointmentTypeName}'
            ,[Code] = N'{contract.AppointmentTypeCode}'
            ,[Duration] = {contract.AppointmentTypeDuration}
        WHERE Id = {contract.AppointmentTypeId};";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}