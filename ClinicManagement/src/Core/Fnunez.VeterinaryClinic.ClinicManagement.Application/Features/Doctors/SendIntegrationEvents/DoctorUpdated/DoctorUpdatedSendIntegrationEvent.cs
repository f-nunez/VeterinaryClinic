using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.SendIntegrationEvents.DoctorUpdated;

public record DoctorUpdatedSendIntegrationEvent(DoctorUpdatedIntegrationEventContract DoctorUpdatedIntegrationEventContract)
    : INotification;