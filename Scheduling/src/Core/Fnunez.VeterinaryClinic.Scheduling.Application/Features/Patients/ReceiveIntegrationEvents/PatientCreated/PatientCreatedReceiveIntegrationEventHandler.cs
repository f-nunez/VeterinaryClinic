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
        PatientCreatedReceiveIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var contract = integrationEvent
            .PatientCreatedIntegrationEventContract;

        var preferredDoctorId = contract.PatientPreferredDoctorId.HasValue
            ? $"{contract.PatientPreferredDoctorId}"
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
            {contract.PatientId},
            {contract.PatientClientId},
            N'{contract.PatientName}',
            {contract.PatientSex},
            N'{contract.PatientBreed}',
            N'{contract.PatientSpecies}',
            N'{contract.PatientPhotoName}',
            N'{contract.PatientPhotoStoredName}',
            {preferredDoctorId}
        );
        
        SET IDENTITY_INSERT [dbo].[Patients] OFF;";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}