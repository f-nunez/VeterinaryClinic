using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.ValueObjects;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.NotificationRequest.Factories;

public class PatientDeletedNotificationRequestFactoryTests
{
    private readonly Guid _correlationId = Guid.NewGuid();
    private readonly PatientDeletedNotificationRequestFactory _factory;
    private readonly AnimalSex _patientAnimalSex = AnimalSex.Female;
    private readonly AnimalType _patientAnimalType = new AnimalType("a", "a");
    private readonly int _patientClientId = 1;
    private readonly int _patientId = 1;
    private readonly string _patientName = "a";
    private readonly Photo _patientPhoto = new Photo("a", "a");
    private readonly int? _patientPreferredDoctorId = 1;
    private readonly string _userId = Guid.NewGuid().ToString();

    public PatientDeletedNotificationRequestFactoryTests()
    {
        var patient = new Patient
        (
            _patientId,
            _patientClientId,
            _patientName,
            _patientAnimalSex,
            _patientAnimalType,
            _patientPhoto,
            _patientPreferredDoctorId
        );

        _factory = new PatientDeletedNotificationRequestFactory
        (
            patient,
            _correlationId,
            _userId
        );
    }

    [Fact]
    public void CreateNotificationRequest_ReturnsPatientDeletedNotificationRequest()
    {
        // Act
        var actual = _factory.CreateNotificationRequest();

        // Assert
        Assert.IsType<PatientDeletedNotificationRequest>(actual);

        var notificationRequest = actual as PatientDeletedNotificationRequest;

        Assert.Equal(_patientClientId, notificationRequest?.ClientId);

        Assert.Equal(_correlationId, notificationRequest?.CorrelationId);

        Assert.Equal(_patientName, notificationRequest?.Name);

        Assert.Equal(_patientId, notificationRequest?.PatientId);

        Assert.Equal(_userId, notificationRequest?.TriggeredByUserId);
    }

    [Fact]
    public void GetNotificationEvent_ReturnsNotificationEvent()
    {
        // Act
        var notificationEvent = _factory.GetNotificationEvent();

        // Assert
        Assert.Equal(
            NotificationEvent.PatientDeleted.ToString(),
            notificationEvent
        );
    }
}