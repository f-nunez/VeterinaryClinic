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
        ClinicDeletedReceiveIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var contract = integrationEvent
            .ClinicDeletedIntegrationEventContract;

        string sql = @$"
        DELETE FROM [dbo].[Clinics]
        WHERE Id = {contract.ClinicId}";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}