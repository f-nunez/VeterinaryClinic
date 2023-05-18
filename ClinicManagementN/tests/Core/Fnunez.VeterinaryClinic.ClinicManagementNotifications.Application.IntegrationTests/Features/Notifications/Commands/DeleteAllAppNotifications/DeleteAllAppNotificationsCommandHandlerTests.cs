using System.Text.Json;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Commands.DeleteAllAppNotifications;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.AppNotificationAggregate;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.NotificationAggregate;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.NotificationAggregate.Enums;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.IntegrationTests.Features.Notifications.Commands.DeleteAllAppNotifications;

[Collection(nameof(TestContextFixture))]
public class DeleteAllAppNotificationsCommandHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public DeleteAllAppNotificationsCommandHandlerTests(
        TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_ReturnsDeleteAllAppNotificationsResponse()
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

        var request = new DeleteAllAppNotificationsRequest();

        var command = new DeleteAllAppNotificationsCommand(request);

        var loggedUserId = _fixture.GetUserIdAsStaff();

        // Act
        var actual = await _fixture.SendAsync(command, loggedUserId);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<DeleteAllAppNotificationsResponse>(actual);

        var deletedAppNotification = await _fixture
            .GetByIdAsync<AppNotification>(appNotification.Id);

        Assert.NotNull(deletedAppNotification);

        Assert.False(deletedAppNotification.IsActive);
    }
}