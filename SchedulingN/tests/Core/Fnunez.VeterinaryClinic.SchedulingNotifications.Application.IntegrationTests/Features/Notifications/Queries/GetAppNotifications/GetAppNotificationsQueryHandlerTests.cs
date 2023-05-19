using System.Text.Json;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Queries.GetAppNotifications;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.AppNotificationAggregate;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.NotificationAggregate;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.NotificationAggregate.Enums;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.IntegrationTests.Features.Notifications.Queries.GetAppNotifications;

[Collection(nameof(TestContextFixture))]
public class GetAppNotificationsQueryHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetAppNotificationsQueryHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetAppNotificationsRequest();

        var query = new GetAppNotificationsQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetAppNotificationsResponse()
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

        var loggedUserId = _fixture.GetUserIdAsStaff();

        var take = 2;

        var request = new GetAppNotificationsRequest { Take = take };

        var query = new GetAppNotificationsQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query, loggedUserId);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetAppNotificationsResponse>(actual);

        Assert.NotEmpty(actual.AppNotifications);

        Assert.Equal(take, actual.AppNotifications.Count);
    }
}