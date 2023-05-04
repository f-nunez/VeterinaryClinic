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
        PatientUpdatedReceiveIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var contract = integrationEvent
            .PatientUpdatedIntegrationEventContract;
        
        var preferredDoctorId = contract.PatientPreferredDoctorId.HasValue
            ? $"{contract.PatientPreferredDoctorId}"
            : "NULL";

        string sql = @$"
        UPDATE [dbo].[Patients]
        SET [ClientId] = {contract.PatientClientId}
            ,[Name] = N'{contract.PatientName}'
            ,[AnimalSex] = {contract.PatientSex}
            ,[AnimalType_Breed] = N'{contract.PatientBreed}'
            ,[AnimalType_Species] = N'{contract.PatientSpecies}'
            ,[Photo_Name] = N'{contract.PatientPhotoName}'
            ,[Photo_StoredName] = N'{contract.PatientPhotoStoredName}'
            ,[PreferredDoctorId] = {preferredDoctorId}
        WHERE Id = {contract.PatientId}";

        var result = await _unitOfWork
            .ExecuteSqlCommandAsync(sql, cancellationToken);
    }
}