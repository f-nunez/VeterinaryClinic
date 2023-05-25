using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.ReceiveIntegrationEvents.PatientUpdated;

public class PatientUpdatedReceiveIntegrationEventHandler
    : INotificationHandler<PatientUpdatedReceiveIntegrationEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public PatientUpdatedReceiveIntegrationEventHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        PatientUpdatedReceiveIntegrationEvent receiveIntegrationEvent,
        CancellationToken cancellationToken)
    {
        var integrationEvent = receiveIntegrationEvent
            .PatientUpdatedIntegrationEvent;

        var preferredDoctorId = integrationEvent.PatientPreferredDoctorId.HasValue
            ? $"{integrationEvent.PatientPreferredDoctorId}"
            : "NULL";

        string sql = @$"
        UPDATE [dbo].[Patients]
        SET [ClientId] = {integrationEvent.PatientClientId}
            ,[Name] = N'{integrationEvent.PatientName}'
            ,[AnimalSex] = {integrationEvent.PatientSex}
            ,[AnimalType_Breed] = N'{integrationEvent.PatientBreed}'
            ,[AnimalType_Species] = N'{integrationEvent.PatientSpecies}'
            ,[Photo_Name] = N'{integrationEvent.PatientPhotoName}'
            ,[Photo_StoredName] = N'{integrationEvent.PatientPhotoStoredName}'
            ,[PreferredDoctorId] = {preferredDoctorId}
        WHERE Id = {integrationEvent.PatientId}";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}