using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.AppNotificationAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.UnitTests.AppNotificationAggregate;

public class AppNotificationTests
{
    [Fact]
    public void Constructor_CreatedOn_SetsCreatedOnProperty()
    {
        // Arrange
        var id = Guid.NewGuid();

        var createdOn = DateTimeOffset.UtcNow;

        var notificationId = 1;

        var userId = Guid.NewGuid().ToString();

        var appNotification = new AppNotification
        (
            id,
            createdOn,
            notificationId,
            userId
        );

        // Assert
        Assert.Equal(createdOn, appNotification.CreatedOn);
    }

    [Fact]
    public void Constructor_Id_SetsIdProperty()
    {
        // Arrange
        var id = Guid.NewGuid();

        var createdOn = DateTimeOffset.UtcNow;

        var notificationId = 1;

        var userId = Guid.NewGuid().ToString();

        var appNotification = new AppNotification
        (
            id,
            createdOn,
            notificationId,
            userId
        );

        // Assert
        Assert.Equal(id, appNotification.Id);
    }

    [Fact]
    public void Constructor_IdIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var id = Guid.Empty;

        var createdOn = DateTimeOffset.UtcNow;

        var notificationId = 1;

        var userId = Guid.NewGuid().ToString();

        // Act
        Action actual = () => new AppNotification
        (
            id,
            createdOn,
            notificationId,
            userId
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_NotificationId_SetsNotificationIdProperty()
    {
        // Arrange
        var id = Guid.NewGuid();

        var createdOn = DateTimeOffset.UtcNow;

        var notificationId = 1;

        var userId = Guid.NewGuid().ToString();

        var appNotification = new AppNotification
        (
            id,
            createdOn,
            notificationId,
            userId
        );

        // Assert
        Assert.Equal(notificationId, appNotification.NotificationId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_NotificationIdIsLessOrEqualToZero_ThrowsArgumentException(
        int notificationId)
    {
        // Arrange
        var id = Guid.NewGuid();

        var createdOn = DateTimeOffset.UtcNow;

        var userId = Guid.NewGuid().ToString();

        // Act
        Action actual = () => new AppNotification
        (
            id,
            createdOn,
            notificationId,
            userId
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_UserId_SetsUserIdProperty()
    {
        // Arrange
        var id = Guid.NewGuid();

        var createdOn = DateTimeOffset.UtcNow;

        var notificationId = 1;

        var userId = Guid.NewGuid().ToString();

        var appNotification = new AppNotification
        (
            id,
            createdOn,
            notificationId,
            userId
        );

        // Assert
        Assert.Equal(userId, appNotification.UserId);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_UserIdIsEmpty_ThrowsArgumentException(
        string userId)
    {
        // Arrange
        var id = Guid.NewGuid();

        var notificationId = 1;

        var createdOn = DateTimeOffset.UtcNow;

        // Act
        Action actual = () => new AppNotification
        (
            id,
            createdOn,
            notificationId,
            userId
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void UpdateDeletedOn_DeletedOn_UpdatesDeletedOnProperty()
    {
        // Arrange
        var id = Guid.NewGuid();

        var createdOn = DateTimeOffset.UtcNow;

        var notificationId = 1;

        var userId = Guid.NewGuid().ToString();

        var appNotification = new AppNotification
        (
            id,
            createdOn,
            notificationId,
            userId
        );

        var deletedOn = DateTimeOffset.UtcNow;

        // Act
        appNotification.UpdateDeletedOn(deletedOn);

        // Assert
        Assert.Equal(deletedOn, appNotification.DeletedOn);
    }

    [Fact]
    public void UpdateReadOn_ReadOn_UpdatesReadOnProperty()
    {
        // Arrange
        var id = Guid.NewGuid();

        var createdOn = DateTimeOffset.UtcNow;

        var notificationId = 1;

        var userId = Guid.NewGuid().ToString();

        var appNotification = new AppNotification
        (
            id,
            createdOn,
            notificationId,
            userId
        );

        var readOn = DateTimeOffset.UtcNow;

        // Act
        appNotification.UpdateReadOn(readOn);

        // Assert
        Assert.Equal(readOn, appNotification.ReadOn);
    }
}