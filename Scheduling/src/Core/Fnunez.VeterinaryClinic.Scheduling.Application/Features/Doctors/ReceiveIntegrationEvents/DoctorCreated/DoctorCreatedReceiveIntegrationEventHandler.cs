using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.ReceiveIntegrationEvents.DoctorCreated;

public class DoctorCreatedReceiveIntegrationEventHandler
    : INotificationHandler<DoctorCreatedReceiveIntegrationEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public DoctorCreatedReceiveIntegrationEventHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        DoctorCreatedReceiveIntegrationEvent receiveIntegrationEvent,
        CancellationToken cancellationToken)
    {
        var integrationEvent = receiveIntegrationEvent
            .DoctorCreatedIntegrationEvent;

        string sql = @$"
        SET IDENTITY_INSERT [dbo].[Doctors] ON;

        INSERT [dbo].[Doctors] (
            [IsActive],
            [Id],
            [FullName]
        ) VALUES (
            1,
            {integrationEvent.DoctorId},
            N'{integrationEvent.DoctorFullName}'
        );
        
        SET IDENTITY_INSERT [dbo].[Doctors] OFF;";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}