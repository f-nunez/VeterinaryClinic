using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.ReceiveIntegrationEvents.ClinicDeleted;

public class ClinicDeletedReceiveIntegrationEventHandler
    : INotificationHandler<ClinicDeletedReceiveIntegrationEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public ClinicDeletedReceiveIntegrationEventHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        ClinicDeletedReceiveIntegrationEvent receiveIntegrationEvent,
        CancellationToken cancellationToken)
    {
        var integrationEvent = receiveIntegrationEvent
            .ClinicDeletedIntegrationEvent;

        string sql = @$"
        UPDATE [dbo].[Clinics]
        SET [IsActive] = 0
        WHERE Id = {integrationEvent.ClinicId}";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}