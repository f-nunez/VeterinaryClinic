using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.ApplicationUserRoleAggregate;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.UnitTests.ApplicationUserRoleAggregate;

public class ApplicationUserRoleTests
{
    [Fact]
    public void Constructor_Id_SetsIdProperty()
    {
        // Arrange
        var id = Guid.NewGuid();

        var roleId = Guid.NewGuid().ToString();

        var userId = Guid.NewGuid().ToString();

        var applicationRole = new ApplicationUserRole
        (
            id,
            roleId,
            userId
        );

        // Assert
        Assert.Equal(id, applicationRole.Id);
    }

    [Fact]
    public void Constructor_IdIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var id = Guid.Empty;

        var roleId = Guid.NewGuid().ToString();

        var userId = Guid.NewGuid().ToString();

        // Act
        Action actual = () => new ApplicationUserRole(id, roleId, userId);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_RoleId_SetsRoleIdProperty()
    {
        // Arrange
        var id = Guid.NewGuid();

        var roleId = Guid.NewGuid().ToString();

        var userId = Guid.NewGuid().ToString();

        var applicationRole = new ApplicationUserRole
        (
            id,
            roleId,
            userId
        );

        // Assert
        Assert.Equal(roleId, applicationRole.RoleId);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_RoleIdIsEmpty_ThrowsArgumentException(string roleId)
    {
        // Arrange
        var id = Guid.Empty;

        var userId = Guid.NewGuid().ToString();

        // Act
        Action actual = () => new ApplicationUserRole(id, roleId, userId);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_UserId_SetsUserIdProperty()
    {
        // Arrange
        var id = Guid.NewGuid();

        var roleId = Guid.NewGuid().ToString();

        var userId = Guid.NewGuid().ToString();

        var applicationRole = new ApplicationUserRole
        (
            id,
            roleId,
            userId
        );

        // Assert
        Assert.Equal(userId, applicationRole.UserId);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_UserIdIsEmpty_ThrowsArgumentException(string userId)
    {
        // Arrange
        var id = Guid.Empty;

        var roleId = Guid.NewGuid().ToString();

        // Act
        Action actual = () => new ApplicationUserRole(id, roleId, userId);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }
}