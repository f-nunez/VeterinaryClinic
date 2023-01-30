using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.ReceiveIntegrationEvents.PatientCreated;

public record PatientCreatedReceiveIntegrationEvent(PatientCreatedIntegrationEventContract PatientCreatedIntegrationEventContract)
    : INotification;