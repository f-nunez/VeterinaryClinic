using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.ValueObjects;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.NotificationRequest.Factories;

public class PatientCreatedNotificationRequestFactoryTests
{
    private readonly Guid _correlationId = Guid.NewGuid();
    private readonly PatientCreatedNotificationRequestFactory _factory;
    private readonly AnimalSex _patientAnimalSex = AnimalSex.Female;
    private readonly AnimalType _patientAnimalType = new AnimalType("a", "a");
    private readonly int _patientClientId = 1;
    private readonly int _patientId = 1;
    private readonly string _patientName = "a";
    private readonly Photo _patientPhoto = new Photo("a", "a");
    private readonly int? _patientPreferredPatientId = 1;
    private readonly string _userId = Guid.NewGuid().ToString();

    public PatientCreatedNotificationRequestFactoryTests()
    {
        var patient = new Patient(
            _patientId,
            _patientClientId,
            _patientName,
            _patientAnimalSex,
            _patientAnimalType,
            _patientPhoto,
            _patientPreferredPatientId
        );

        _factory = new PatientCreatedNotificationRequestFactory(
            patient,
            _correlationId,
            _userId
        );
    }

    [Fact]
    public void CreateNotificationRequest_WithoutEntries_ReturnsNotificationRequest()
    {
        // Act
        var notificationRequest = _factory.CreateNotificationRequest()
            as PatientCreatedNotificationRequest;

        // Assert
        Assert.Equal(_patientClientId, notificationRequest?.ClientId);
        Assert.Equal(_correlationId, notificationRequest?.CorrelationId);
        Assert.Equal(_patientName, notificationRequest?.Name);
        Assert.Equal(_patientId, notificationRequest?.PatientId);
        Assert.Equal(_userId, notificationRequest?.TriggeredByUserId);
    }

    [Fact]
    public void GetNotificationEvent_WithoutEntries_ReturnsNotificationEvent()
    {
        // Act
        var notificationEvent = _factory.GetNotificationEvent();

        // Assert
        Assert.Equal(
            NotificationEvent.PatientCreated.ToString(),
            notificationEvent
        );
    }
}