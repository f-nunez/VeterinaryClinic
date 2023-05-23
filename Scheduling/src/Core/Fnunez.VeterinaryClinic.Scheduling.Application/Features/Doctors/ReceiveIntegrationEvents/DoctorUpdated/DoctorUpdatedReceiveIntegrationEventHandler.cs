using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.ReceiveIntegrationEvents.DoctorUpdated;

public class DoctorUpdatedReceiveIntegrationEventHandler
    : INotificationHandler<DoctorUpdatedReceiveIntegrationEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public DoctorUpdatedReceiveIntegrationEventHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        DoctorUpdatedReceiveIntegrationEvent receiveIntegrationEvent,
        CancellationToken cancellationToken)
    {
        var integrationEvent = receiveIntegrationEvent
            .DoctorUpdatedIntegrationEvent;

        string sql = @$"
        UPDATE [dbo].[Doctors]
        SET [FullName] = N'{integrationEvent.DoctorFullName}'
        WHERE Id = {integrationEvent.DoctorId}";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}