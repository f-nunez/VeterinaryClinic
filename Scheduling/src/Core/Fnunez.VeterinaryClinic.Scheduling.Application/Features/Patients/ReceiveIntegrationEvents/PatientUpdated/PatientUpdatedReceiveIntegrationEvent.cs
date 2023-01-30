using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.ReceiveIntegrationEvents.PatientUpdated;

public record PatientUpdatedReceiveIntegrationEvent(PatientUpdatedIntegrationEventContract PatientUpdatedIntegrationEventContract)
    : INotification;