using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.ReceiveIntegrationEvents.ClinicUpdated;

public class ClinicUpdatedReceiveIntegrationEventHandler
    : INotificationHandler<ClinicUpdatedReceiveIntegrationEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public ClinicUpdatedReceiveIntegrationEventHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        ClinicUpdatedReceiveIntegrationEvent receiveIntegrationEvent,
        CancellationToken cancellationToken)
    {
        var integrationEvent = receiveIntegrationEvent
            .ClinicUpdatedIntegrationEvent;

        string sql = @$"
        UPDATE [dbo].[Clinics]
        SET [Address] = N'{integrationEvent.ClinicAddress}'
            ,[EmailAddress] = N'{integrationEvent.ClinicEmailAddress}'
            ,[Name] = N'{integrationEvent.ClinicName}'
        WHERE Id = {integrationEvent.ClinicId}";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}