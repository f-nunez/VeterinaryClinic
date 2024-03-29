using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.ReceiveIntegrationEvents.DoctorDeleted;

public class DoctorDeletedReceiveIntegrationEventHandler
    : INotificationHandler<DoctorDeletedReceiveIntegrationEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public DoctorDeletedReceiveIntegrationEventHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        DoctorDeletedReceiveIntegrationEvent receiveIntegrationEvent,
        CancellationToken cancellationToken)
    {
        var integrationEvent = receiveIntegrationEvent
            .DoctorDeletedIntegrationEvent;

        string sql = @$"
        UPDATE [dbo].[Doctors]
        SET [IsActive] = 0
        WHERE Id = {integrationEvent.DoctorId}";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}