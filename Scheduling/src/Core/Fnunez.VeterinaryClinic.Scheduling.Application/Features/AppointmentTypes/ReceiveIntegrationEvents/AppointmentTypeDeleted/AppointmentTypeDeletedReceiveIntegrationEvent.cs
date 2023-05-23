using Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.IntegrationEvents;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.ReceiveIntegrationEvents.AppointmentTypeDeleted;

public record AppointmentTypeDeletedReceiveIntegrationEvent(AppointmentTypeDeletedIntegrationEvent AppointmentTypeDeletedIntegrationEvent)
    : INotification;