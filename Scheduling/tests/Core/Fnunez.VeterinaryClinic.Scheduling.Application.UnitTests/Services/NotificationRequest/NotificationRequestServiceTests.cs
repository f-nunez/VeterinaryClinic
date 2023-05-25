using Contracts.Scheduling;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate.ValueObjects;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Services.NotificationRequest;

public class NotificationRequestServiceTests
{
    private readonly int _appointmentTypeId = 1;
    private readonly int _clientId = 1;
    private readonly int _clinicId = 1;
    private readonly DateTimeOffset _confirmOn = DateTimeOffset.UtcNow.AddDays(1);
    private readonly DateTimeOffset _dateRangeStartOn = DateTimeOffset.UtcNow;
    private readonly DateTimeOffset _dateRangeEndOn = DateTimeOffset.UtcNow.AddMinutes(1);
    private readonly string _description = "a";
    private readonly int _doctorId = 1;
    private readonly Guid _id = Guid.NewGuid();
    private readonly int _patientId = 1;
    private readonly int _roomId = 1;
    private readonly string _title = "a";

    private readonly Guid _correlationId = Guid.NewGuid();
    private readonly AppointmentCreatedNotificationRequestFactory _factory;
    private readonly string _userId = Guid.NewGuid().ToString();

    public NotificationRequestServiceTests()
    {
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        _factory = new AppointmentCreatedNotificationRequestFactory
        (
            appointment,
            _correlationId,
            _userId
        );
    }

    [Fact]
    public async void SendAsync_CallsPublishAsyncMethodOnceFromServiceBus()
    {
        // Arrange
        var mockIServiceBus = new Mock<IServiceBus>();

        mockIServiceBus.Setup(x =>
            x.PublishAsync(
                It.IsAny<NotificationRequestSchedulingContract>(),
                CancellationToken.None
            )
        );

        var notificationRequestService = new NotificationRequestService(
            mockIServiceBus.Object);

        // Act
        await notificationRequestService.SendAsync(
            _factory, CancellationToken.None);

        // Assert
        mockIServiceBus.Verify(x =>
            x.PublishAsync(
                It.IsAny<NotificationRequestSchedulingContract>(),
                CancellationToken.None
            ),
            Times.Once()
        );
    }
}