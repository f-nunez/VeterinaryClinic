using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.ReceiveIntegrationEvents.PatientCreated;

public class PatientCreatedReceiveIntegrationEventHandler
    : INotificationHandler<PatientCreatedReceiveIntegrationEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public PatientCreatedReceiveIntegrationEventHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        PatientCreatedReceiveIntegrationEvent receiveIntegrationEvent,
        CancellationToken cancellationToken)
    {
        var integrationEvent = receiveIntegrationEvent
            .PatientCreatedIntegrationEvent;

        var preferredDoctorId = integrationEvent.PatientPreferredDoctorId.HasValue
            ? $"{integrationEvent.PatientPreferredDoctorId}"
            : "NULL";

        string sql = @$"
        SET IDENTITY_INSERT [dbo].[Patients] ON;
        
        INSERT [dbo].[Patients] (
            [IsActive],
            [Id],
            [ClientId],
            [Name],
            [AnimalSex],
            [AnimalType_Breed],
            [AnimalType_Species],
            [Photo_Name],
            [Photo_StoredName],
            [PreferredDoctorId]
        ) VALUES (
            1,
            {integrationEvent.PatientId},
            {integrationEvent.PatientClientId},
            N'{integrationEvent.PatientName}',
            {integrationEvent.PatientSex},
            N'{integrationEvent.PatientBreed}',
            N'{integrationEvent.PatientSpecies}',
            N'{integrationEvent.PatientPhotoName}',
            N'{integrationEvent.PatientPhotoStoredName}',
            {preferredDoctorId}
        );
        
        SET IDENTITY_INSERT [dbo].[Patients] OFF;";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}