using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;

public class DoctorCreatedIntegrationEventFactory
    : IIntegrationEventFactory
{
    private readonly Doctor _doctor;

    public DoctorCreatedIntegrationEventFactory(Doctor doctor)
    {
        _doctor = doctor;
    }

    public BaseIntegrationEvent CreateIntegrationEvent()
    {
        return new DoctorCreatedIntegrationEvent
        {
            DoctorFullName = _doctor.FullName,
            DoctorId = _doctor.Id
        };
    }

    public string GetIntegrationEvent()
    {
        return IntegrationEvent.DoctorCreated.ToString();
    }
}