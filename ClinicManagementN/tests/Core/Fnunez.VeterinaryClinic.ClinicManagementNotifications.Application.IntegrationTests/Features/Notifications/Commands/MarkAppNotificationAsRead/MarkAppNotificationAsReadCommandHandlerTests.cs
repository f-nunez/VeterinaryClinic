using System.Text.Json;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Commands.MarkAppNotificationAsRead;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.AppNotificationAggregate;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.NotificationAggregate;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.NotificationAggregate.Enums;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.IntegrationTests.Features.Notifications.Commands.MarkAppNotificationAsRead;

[Collection(nameof(TestContextFixture))]
public class MarkAppNotificationAsReadCommandHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public MarkAppNotificationAsReadCommandHandlerTests(
        TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new MarkAppNotificationAsReadRequest();

        var command = new MarkAppNotificationAsReadCommand(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(command));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsMarkAppNotificationAsReadResponse()
    {
        // Arrange
        var notificationPayload = JsonSerializer.Serialize(
            new AppointmentTypeCreatedPayload
            {
                Id = 1,
                Name = "name"
            }
        );

        var triggeredByUserId = _fixture.GetUserIdAsManager();

        var notification = new Notification
        (
            Guid.NewGuid(),
            DateTimeOffset.UtcNow,
            NotificationEvent.AppointmentTypeCreated,
            notificationPayload,
            triggeredByUserId
        );

        await _fixture.AddAsync<Notification>(notification);

        var notifyToUserId = _fixture.GetUserIdAsStaff();

        var appNotification = new AppNotification
        (
            Guid.NewGuid(),
            DateTimeOffset.UtcNow,
            notification.Id,
            notifyToUserId
        );

        await _fixture.AddAsync<AppNotification>(appNotification);

        var request = new MarkAppNotificationAsReadRequest
        {
            AppNotificationId = appNotification.Id
        };

        var command = new MarkAppNotificationAsReadCommand(request);

        var loggedUserId = _fixture.GetUserIdAsStaff();

        // Act
        var actual = await _fixture.SendAsync(command, loggedUserId);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<MarkAppNotificationAsReadResponse>(actual);

        var appNotificationMarkedAsRead = await _fixture
            .GetByIdAsync<AppNotification>(appNotification.Id);

        Assert.NotNull(appNotificationMarkedAsRead);

        Assert.True(appNotificationMarkedAsRead.IsActive);

        Assert.NotNull(appNotificationMarkedAsRead.ReadOn);
    }
}