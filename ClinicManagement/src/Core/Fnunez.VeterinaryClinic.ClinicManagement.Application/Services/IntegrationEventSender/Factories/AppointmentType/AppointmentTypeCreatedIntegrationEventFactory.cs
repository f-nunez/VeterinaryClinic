using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;

public class AppointmentTypeCreatedIntegrationEventFactory
    : IIntegrationEventFactory
{
    private readonly AppointmentType _appointmentType;

    public AppointmentTypeCreatedIntegrationEventFactory(
        AppointmentType appointmentType)
    {
        _appointmentType = appointmentType;
    }

    public BaseIntegrationEvent CreateIntegrationEvent()
    {
        return new AppointmentTypeCreatedIntegrationEvent
        {
            AppointmentTypeCode = _appointmentType.Code,
            AppointmentTypeDuration = _appointmentType.Duration,
            AppointmentTypeId = _appointmentType.Id,
            AppointmentTypeName = _appointmentType.Name
        };
    }

    public string GetIntegrationEvent()
    {
        return IntegrationEvent.AppointmentTypeCreated.ToString();
    }
}