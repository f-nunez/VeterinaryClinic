using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.ReceiveIntegrationEvents.PatientDeleted;

public class PatientDeletedReceiveIntegrationEventHandler
    : INotificationHandler<PatientDeletedReceiveIntegrationEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public PatientDeletedReceiveIntegrationEventHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        PatientDeletedReceiveIntegrationEvent receiveIntegrationEvent,
        CancellationToken cancellationToken)
    {
        var integrationEvent = receiveIntegrationEvent
            .PatientDeletedIntegrationEvent;

        string sql = @$"
        UPDATE [dbo].[Patients]
        SET [IsActive] = 0
        WHERE Id = {integrationEvent.PatientId}";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}