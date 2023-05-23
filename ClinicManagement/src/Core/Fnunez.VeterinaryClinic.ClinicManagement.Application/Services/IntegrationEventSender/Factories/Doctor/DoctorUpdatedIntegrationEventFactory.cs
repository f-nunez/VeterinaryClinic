using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;

public class DoctorUpdatedIntegrationEventFactory
    : IIntegrationEventFactory
{
    private readonly Doctor _doctor;

    public DoctorUpdatedIntegrationEventFactory(Doctor doctor)
    {
        _doctor = doctor;
    }

    public BaseIntegrationEvent CreateIntegrationEvent()
    {
        return new DoctorUpdatedIntegrationEvent
        {
            DoctorFullName = _doctor.FullName,
            DoctorId = _doctor.Id
        };
    }

    public string GetIntegrationEvent()
    {
        return IntegrationEvent.DoctorUpdated.ToString();
    }
}