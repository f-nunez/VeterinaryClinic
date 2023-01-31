using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.ReceiveIntegrationEvents.AppointmentTypeDeleted;

public class AppointmentTypeDeletedReceiveIntegrationEventHandler
    : INotificationHandler<AppointmentTypeDeletedReceiveIntegrationEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentTypeDeletedReceiveIntegrationEventHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        AppointmentTypeDeletedReceiveIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var contract = integrationEvent
            .AppointmentTypeDeletedIntegrationEventContract;

        string sql = @$"
        UPDATE [dbo].[AppointmentTypes]
        SET [IsActive] = 0
        WHERE Id = {contract.AppointmentTypeId}";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}