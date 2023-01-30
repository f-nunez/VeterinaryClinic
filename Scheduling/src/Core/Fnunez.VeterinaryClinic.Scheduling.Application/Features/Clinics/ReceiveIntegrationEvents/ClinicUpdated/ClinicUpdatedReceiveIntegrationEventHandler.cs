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
        ClinicUpdatedReceiveIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var contract = integrationEvent
            .ClinicUpdatedIntegrationEventContract;

        string sql = @$"
        UPDATE [dbo].[Clinics]
        SET [Address] = N'{contract.ClinicAddress}'
            ,[EmailAddress] = N'{contract.ClinicEmailAddress}'
            ,[Name] = N'{contract.ClinicName}'
        WHERE Id = {contract.ClinicId}";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}