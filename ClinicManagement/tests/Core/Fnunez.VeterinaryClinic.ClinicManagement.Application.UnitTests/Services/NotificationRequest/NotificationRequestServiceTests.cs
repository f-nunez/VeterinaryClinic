using ClinicManagementContracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.NotificationRequest;

public class NotificationRequestServiceTests
{
    private readonly string _appointmentTypeCode = "c";
    private readonly int _appointmentTypeDuration = 1;
    private readonly int _appointmentTypeId = 1;
    private readonly string _appointmentTypeName = "n";
    private readonly Guid _correlationId = Guid.NewGuid();
    private readonly AppointmentTypeCreatedNotificationRequestFactory _factory;
    private readonly string _userId = Guid.NewGuid().ToString();

    public NotificationRequestServiceTests()
    {
        var appointmentType = new AppointmentType(
            _appointmentTypeId,
            _appointmentTypeName,
            _appointmentTypeCode,
            _appointmentTypeDuration
        );

        _factory = new AppointmentTypeCreatedNotificationRequestFactory(
            appointmentType,
            _correlationId,
            _userId
        );
    }

    [Fact]
    public async void CreateAndSendAsync_CallsPublishAsyncMethodOnceFromServiceBus()
    {
        // Arrange
        var mockIServiceBus = new Mock<IServiceBus>();

        mockIServiceBus.Setup(x =>
            x.PublishAsync(
                It.IsAny<NotificationRequestContract>(),
                CancellationToken.None
            )
        );

        var mockNotificationRequestService = new Mock<NotificationRequestService>(
            mockIServiceBus.Object);

        // Act
        await mockNotificationRequestService.Object.CreateAndSendAsync(
            _factory, CancellationToken.None);

        // Assert
        mockIServiceBus.Verify(x =>
            x.PublishAsync(
                It.IsAny<NotificationRequestContract>(),
                CancellationToken.None
            ),
            Times.Once()
        );
    }
}