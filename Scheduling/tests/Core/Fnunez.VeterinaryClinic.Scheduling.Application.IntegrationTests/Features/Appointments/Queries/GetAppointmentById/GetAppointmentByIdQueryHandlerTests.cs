using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentById;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentById;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate.ValueObjects;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.Enums;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.ValueObjects;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClinicAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.DoctorAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.RoomAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.IntegrationTests.Features.Appointments.Queries.GetAppointmentById;

[Collection(nameof(TestContextFixture))]
public class GetAppointmentByIdQueryHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetAppointmentByIdQueryHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetAppointmentByIdRequest();

        var query = new GetAppointmentByIdQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetAppointmentByIdResponse()
    {
        // Arrange

        // AppointmentType
        var appointmentTypeCode = "code";

        var appointmentTypeDuration = 60;

        var appointmentTypeName = "name";

        var appointmentType = new AppointmentType
        (
            appointmentTypeName,
            appointmentTypeCode,
            appointmentTypeDuration
        );

        await _fixture.AddAsync<AppointmentType>(appointmentType);

        // Client
        var clientEmailAddress = "test@nunez.ninja";

        var clientFullName = "a";

        int? clientPreferredDoctorId = null;

        var clientPreferredLanguage = PreferredLanguage.English;

        var clientPreferredName = "b";

        var clientSalutation = "Mister a";


        var client = new Client
        (
            clientFullName,
            clientPreferredName,
            clientSalutation,
            clientEmailAddress,
            clientPreferredLanguage,
            clientPreferredDoctorId
        );

        await _fixture.AddAsync<Client>(client);

        // Clinic
        var clinicAddress = "a";

        var clinicEmailAddress = "test@nunez.ninja";

        var clinicName = "b";

        var clinic = new Clinic(clinicAddress, clinicEmailAddress, clinicName);

        await _fixture.AddAsync<Clinic>(clinic);

        // Doctor
        var doctorFullName = "a";

        var doctor = new Doctor(doctorFullName);

        await _fixture.AddAsync<Doctor>(doctor);

        // Patient
        var patientAnimalSex = AnimalSex.Female;

        var patientAnimalType = new AnimalType("a", "a");

        var patientName = "a";

        var patientPhoto = new Photo("a", "a");

        int? patientPreferredDoctorId = null;

        var patient = new Patient(
            client.Id,
            patientName,
            patientAnimalSex,
            patientAnimalType,
            patientPhoto,
            patientPreferredDoctorId
        );

        client.AddPatient(patient);

        await _fixture.UpdateAsync<Client>(client);

        // Room
        var roomName = "a";

        var room = new Room(roomName);

        await _fixture.AddAsync<Room>(room);

        // Appointment
        var appointmentDescription = "description";

        var appointmentEndOn = DateTimeOffset.UtcNow;

        var appointmentId = Guid.NewGuid();

        var appointmentStartOn = appointmentEndOn.AddHours(-1);

        var appointmentTitle = "title";

        var appointment = new Appointment
        (
            appointmentId,
            appointmentType.Id,
            client.Id,
            clinic.Id,
            doctor.Id,
            patient.Id,
            room.Id,
            new DateTimeOffsetRange(appointmentStartOn, appointmentEndOn),
            appointmentDescription,
            appointmentTitle
        );

        await _fixture.AddAsync<Appointment>(appointment);

        var request = new GetAppointmentByIdRequest
        {
            AppointmentId = appointment.Id
        };

        var query = new GetAppointmentByIdQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetAppointmentByIdResponse>(actual);

        Assert.NotNull(actual.Appointment);

        Assert.Equal(appointmentType.Id, actual.Appointment.AppointmentTypeId);

        Assert.Equal(client.Id, actual.Appointment.ClientId);

        Assert.Equal(clinic.Id, actual.Appointment.ClinicId);

        Assert.Equal(appointmentDescription, actual.Appointment.Description);

        Assert.Equal(doctor.Id, actual.Appointment.DoctorId);

        Assert.Equal(appointmentEndOn, actual.Appointment.EndOn);

        Assert.Equal(patient.Id, actual.Appointment.PatientId);

        Assert.Equal(room.Id, actual.Appointment.RoomId);

        Assert.Equal(appointmentStartOn, actual.Appointment.StartOn);

        Assert.Equal(appointmentTitle, actual.Appointment.Title);
    }
}