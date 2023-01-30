using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.SendIntegrationEvents.PatientDeleted;

public record PatientDeletedSendIntegrationEvent(PatientDeletedIntegrationEventContract PatientDeletedIntegrationEventContract)
    : INotification;