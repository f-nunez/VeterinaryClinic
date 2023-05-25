using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationHub;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.UnitTests.Services.NotificationEngine;

public class NotificationEngineServiceTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task CreateAndNotifyAsync_NotificationEventStringIsEmpty_ThrowsArgumentException(
        string notificationEventString)
    {
        // Arrange
        var mockINotificationHubService = new Mock<INotificationHubService>();

        var mockINotificationRequestFactory = new Mock<INotificationRequestFactory>();

        var mockIPayloadFactory = new Mock<IPayloadFactory>();

        var mockIUnitOfWork = new Mock<IUnitOfWork>();

        var notificationEngineService = new NotificationEngineService
        (
            mockINotificationHubService.Object,
            mockINotificationRequestFactory.Object,
            mockIPayloadFactory.Object,
            mockIUnitOfWork.Object
        );

        var expectedErrorMessage = $"{nameof(notificationEventString)} is empty.";

        var serializedNotificationRequest = "serializedNotificationRequest";

        // Act
        var actual = await Assert.ThrowsAsync<ArgumentException>(() =>
            notificationEngineService.CreateAndNotifyAsync(
                notificationEventString,
                serializedNotificationRequest,
                CancellationToken.None
            )
        );

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ArgumentException>(actual);

        Assert.Equal(expectedErrorMessage, actual.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task CreateAndNotifyAsync_SerializedNotificationRequestIsEmpty_ThrowsArgumentException(
        string serializedNotificationRequest)
    {
        // Arrange
        var mockINotificationHubService = new Mock<INotificationHubService>();

        var mockINotificationRequestFactory = new Mock<INotificationRequestFactory>();

        var mockIPayloadFactory = new Mock<IPayloadFactory>();

        var mockIUnitOfWork = new Mock<IUnitOfWork>();

        var notificationEngineService = new NotificationEngineService
        (
            mockINotificationHubService.Object,
            mockINotificationRequestFactory.Object,
            mockIPayloadFactory.Object,
            mockIUnitOfWork.Object
        );

        var expectedErrorMessage = $"{nameof(serializedNotificationRequest)} is empty.";

        var notificationEventString = "notificationEventString";

        // Act
        var actual = await Assert.ThrowsAsync<ArgumentException>(() =>
            notificationEngineService.CreateAndNotifyAsync(
                notificationEventString,
                serializedNotificationRequest,
                CancellationToken.None
            )
        );

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ArgumentException>(actual);

        Assert.Equal(expectedErrorMessage, actual.Message);
    }
}