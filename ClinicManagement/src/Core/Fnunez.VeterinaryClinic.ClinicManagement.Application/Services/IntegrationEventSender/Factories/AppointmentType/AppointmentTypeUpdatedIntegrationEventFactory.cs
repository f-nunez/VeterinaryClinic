using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;

public class AppointmentTypeUpdatedIntegrationEventFactory
    : IIntegrationEventFactory
{
    private readonly AppointmentType _appointmentType;

    public AppointmentTypeUpdatedIntegrationEventFactory(
        AppointmentType appointmentType)
    {
        _appointmentType = appointmentType;
    }

    public BaseIntegrationEvent CreateIntegrationEvent()
    {
        return new AppointmentTypeUpdatedIntegrationEvent
        {
            AppointmentTypeCode = _appointmentType.Code,
            AppointmentTypeDuration = _appointmentType.Duration,
            AppointmentTypeId = _appointmentType.Id,
            AppointmentTypeName = _appointmentType.Name
        };
    }

    public string GetIntegrationEvent()
    {
        return IntegrationEvent.AppointmentTypeUpdated.ToString();
    }
}