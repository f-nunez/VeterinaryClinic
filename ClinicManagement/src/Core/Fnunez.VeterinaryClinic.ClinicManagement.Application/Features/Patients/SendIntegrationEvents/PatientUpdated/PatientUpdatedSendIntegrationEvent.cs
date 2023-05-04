using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.SendIntegrationEvents.PatientUpdated;

public record PatientUpdatedSendIntegrationEvent(PatientUpdatedIntegrationEventContract PatientUpdatedIntegrationEventContract)
    : INotification;