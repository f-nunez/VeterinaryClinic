using System.Text.Json;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Queries.GetUnreadAppNotificationsCount;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.AppNotificationAggregate;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.NotificationAggregate;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.NotificationAggregate.Enums;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.IntegrationTests.Features.Notifications.Queries.GetUnreadAppNotificationsCount;

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