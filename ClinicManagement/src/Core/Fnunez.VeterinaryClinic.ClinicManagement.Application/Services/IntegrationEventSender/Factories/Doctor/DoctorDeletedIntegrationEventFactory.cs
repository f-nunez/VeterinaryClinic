using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;

public class DoctorDeletedIntegrationEventFactory
    : IIntegrationEventFactory
{
    private readonly Doctor _doctor;

    public DoctorDeletedIntegrationEventFactory(Doctor doctor)
    {
        _doctor = doctor;
    }

    public BaseIntegrationEvent CreateIntegrationEvent()
    {
        return new DoctorDeletedIntegrationEvent
        {
            DoctorId = _doctor.Id
        };
    }

    public string GetIntegrationEvent()
    {
        return IntegrationEvent.DoctorDeleted.ToString();
    }
}