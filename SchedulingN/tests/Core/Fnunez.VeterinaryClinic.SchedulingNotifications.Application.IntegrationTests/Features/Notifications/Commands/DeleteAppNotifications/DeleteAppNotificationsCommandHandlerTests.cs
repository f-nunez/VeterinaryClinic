using System.Text.Json;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Commands.DeleteAppNotifications;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.AppNotificationAggregate;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.NotificationAggregate;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.NotificationAggregate.Enums;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.IntegrationTests.Features.Notifications.Commands.DeleteAppNotifications;

[Collection(nameof(TestContextFixture))]
public class DeleteAppNotificationsCommandHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public DeleteAppNotificationsCommandHandlerTests(
        TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new DeleteAppNotificationsRequest();

        var command = new DeleteAppNotificationsCommand(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(command));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsDeleteAppNotificationsResponse()
    {
        // Arrange
        var notificationPayload = JsonSerializer.Serialize(
            new AppointmentCreatedPayload
            {
                Id = Guid.NewGuid(),
                Title = "title"
            }
        );

        var triggeredByUserId = _fixture.GetUserIdAsManager();

        var notification = new Notification
        (
            Guid.NewGuid(),
            DateTimeOffset.UtcNow,
            NotificationEvent.AppointmentCreated,
            notificationPayload,
            triggeredByUserId
        );

        await _fixture.AddAsync<Notification>(notification);

        var notifyToUserId = _fixture.GetUserIdAsStaff();

        var firstAppNotification = new AppNotification
        (
            Guid.NewGuid(),
            DateTimeOffset.UtcNow,
            notification.Id,
            notifyToUserId
        );

        await _fixture.AddAsync<AppNotification>(firstAppNotification);

        var secondAppNotification = new AppNotification
        (
            Guid.NewGuid(),
            DateTimeOffset.UtcNow,
            notification.Id,
            notifyToUserId
        );

        await _fixture.AddAsync<AppNotification>(secondAppNotification);

        var request = new DeleteAppNotificationsRequest
        {
            AppNotificationIds = new()
            {
                firstAppNotification.Id,
                secondAppNotification.Id
            }
        };

        var command = new DeleteAppNotificationsCommand(request);

        var loggedUserId = _fixture.GetUserIdAsStaff();

        // Act
        var actual = await _fixture.SendAsync(command, loggedUserId);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<DeleteAppNotificationsResponse>(actual);

        var firstDeletedAppNotification = await _fixture
            .GetByIdAsync<AppNotification>(firstAppNotification.Id);

        Assert.NotNull(firstDeletedAppNotification);

        Assert.False(firstDeletedAppNotification.IsActive);

        var secondDeletedAppNotification = await _fixture
            .GetByIdAsync<AppNotification>(secondAppNotification.Id);

        Assert.NotNull(secondDeletedAppNotification);

        Assert.False(secondDeletedAppNotification.IsActive);
    }
}