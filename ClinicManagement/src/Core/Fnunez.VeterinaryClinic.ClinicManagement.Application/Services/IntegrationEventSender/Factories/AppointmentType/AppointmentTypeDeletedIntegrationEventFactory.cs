using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;

public class AppointmentTypeDeletedIntegrationEventFactory
    : IIntegrationEventFactory
{
    private readonly AppointmentType _appointmentType;

    public AppointmentTypeDeletedIntegrationEventFactory(
        AppointmentType appointmentType)
    {
        _appointmentType = appointmentType;
    }

    public BaseIntegrationEvent CreateIntegrationEvent()
    {
        return new AppointmentTypeDeletedIntegrationEvent
        {
            AppointmentTypeId = _appointmentType.Id
        };
    }

    public string GetIntegrationEvent()
    {
        return IntegrationEvent.AppointmentTypeDeleted.ToString();
    }
}