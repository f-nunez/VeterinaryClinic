using System.Text.Json;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.ReceiveIntegrationEvents.AppointmentTypeCreated;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.ReceiveIntegrationEvents.AppointmentTypeDeleted;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.ReceiveIntegrationEvents.AppointmentTypeUpdated;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.ReceiveIntegrationEvents.ClientCreated;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.ReceiveIntegrationEvents.ClientDeleted;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.ReceiveIntegrationEvents.ClientUpdated;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.ReceiveIntegrationEvents.ClinicCreated;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.ReceiveIntegrationEvents.ClinicDeleted;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.ReceiveIntegrationEvents.ClinicUpdated;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.ReceiveIntegrationEvents.DoctorCreated;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.ReceiveIntegrationEvents.DoctorDeleted;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.ReceiveIntegrationEvents.DoctorUpdated;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.ReceiveIntegrationEvents.PatientCreated;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.ReceiveIntegrationEvents.PatientDeleted;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.ReceiveIntegrationEvents.PatientUpdated;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.ReceiveIntegrationEvents.RoomCreated;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.ReceiveIntegrationEvents.RoomDeleted;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.ReceiveIntegrationEvents.RoomUpdated;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.IntegrationEvents;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.Factories;

public class IntegrationEventFactory : IIntegrationEventFactory
{
    public BaseIntegrationEvent GetDeserializedIntegrationEvent(
        IntegrationEvent integrationEvent,
        string serializedIntegrationEvent)
    {
        BaseIntegrationEvent? baseIntegrationEvent;

        switch (integrationEvent)
        {
            case IntegrationEvent.AppointmentTypeCreated:
                baseIntegrationEvent = JsonSerializer.Deserialize<AppointmentTypeCreatedIntegrationEvent>(serializedIntegrationEvent);
                break;
            case IntegrationEvent.AppointmentTypeDeleted:
                baseIntegrationEvent = JsonSerializer.Deserialize<AppointmentTypeDeletedIntegrationEvent>(serializedIntegrationEvent);
                break;
            case IntegrationEvent.AppointmentTypeUpdated:
                baseIntegrationEvent = JsonSerializer.Deserialize<AppointmentTypeUpdatedIntegrationEvent>(serializedIntegrationEvent);
                break;
            case IntegrationEvent.ClientCreated:
                baseIntegrationEvent = JsonSerializer.Deserialize<ClientCreatedIntegrationEvent>(serializedIntegrationEvent);
                break;
            case IntegrationEvent.ClientDeleted:
                baseIntegrationEvent = JsonSerializer.Deserialize<ClientDeletedIntegrationEvent>(serializedIntegrationEvent);
                break;
            case IntegrationEvent.ClientUpdated:
                baseIntegrationEvent = JsonSerializer.Deserialize<ClientUpdatedIntegrationEvent>(serializedIntegrationEvent);
                break;
            case IntegrationEvent.ClinicCreated:
                baseIntegrationEvent = JsonSerializer.Deserialize<ClinicCreatedIntegrationEvent>(serializedIntegrationEvent);
                break;
            case IntegrationEvent.ClinicDeleted:
                baseIntegrationEvent = JsonSerializer.Deserialize<ClinicDeletedIntegrationEvent>(serializedIntegrationEvent);
                break;
            case IntegrationEvent.ClinicUpdated:
                baseIntegrationEvent = JsonSerializer.Deserialize<ClinicUpdatedIntegrationEvent>(serializedIntegrationEvent);
                break;
            case IntegrationEvent.DoctorCreated:
                baseIntegrationEvent = JsonSerializer.Deserialize<DoctorCreatedIntegrationEvent>(serializedIntegrationEvent);
                break;
            case IntegrationEvent.DoctorDeleted:
                baseIntegrationEvent = JsonSerializer.Deserialize<DoctorDeletedIntegrationEvent>(serializedIntegrationEvent);
                break;
            case IntegrationEvent.DoctorUpdated:
                baseIntegrationEvent = JsonSerializer.Deserialize<DoctorUpdatedIntegrationEvent>(serializedIntegrationEvent);
                break;
            case IntegrationEvent.PatientCreated:
                baseIntegrationEvent = JsonSerializer.Deserialize<PatientCreatedIntegrationEvent>(serializedIntegrationEvent);
                break;
            case IntegrationEvent.PatientDeleted:
                baseIntegrationEvent = JsonSerializer.Deserialize<PatientDeletedIntegrationEvent>(serializedIntegrationEvent);
                break;
            case IntegrationEvent.PatientUpdated:
                baseIntegrationEvent = JsonSerializer.Deserialize<PatientUpdatedIntegrationEvent>(serializedIntegrationEvent);
                break;
            case IntegrationEvent.RoomCreated:
                baseIntegrationEvent = JsonSerializer.Deserialize<RoomCreatedIntegrationEvent>(serializedIntegrationEvent);
                break;
            case IntegrationEvent.RoomDeleted:
                baseIntegrationEvent = JsonSerializer.Deserialize<RoomDeletedIntegrationEvent>(serializedIntegrationEvent);
                break;
            case IntegrationEvent.RoomUpdated:
                baseIntegrationEvent = JsonSerializer.Deserialize<RoomUpdatedIntegrationEvent>(serializedIntegrationEvent);
                break;
            default:
                throw new ArgumentException(
                    $"{nameof(integrationEvent)} not found with value: {integrationEvent}");
        }

        if (baseIntegrationEvent is null)
            throw new ArgumentNullException(nameof(baseIntegrationEvent));

        return baseIntegrationEvent;
    }

    public object GetReceiveIntegrationEvent(
        IntegrationEvent integrationEvent,
        BaseIntegrationEvent baseIntegrationEvent)
    {
        object? receiveIntegrationEvent = null;

        switch (integrationEvent)
        {
            case IntegrationEvent.AppointmentTypeCreated:
                receiveIntegrationEvent = new AppointmentTypeCreatedReceiveIntegrationEvent
                (
                    (AppointmentTypeCreatedIntegrationEvent)baseIntegrationEvent
                );
                break;
            case IntegrationEvent.AppointmentTypeDeleted:
                receiveIntegrationEvent = new AppointmentTypeDeletedReceiveIntegrationEvent
                (
                    (AppointmentTypeDeletedIntegrationEvent)baseIntegrationEvent
                );
                break;
            case IntegrationEvent.AppointmentTypeUpdated:
                receiveIntegrationEvent = new AppointmentTypeUpdatedReceiveIntegrationEvent
                (
                    (AppointmentTypeUpdatedIntegrationEvent)baseIntegrationEvent
                );
                break;
            case IntegrationEvent.ClientCreated:
                receiveIntegrationEvent = new ClientCreatedReceiveIntegrationEvent
                (
                    (ClientCreatedIntegrationEvent)baseIntegrationEvent
                );
                break;
            case IntegrationEvent.ClientDeleted:
                receiveIntegrationEvent = new ClientDeletedReceiveIntegrationEvent
                (
                    (ClientDeletedIntegrationEvent)baseIntegrationEvent
                );
                break;
            case IntegrationEvent.ClientUpdated:
                receiveIntegrationEvent = new ClientUpdatedReceiveIntegrationEvent
                (
                    (ClientUpdatedIntegrationEvent)baseIntegrationEvent
                );
                break;
            case IntegrationEvent.ClinicCreated:
                receiveIntegrationEvent = new ClinicCreatedReceiveIntegrationEvent
                (
                    (ClinicCreatedIntegrationEvent)baseIntegrationEvent
                );
                break;
            case IntegrationEvent.ClinicDeleted:
                receiveIntegrationEvent = new ClinicDeletedReceiveIntegrationEvent
                (
                    (ClinicDeletedIntegrationEvent)baseIntegrationEvent
                );
                break;
            case IntegrationEvent.ClinicUpdated:
                receiveIntegrationEvent = new ClinicUpdatedReceiveIntegrationEvent
                (
                    (ClinicUpdatedIntegrationEvent)baseIntegrationEvent
                );
                break;
            case IntegrationEvent.DoctorCreated:
                receiveIntegrationEvent = new DoctorCreatedReceiveIntegrationEvent
                (
                    (DoctorCreatedIntegrationEvent)baseIntegrationEvent
                );
                break;
            case IntegrationEvent.DoctorDeleted:
                receiveIntegrationEvent = new DoctorDeletedReceiveIntegrationEvent
                (
                    (DoctorDeletedIntegrationEvent)baseIntegrationEvent
                );
                break;
            case IntegrationEvent.DoctorUpdated:
                receiveIntegrationEvent = new DoctorUpdatedReceiveIntegrationEvent
                (
                    (DoctorUpdatedIntegrationEvent)baseIntegrationEvent
                );
                break;
            case IntegrationEvent.PatientCreated:
                receiveIntegrationEvent = new PatientCreatedReceiveIntegrationEvent
                (
                    (PatientCreatedIntegrationEvent)baseIntegrationEvent
                );
                break;
            case IntegrationEvent.PatientDeleted:
                receiveIntegrationEvent = new PatientDeletedReceiveIntegrationEvent
                (
                    (PatientDeletedIntegrationEvent)baseIntegrationEvent
                );
                break;
            case IntegrationEvent.PatientUpdated:
                receiveIntegrationEvent = new PatientUpdatedReceiveIntegrationEvent
                (
                    (PatientUpdatedIntegrationEvent)baseIntegrationEvent
                );
                break;
            case IntegrationEvent.RoomCreated:
                receiveIntegrationEvent = new RoomCreatedReceiveIntegrationEvent
                (
                    (RoomCreatedIntegrationEvent)baseIntegrationEvent
                );
                break;
            case IntegrationEvent.RoomDeleted:
                receiveIntegrationEvent = new RoomDeletedReceiveIntegrationEvent
                (
                    (RoomDeletedIntegrationEvent)baseIntegrationEvent
                );
                break;
            case IntegrationEvent.RoomUpdated:
                receiveIntegrationEvent = new RoomUpdatedReceiveIntegrationEvent
                (
                    (RoomUpdatedIntegrationEvent)baseIntegrationEvent
                );
                break;
            default:
                throw new ArgumentException(
                    $"{nameof(integrationEvent)} not found with value: {integrationEvent}");
        }

        if (receiveIntegrationEvent is null)
            throw new ArgumentNullException(nameof(receiveIntegrationEvent));

        return receiveIntegrationEvent;
    }
}