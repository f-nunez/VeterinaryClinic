using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.ReceiveIntegrationEvents.DoctorDeleted;

public record DoctorDeletedReceiveIntegrationEvent(DoctorDeletedIntegrationEventContract DoctorDeletedIntegrationEventContract)
    : INotification;