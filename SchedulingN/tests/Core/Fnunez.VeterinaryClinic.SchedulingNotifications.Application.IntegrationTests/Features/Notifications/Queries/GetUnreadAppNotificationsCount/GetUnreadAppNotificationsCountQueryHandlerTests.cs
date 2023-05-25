using System.Text.Json;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Queries.GetUnreadAppNotificationsCount;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.AppNotificationAggregate;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.NotificationAggregate;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.NotificationAggregate.Enums;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.IntegrationTests.Features.Notifications.Queries.GetUnreadAppNotificationsCount;

[Collection(nameof(TestContextFixture))]
public class GetUnreadAppNotificationsCountQueryHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetUnreadAppNotificationsCountQueryHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_ReturnsGetUnreadAppNotificationsCountResponse()
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

        var appNotification = new AppNotification
        (
            Guid.NewGuid(),
            DateTimeOffset.UtcNow,
            notification.Id,
            notifyToUserId
        );

        await _fixture.AddAsync<AppNotification>(appNotification);

        var loggedUserId = _fixture.GetUserIdAsStaff();

        var request = new GetUnreadAppNotificationsCountRequest();

        var query = new GetUnreadAppNotificationsCountQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query, loggedUserId);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetUnreadAppNotificationsCountResponse>(actual);

        Assert.NotEqual(0, actual.Count);
    }
}