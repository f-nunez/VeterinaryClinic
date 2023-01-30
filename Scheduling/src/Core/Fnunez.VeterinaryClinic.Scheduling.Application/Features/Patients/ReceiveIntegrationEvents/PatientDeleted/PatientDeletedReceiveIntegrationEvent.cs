using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.ReceiveIntegrationEvents.PatientDeleted;

public record PatientDeletedReceiveIntegrationEvent(PatientDeletedIntegrationEventContract PatientDeletedIntegrationEventContract)
    : INotification;