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
        PatientDeletedReceiveIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var contract = integrationEvent
            .PatientDeletedIntegrationEventContract;

        string sql = @$"
        UPDATE [dbo].[Patients]
        SET [IsActive] = 0
        WHERE Id = {contract.PatientId}";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}