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
        DoctorDeletedReceiveIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var contract = integrationEvent
            .DoctorDeletedIntegrationEventContract;

        string sql = @$"
        DELETE FROM [dbo].[Doctors]
        WHERE Id = {contract.DoctorId}";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}